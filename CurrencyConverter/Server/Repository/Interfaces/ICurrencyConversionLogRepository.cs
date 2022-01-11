using CurrencyConverter.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Server.Repository.Interfaces
{
    public interface ICurrencyConversionLogRepository //: IRepository<CurrencyConversionLog>
    {
        Task<IEnumerable<CurrencyConversionLog>> GetAsync();
        Task<CurrencyConversionLog> FindAsync(Guid id);
        Task<CurrencyConversionLog> GetAsync(Guid id);
        Task<(bool Success, string Error)> CreateAsync(CurrencyConversionLog currencyConversionLog);
        Task<(bool Success, string Error)> CreateAsync(IEnumerable<CurrencyConversionLog> currencyConversionLogs);
        Task<(bool Success, string Error)> UpdateAsync(CurrencyConversionLog currencyConversionLog);
        Task<(bool Success, string Error)> UpdateAsync(IEnumerable<CurrencyConversionLog> currencyConversionLogs);
        Task<(bool Success, string Error)> DeleteAsync(CurrencyConversionLog currencyConversionLog);
        Task<(bool Success, string Error)> DeleteAsync(IEnumerable<CurrencyConversionLog> currencyConversionLogs);
    }
}