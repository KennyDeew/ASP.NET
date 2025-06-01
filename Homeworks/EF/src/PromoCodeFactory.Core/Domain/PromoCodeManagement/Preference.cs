using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Preference
        : BaseEntity
    {
        [Column("name"), MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Список CustomerPreferences
        /// </summary>
        public ICollection<CustomerPreference>? CustomerPreferences { get; set; }

        /// <summary>
        /// Список PromoCodes
        /// </summary>
        public ICollection<PromoCode>? PromoCodes { get; set; }
    }
}