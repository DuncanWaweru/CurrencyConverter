using CurrencyConverter.Server.Models;
using CurrencyConverter.Server.Repository.Interfaces;
using CurrencyConverter.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CurrencyConverter.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConvertersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICurrencyConversionLogRepository _currencyConversionLog;
        public CurrencyConvertersController(IConfiguration configuration, ICurrencyConversionLogRepository currencyConversionLog)
        {
            _configuration = configuration;
            _currencyConversionLog = currencyConversionLog;
        }

        //fetch currencies
        ///TODO: fetch and store in our database. For now we are fetching from open api
        /// The api url is https://openexchangerates.org/api/currencies.json

        [HttpGet("GetAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
 
            List<CurrencyViewModel> currencies = new List<CurrencyViewModel>();
            using (var client = new HttpClient())
            {
                var Baseurl = "https://openexchangerates.org/";
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/currencies.json?prettyprint=true&show_alternative=true&show_inactive=false");
                if (response.IsSuccessStatusCode)
                {
                    var currenciesResponse = response.Content.ReadAsStringAsync().Result;
                    var currenciesArray = currenciesResponse.Replace("{", "").Replace("}", "").Replace("\"","").Split(",");
                    foreach(var item in currenciesArray)
                    {
                        var itemArray = item.Split(":");
                        currencies.Add(new CurrencyViewModel() {
                        
                        CountryName= itemArray[1],
                        ShortCode = itemArray[0].Replace(" ", "").Replace("\n", "").Replace("\r", "")
                    });
                    }
                   // currencies = JsonConvert.DeserializeObject<List<CurrencyViewModel>>(currenciesResponse);
                    return Ok(currencies);
                }
                else 
                {
                    return BadRequest(response.Content.ReadAsStringAsync().Result);
                }                
            }
        }

        //do the actual conversion
        [HttpPost("Convert")]
        public async Task<IActionResult> ConvertCurrencies([FromBody] CurrencyConversionCreateViewModel currencyConversion)
        {
            ConversionResponseViewModel conversionResponseViewModel = new ConversionResponseViewModel();
        //currencyConversion
        var key = _configuration["currencyconverterapi"].ToString();
            var currencyCode = $"{currencyConversion.FromCurrency}_{currencyConversion.FinalCurrency}";

            //var endPoint = $"api/convert/{currencyConversion.AmountToConvert}/{currencyConversion.FromCurrency}/{currencyConversion.FinalCurrency}?app_id={key}"; //requires payments
            var endPoint = $"api/v7/convert?q={currencyCode}&compact=ultra&apiKey={key}";
            
            using (var client = new HttpClient())
            {
                var Baseurl = "https://free.currconv.com/";
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    var currenciesResponse = response.Content.ReadAsStringAsync().Result;

                    var json = Convert.ToDouble(JObject.Parse(currenciesResponse)[currencyCode]);
                    //log the request in our database
                    var conversionLog = new CurrencyConversionLog()
                    {
                        FinalCurrency = currencyConversion.FinalCurrency,
                        AmountToConvert = currencyConversion.AmountToConvert,
                        FromCurrency = currencyConversion.FromCurrency,
                        ConversionRate = json,
                        ConvertedAmount = json* currencyConversion.AmountToConvert
                    };
                   await  _currencyConversionLog.CreateAsync(conversionLog);

                    return Ok(json.ToString());
                }
                else
                {
                    return BadRequest(response.Content.ReadAsStringAsync().Result);
                }
            }
        }
    }
}
