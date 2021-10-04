using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/record")]
    [ApiController]
    public class RecordController : Controller
    {
        private readonly IDataRepository<Record> _dataRepository;
        public RecordController(IDataRepository<Record> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        // GET: api/Record
        [HttpGet]
        public async Task<IActionResult> Get(string searchByName, bool? searchByStatus)
        {
            IEnumerable<Record> records = await _dataRepository.GetAll(searchByName, searchByStatus);
            return Ok(records);
        }
        // GET: api/Record/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            Record record = await _dataRepository.Get(id);
            if (record == null)
            {
                return NotFound("The record couldn't be found.");
            }
            return Ok(record);
        }
        // POST: api/Record
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Record record)
        {
            if (record == null)
            {
                return BadRequest("Record is null.");
            }
            await _dataRepository.Add(record);
            return CreatedAtRoute(
                  "Get",
                  new { Id = record.ID },
                  record);
        }
        // PUT: api/Record/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Record record)
        {
            if (record == null)
            {
                return BadRequest("Record is null.");
            }
            Record recordToUpdate = await _dataRepository.Get(record.ID);
            if (recordToUpdate == null)
            {
                return NotFound("The Record record couldn't be found.");
            }
            await _dataRepository.Update(recordToUpdate, record);
            return NoContent();
        }
        // DELETE: api/Record/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Record record = await _dataRepository.Get(id);
            if (record == null)
            {
                return NotFound("The Record record couldn't be found.");
            }
            await _dataRepository.Delete(record);
            return NoContent();
        }
    }
}