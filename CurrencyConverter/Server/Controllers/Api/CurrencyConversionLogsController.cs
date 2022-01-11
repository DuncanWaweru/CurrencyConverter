using CurrencyConverter.Server.Models;
using CurrencyConverter.Server.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConversionLogsController : ControllerBase //endpoints for allcrud operations.
    {
        private readonly ICurrencyConversionLogRepository _currencyConversionLog;
        public CurrencyConversionLogsController(ICurrencyConversionLogRepository currencyConversionLog)
        {
            _currencyConversionLog = currencyConversionLog;
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrencyConversionLogs()
        {
            var currencyConversionLogs = await _currencyConversionLog.GetAsync();
            return Ok(currencyConversionLogs.OrderByDescending(x=>x.CreatedDate));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyConversionLog([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var currencyConversionLogId))
                return BadRequest("Invalid id");

            var currencyConversionLog = await _currencyConversionLog.GetAsync(currencyConversionLogId);


            return Ok(currencyConversionLog);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCurrencyConversionLog([FromBody] CurrencyConversionLog currencyConversionCreateViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (currencyConversionCreateViewModel == null)
                return BadRequest($"{nameof(currencyConversionCreateViewModel)} cannot be null");
            //

            var (success, error) = await _currencyConversionLog.CreateAsync(currencyConversionCreateViewModel);
            if (!success)
                return BadRequest(error);

            return Ok(new {message ="Conversion logged successfully." });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrencyConversionLog([FromRoute] string id, [FromBody] CurrencyConversionLog currencyConversionLog)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (currencyConversionLog == null)
                return BadRequest($"{nameof(currencyConversionLog)} cannot be null");

            if (!Guid.TryParse(id, out var currencyConversionLogId))
                return BadRequest("Invalid id");

            if (currencyConversionLogId != currencyConversionLog.Id)
                return BadRequest("Conflicting id in parameter and model data");

            var appCurrencyConversionLog = await _currencyConversionLog.GetAsync(currencyConversionLogId);

            if (appCurrencyConversionLog == null)
                return NotFound();


            var (success, error) = await _currencyConversionLog.UpdateAsync(appCurrencyConversionLog);
            if (!success)
                return BadRequest(error);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencyConversionLog([FromRoute] string id)
        {
            if (!Guid.TryParse(id, out var currencyConversionLogId))
                return BadRequest("Invalid id");

            var appCurrencyConversionLog = await _currencyConversionLog.GetAsync(currencyConversionLogId);

            if (appCurrencyConversionLog == null)
                return NotFound(id);


            var (success, error) = await _currencyConversionLog.DeleteAsync(appCurrencyConversionLog);
            if (!success)
                return BadRequest(error);

            return Ok(appCurrencyConversionLog);
        }

       

    }
}