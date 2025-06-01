using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using PromoCodeFactory.Core.Domain;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class PromoCode
        : BaseEntity
    {
        [Column("code"), MaxLength(50)]
        public string Code { get; set; }

        [Column("serviceinfo"), MaxLength(200)]
        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column("partnername"), MaxLength(100)]
        public string PartnerName { get; set; }

        public Employee PartnerManager { get; set; }

        public Preference Preference { get; set; }

        public Customer Customer { get; set; }

        public Guid CustomerId { get; set; }

        public Guid PreferenceId { get; set; }
    }
}