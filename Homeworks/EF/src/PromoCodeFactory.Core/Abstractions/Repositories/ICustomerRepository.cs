﻿using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IQueryable<Customer>> GetAllWithPreferenceAsync();

        Task<Customer> GetByIdWithPreferenceAsync(Guid id);
    }
}
