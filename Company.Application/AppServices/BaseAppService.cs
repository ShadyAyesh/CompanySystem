using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanySystem.Application.Interface.AppServices;
using CompanySystem.Domain.Interfaces.Services;

namespace CompanySystem.Application.AppServices
{
    public class BaseAppService<TEntity> : IDisposable, IBaseAppService<TEntity>
        where TEntity : class
    {
        private readonly IBaseService<TEntity> _baseService;

        public BaseAppService(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }

        public async Task<TEntity> Add(TEntity entity) => await _baseService.Add(entity);

        public async Task<TEntity> GetById(int id, string include = null) => 
            await _baseService.GetById(id, include);

        public async Task<IEnumerable<TEntity>> GetAll(string include = null) => 
            await _baseService.GetAll(include);
        
        public async Task<TEntity> Update(TEntity entity) => await _baseService.Update(entity);

        public async Task Delete(TEntity entity) => await _baseService.Delete(entity);

        public void Dispose() => _baseService.Dispose();
    }
}