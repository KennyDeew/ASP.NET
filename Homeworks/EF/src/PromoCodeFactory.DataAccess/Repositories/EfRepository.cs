using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
