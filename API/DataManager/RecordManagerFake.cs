using API.Models;
using API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataManager
{
    public class RecordManagerFake : IDataRepository<Record>
    {
        private readonly List<Record> _records;

        public RecordManagerFake()
        {
            _records = new List<Record>()
            {
                new Record() { ID = 1,
                    ClockInTime = DateTime.Now, ClockOutTime = DateTime.Now, IsActive = true },
                new Record() { ID = 2,
                    ClockInTime = DateTime.Now, ClockOutTime = DateTime.Now, IsActive = true },
                new Record() { ID = 3,
                    ClockInTime = DateTime.Now, ClockOutTime = DateTime.Now, IsActive = true },

            };
        }
        public async Task Add(Record entity)
        {
            entity.ID = 100;
            entity.ClockInTime = DateTime.Now;
            entity.ClockOutTime = DateTime.Now;
            _records.Add(entity);
        }

        public async Task Delete(Record entity)
        {
            var existing = _records.First(x => x.ID == entity.ID);
            _records.Remove(existing);
        }

        public async Task<Record> Get(int id)
        {
           return _records.First(x => x.ID == id);
        }

        public async Task<IEnumerable<Record>> GetAll(string searchByName, bool? searchByStatus)
        {
            return _records.Where(x => searchByStatus == null || x.IsActive == searchByStatus).Where(x => searchByName == null || x.EmployeeName.Contains(searchByName)).ToList();
        }

        public async Task Update(Record dbEntity, Record entity)
        {
            dbEntity.EmployeeName = entity.EmployeeName;
            dbEntity.ClockInTime = entity.ClockInTime;
            dbEntity.ClockOutTime = entity.ClockOutTime;
            dbEntity.IsActive = entity.IsActive;
        }
    }
}
