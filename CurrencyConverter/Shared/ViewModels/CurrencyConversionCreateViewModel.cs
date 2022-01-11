using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Shared.ViewModels
{
    public class CurrencyConversionCreateViewModel
    {
        public string FromCurrency { get; set; }
        public string FinalCurrency { get; set; }
        public double AmountToConvert { get; set; }
        public double ConvertedAmount { get; set; }
    }
}
