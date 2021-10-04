using API.Controllers;
using API.DataManager;
using API.Repository;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Test
{
    public class RecordControllerTest
    {
        IDataRepository<API.Models.Record> _service;

        public RecordControllerTest()
        {
            _service = new RecordManagerFake();
        }

        [Fact]
        public void Get_ReturnsOkResult()
        {
            var result = _service.GetAll("", null);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Get_ReturnsAllRecords()
        {
            var okResult = _service.GetAll("", null).Result as OkObjectResult;

            var items = Assert.IsType<List<Record>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_ReturnsNotFoundResult()
        {
            var notFoundResult = _service.Get(1000);

            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ReturnsOkResult()
        {
            var okResult = _service.Get(1);

            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ReturnsRightItem()
        {
            var testGuid = 2;

            var okResult = _service.Get(2);

            Assert.IsType<Record>(okResult);

            Assert.Equal(testGuid, okResult.Id);
        }
    }
   
}
