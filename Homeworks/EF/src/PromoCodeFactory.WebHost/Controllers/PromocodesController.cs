using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {
        private readonly IRepository<PromoCode> _promoCodeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRepository<Preference> _preferenceRepository;

        public PromocodesController(IRepository<PromoCode> promoCodeRepository, ICustomerRepository customerRepository, IRepository<Preference> preferenceRepository)
        {
            _promoCodeRepository = promoCodeRepository;
            _customerRepository = customerRepository;
            _preferenceRepository = preferenceRepository;
        }

        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            var promocodes = await _promoCodeRepository.GetAllAsync();

            var promocodesShortResponseList = promocodes.Select(x =>
                new PromoCodeShortResponse()
                {
                    Id = x.Id,
                    Code = x.Code,
                    ServiceInfo = x.ServiceInfo,
                    BeginDate = x.BeginDate.ToString(),
                    EndDate = x.EndDate.ToString(),
                    PartnerName = x.PartnerName
                }).ToList();

            return promocodesShortResponseList;
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            var preferences = await _preferenceRepository.GetAllAsync();
            var preference = preferences.FirstOrDefault(p => p.Name == request.Preference);
            if (preference == null)
            {
                return Problem("Не найдено указанное предпочтение");
            }
            var customers = await _customerRepository.GetAllWithPreferenceAsync();
            foreach (var customer in customers.Where(c => c.CustomerPreferences != null && c.CustomerPreferences.Select(cp => cp.Preference).Contains(preference)))
            {
                var promocode = new PromoCode()
                {
                    Id = Guid.NewGuid(),
                    Code = request.PromoCode,
                    ServiceInfo = request.ServiceInfo,
                    PartnerName= request.PartnerName,
                    PreferenceId = preference.Id,
                    CustomerId = customer.Id,
                    BeginDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(7)
                };
                var created = await _promoCodeRepository.AddAsync(promocode);
            }
            return NoContent();
        }
    }
}