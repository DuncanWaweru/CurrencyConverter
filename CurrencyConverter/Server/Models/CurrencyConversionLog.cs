using CurrencyConverter.Server.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Server.Models
{
    public class CurrencyConversionLog : AuditableEntity
    {
        public string FromCurrency { get; set; }
        public string FinalCurrency { get; set; }
        public double ConversionRate { get; set; }
        public double AmountToConvert { get; set; }
        public double ConvertedAmount { get; set; }
    }
}
