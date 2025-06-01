using PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Customer
        : BaseEntity
    {
        [Column("firstname"), MaxLength(50)]
        public string FirstName { get; set; }
        [Column("lastname"), MaxLength(50)]
        public string LastName { get; set; }

        [Column("fullname"), MaxLength(101)]
        public string FullName => $"{FirstName} {LastName}";

        [Column("email"), MaxLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Список CustomerPreferences
        /// </summary>
        public ICollection<CustomerPreference>? CustomerPreferences { get; set; }

        /// <summary>
        /// Список Promocodes
        /// </summary>
        public ICollection<PromoCode>? Promocodes { get; set; }
    }
}