using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanySystem.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);

        Task<TEntity> GetById(int id, string include = null);

        Task<IEnumerable<TEntity>> GetAll(string include = null);

        Task<TEntity> Update(TEntity entity);

        Task Delete(TEntity entity);

        void Dispose();
    }
}