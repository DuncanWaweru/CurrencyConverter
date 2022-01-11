using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Shared.ViewModels
{


    public class CurrencyConversionLogViewModel 
    {
        public Guid Id { get; set; }
        public string FromCurrency { get; set; }
        public string FinalCurrency { get; set; }
        public double ConversionRate { get; set; }
        public double AmountToConvert { get; set; }
        public double ConvertedAmount { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
