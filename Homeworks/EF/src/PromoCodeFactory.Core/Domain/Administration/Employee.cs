using PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Core.Domain.Administration
{
    public class Employee
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

        [Column("mobile"), MaxLength(50)]
        public string mobile { get; set; }

        public Role Role { get; set; }

        public Guid RoleId { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }
}