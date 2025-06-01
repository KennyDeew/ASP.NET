using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Core.Domain.Administration
{
    public class Role
        : BaseEntity
    {
        [Column("name"), MaxLength(50)]
        public string Name { get; set; }
        [Column("description"), MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// Список Employees
        /// </summary>
        public ICollection<Employee>? Employees { get; set; }


    }
}