using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Repositories
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context) 
        {

        }

        public async Task<IQueryable<Customer>> GetAllWithPreferenceAsync()
        {
            return Context.Set<Customer>().Include(c => c.CustomerPreferences).ThenInclude(cp => cp.Preference).AsNoTracking() ;
        }

        public async Task<Customer> GetByIdWithPreferenceAsync(Guid id)
        {
            return await Context.Set<Customer>().Include(c => c.CustomerPreferences).ThenInclude(cp => cp.Preference).Include(c => c.Promocodes).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
