using API.Models;
using API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace API.DataManager
{
    public class RecordManager : IDataRepository<Record>
    {
        readonly RecordContext _recordContext;
        public RecordManager(RecordContext context)
        {
            _recordContext = context;
        }
        public async Task<IEnumerable<Record>> GetAll(string searchByName, bool? searchByStatus)
        {
            return await _recordContext.Records.Where(x => searchByStatus == null || x.IsActive == searchByStatus).Where(x => searchByName == null || x.EmployeeName.Contains(searchByName)).ToListAsync();
        }
        public async Task<Record> Get(int id)
        {
            return await _recordContext.Records
                  .FirstOrDefaultAsync(e => e.ID == id);
        }
        public async Task Add(Record entity)
        {
            await _recordContext.Records.AddAsync(entity);
            await _recordContext.SaveChangesAsync();
        }
        public async Task Update(Record Record, Record entity)
        {
            Record.EmployeeName = entity.EmployeeName;
            Record.ClockInTime = entity.ClockInTime;
            Record.ClockOutTime = entity.ClockOutTime;
            Record.IsActive = entity.IsActive;
            await _recordContext.SaveChangesAsync();
        }
        public async Task Delete(Record Record)
        {
            _recordContext.Records.Remove(Record);
            await _recordContext.SaveChangesAsync();
        }
    }
}
