using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FooCompany.Statistics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FooCompany.Statistics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FooCompany.Statistics.Data.CustomerActivityStatisticsContext _context;

        public IndexModel(FooCompany.Statistics.Data.CustomerActivityStatisticsContext context)
        {
            _context = context;
        }

        #region filter values
        [BindProperty]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FilterFrom { get; set; } = DateTime.Today.AddDays(-10);

        [BindProperty]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FilterTo { get; set; } = DateTime.Today.AddDays(10);
        #endregion

        #region viewmodel properties
        [DisplayName("Total Sms Count")]
        public int TotalSms { get; private set; }

        [DisplayName("Total Duration of Calls")]
        [DisplayFormat(DataFormatString = "{0:%d}d {0:%hh}h {0:%mm}m {0:%ss}s")]
        public TimeSpan TotalCallDuration { get; private set; }

        [DisplayName("Total Duration of Calls")]
        public string TotalCallDurationFormatted => $"{TotalCallDuration.Days * 24 + TotalCallDuration.Hours}:{TotalCallDuration.Minutes}:{TotalCallDuration.Seconds}";

        public List<SmsCountVM> TopSmsMsisdns;

        public List<CallDurationVM> TopCallMsisdns;
        #endregion

        public async Task<IActionResult> OnGetAsync()
        {
            return await GetValues(null, null);
        }

        public async Task<IActionResult> OnPostAsync(DateTime? filterFrom, DateTime? filterTo)
        {
            return await GetValues(filterFrom, filterTo);
        }

        private async Task<IActionResult> GetValues(DateTime? filterFrom, DateTime? filterTo)
        {
            FilterFrom = filterFrom ?? new DateTime();
            FilterTo = filterTo ?? DateTime.Today;
            var endOfDay = FilterTo.AddDays(1).AddSeconds(-1);

            TotalSms = await _context.Sms.CountAsync(s => s.Date >= FilterFrom && s.Date <= endOfDay);

            TotalCallDuration = TimeSpan.FromSeconds
                (
                await _context.Calls
                    .Where(f => f.Date >= FilterFrom && f.Date <= endOfDay)
                    .SumAsync(c => c.Duration)
                );

            TopCallMsisdns = await _context.Calls
                .Where(c => c.Date >= FilterFrom && c.Date <= endOfDay)
                .GroupBy(c => c.Msisdn)
                .Select(g => new CallDurationVM { Msisdn = g.Key, CallDuration = TimeSpan.FromSeconds(g.Sum(c => c.Duration)) })
                .OrderByDescending(c => c.CallDuration)
                .Take(5)
                .ToListAsync();

            TopSmsMsisdns = await _context.Sms
                .Where(s => s.Date >= FilterFrom && s.Date <= endOfDay)
                .GroupBy(s => s.Msisdn)
                .Select(g => new SmsCountVM { Msisdn = g.Key, SmsCount = g.Count() })
                .OrderByDescending(c => c.SmsCount)
                .Take(5)
                .ToListAsync();

            return Page();
        } 
    }
}
