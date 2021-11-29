using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanySystem.Domain.Interfaces.Repositories;
using CompanySystem.Domain.Interfaces.Services;

namespace CompanySystem.Domain.Services
{
    public class BaseService<TEntity> : IDisposable, IBaseService<TEntity>
        where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> Add(TEntity entity) => await _repository.Add(entity);
        
        public async Task<TEntity> GetById(int id, string include = null) => await _repository.GetById(id, include);

        public async Task<IEnumerable<TEntity>> GetAll(string include = null) => 
            await _repository.GetAll(include);

        public async Task<TEntity> Update(TEntity entity) => await _repository.Update(entity);

        public async Task Delete(TEntity entity) => await _repository.Delete(entity);

        public void Dispose() => _repository.Dispose();
    }
}