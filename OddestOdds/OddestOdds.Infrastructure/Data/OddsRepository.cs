using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OddestOdds.Core.Interfaces;
using OddestOdds.Core.Shared;

namespace OddestOdds.Infrastructure.Data
{
    public class OddsRepository : IOddsRepository
    {
        private readonly OddestOddsContext _dbContext;

        public OddsRepository(OddestOddsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetByIdAsync<T>(Guid id, string[] includes) where T : BaseEntity
        {
            var list = _dbContext.Set<T>().AsQueryable();
            return await includes.Aggregate(list.AsQueryable(), (query, path) => query.Include(path)).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> ListAsync<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<List<T>> ListAsync<T>(string[] includes) where T : BaseEntity
        {
            var list = _dbContext.Set<T>().AsQueryable();
            return await includes.Aggregate(list.AsQueryable(), (query, path) => query.Include(path)).ToListAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(Guid id) where T : BaseEntity
        {
            var entity = await GetByIdAsync<T>(id);
            await DeleteAsync<T>(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
