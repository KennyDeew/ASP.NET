using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class EfRepository<T> : IRepository<T>
        where T : BaseEntity

    {
        protected readonly DbContext Context;
        private readonly DbSet<T> _entitySet;

        public EfRepository(DataContext context)
        {
            Context = context;
            _entitySet = Context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return (await _entitySet.AsNoTracking().ToListAsync()).AsEnumerable();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id).AsTask();
        }


        public async Task<T> AddAsync(T entity)
        {
            var entityEntryAdded = await _entitySet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entityEntryAdded.Entity;
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return;
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
