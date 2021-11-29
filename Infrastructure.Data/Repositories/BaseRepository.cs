using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanySystem.Domain.Interfaces.Repositories;
using CompanySystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity>
        where TEntity : class
    {
        protected CompanyContext Context = new();

        public async Task<TEntity> Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> GetById(int id, string include = null)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);

            if (include != null && entity != null)
                await Context.Entry(entity).Collection(include).LoadAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAll(string include = null)
        {
            if (include != null)
                return await Context.Set<TEntity>().AsNoTracking().Include(include).ToListAsync();

            return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.DisposeAsync();
        }
    }
}