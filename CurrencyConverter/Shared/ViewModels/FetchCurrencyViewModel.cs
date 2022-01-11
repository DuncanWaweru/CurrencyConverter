using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Shared.ViewModels
{
    public class FetchCurrencyViewModel //not necessary as the api uses queryparameters not body. I had made it anyways.
    {
        public bool prettyprint { get; set; } = true;
        public bool show_alternative { get; set; } = true;
        public bool show_inactive { get; set; } = false;
    }
}
