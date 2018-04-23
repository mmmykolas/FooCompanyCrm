using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FooCompany.Statistics.ViewModels
{
    public class CallDurationVM
    {
        [DisplayName("MSISDN")]
        public string Msisdn { get; set; }

        [DisplayName("Duration of Calls")]
        [DisplayFormat(DataFormatString = "{0:%d}d {0:%hh}h {0:%mm}m {0:%ss}s")]
        public TimeSpan CallDuration { get; set; }

        [DisplayName("Total Duration of Calls")]
        public string CallDurationFormatted => $"{CallDuration.Days * 24 + CallDuration.Hours}:{CallDuration.Minutes:D2}:{CallDuration.Seconds:D2}";
    }
}
