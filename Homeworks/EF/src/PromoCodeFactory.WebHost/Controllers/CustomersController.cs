using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController
        : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Получить всех клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<CustomerShortResponse>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync() ?? new List<Customer>();

            var customerShortResponseList = customers.Select(x =>
                new CustomerShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToList();

            return customerShortResponseList;
        }

        /// <summary>
        /// Получить данные клиента по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdWithPreferenceAsync(id);

            if (customer == null)
                return NotFound();
            //var customersPreferencesList = (await _customerRepository.GetByIdWithPreferenceAsync(id)).CustomerPreferences.Select(cp => cp.Preference).ToList();

            var customerResponse = new CustomerResponse()
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Preferences = customer.CustomerPreferences?.Select(cp => new PreferenceResponse { Name = cp.Preference.Name }).ToList() ?? new List<PreferenceResponse>(),
                PromoCodes = customer.Promocodes?.Select(p => new PromoCodeShortResponse {Id = p.Id, Code = p.Code, BeginDate = p.BeginDate.ToString(), EndDate = p.EndDate.ToString(), PartnerName = p.PartnerName, ServiceInfo = p.ServiceInfo }).ToList() ?? new List<PromoCodeShortResponse>()
            };
            return customerResponse;
        }

        /// <summary>
        /// Получить неполные данные клиента по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("short/{id:guid}", Name = "GetCustomerShort")]
        [ProducesResponseType(typeof(CustomerShortResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CustomerShortResponse>> GetShortCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return NotFound();

            var customerShortResponse = new CustomerShortResponse()
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            return Ok(customerShortResponse);
        }

        /// <summary>
        /// Добавить нового клиента
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmployeeResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
            customer.CustomerPreferences = request.PreferenceIds?.Select(p => new CustomerPreference() { CustomerId = customer.Id, PreferenceId = p }).ToList()?? null;
            var createdCustomer = await _customerRepository.AddAsync(customer);
            if (createdCustomer == null) return Problem("Не удалось создать клиента");
            var customerShortResponse = new CustomerShortResponse()
            {
                Id = createdCustomer.Id,
                Email = createdCustomer.Email,
                FirstName = createdCustomer.FirstName,
                LastName = createdCustomer.LastName
            };

            return CreatedAtRoute("GetCustomerShort", new { id = createdCustomer.Id }, customerShortResponse);
        }

        /// <summary>
        /// Обновить существующего клиента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdWithPreferenceAsync(id);
            if (customer == null)
                return NotFound();
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.CustomerPreferences = request.PreferenceIds?.Select(p => new CustomerPreference() { CustomerId = customer.Id, PreferenceId = p }).ToList() ?? null;
            await _customerRepository.UpdateAsync(customer);
            return Ok(id);
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}