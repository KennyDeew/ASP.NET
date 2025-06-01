using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    /// <summary>
    /// связывающая таблица для Customer и Preference
    /// </summary>
    public class CustomerPreference
    {
        public Guid CustomerId { get; set; }
        public Guid PreferenceId { get; set; }
        public Customer Customer { get; set; }
        public Preference Preference { get; set; }
    }
}
