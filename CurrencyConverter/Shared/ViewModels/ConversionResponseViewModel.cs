using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Shared.ViewModels
{
 

    public class Request
    {
        public string query { get; set; }
        public double amount { get; set; }
        public string from { get; set; }
        public string to { get; set; }
    }

    public class Meta
    {
        public int timestamp { get; set; }
        public double rate { get; set; }
    }

    public class ConversionResponseViewModel
    {
        public string disclaimer { get; set; }
        public string license { get; set; }
        public Request request { get; set; }
        public Meta meta { get; set; }
        public double response { get; set; }
    }


}
