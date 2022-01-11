using CurrencyConverter.Server.Data;
using CurrencyConverter.Server.Models;
using CurrencyConverter.Server.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Server.Repository
{
    public class CurrencyConversionLogRepository :ICurrencyConversionLogRepository
    {

        public CurrencyConversionLogRepository(ApplicationDbContext appContext)
        {
            AppContext = appContext;
        }
        public async Task<IEnumerable<CurrencyConversionLog>> GetAsync()
        {
           // var AppContext = new ApplicationDbContext();
            return await AppContext.CurrencyConversionLogs
                .ToListAsync();
        }

        public async Task<CurrencyConversionLog> FindAsync(Guid id)
        {
            return await AppContext.CurrencyConversionLogs
                .FindAsync(id);
        }

        public async Task<CurrencyConversionLog> GetAsync(Guid id)
        {
            return await AppContext.CurrencyConversionLogs
                .FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<(bool Success, string Error)> CreateAsync(CurrencyConversionLog currencyConversionLog)
        {
            await AppContext.CurrencyConversionLogs.AddAsync(currencyConversionLog);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> CreateAsync(IEnumerable<CurrencyConversionLog> currencyConversionLogs)
        {
            await AppContext.CurrencyConversionLogs.AddRangeAsync(currencyConversionLogs);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> UpdateAsync(CurrencyConversionLog currencyConversionLog)
        {
            AppContext.CurrencyConversionLogs.Update(currencyConversionLog);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> UpdateAsync(IEnumerable<CurrencyConversionLog> currencyConversionLogs)
        {
            AppContext.CurrencyConversionLogs.UpdateRange(currencyConversionLogs);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> DeleteAsync(CurrencyConversionLog currencyConversionLog)
        {
            AppContext.CurrencyConversionLogs.Remove(currencyConversionLog);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        public async Task<(bool Success, string Error)> DeleteAsync(IEnumerable<CurrencyConversionLog> currencyConversionLogs)
        {
            AppContext.CurrencyConversionLogs.RemoveRange(currencyConversionLogs);

            try
            {
                await AppContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }

        private ApplicationDbContext AppContext;
    }
}
