using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public interface IDataRepository<TEntity>
    {
        Task<IEnumerable<Record>> GetAll(string searchByName, bool? searchByStatus);
        Task<TEntity> Get(int id);
        Task Add(TEntity entity);
        Task Update(TEntity dbEntity, TEntity entity);
        Task Delete(TEntity entity);
    }
}
