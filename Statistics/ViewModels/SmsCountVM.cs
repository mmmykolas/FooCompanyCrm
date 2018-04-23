using System.ComponentModel;

namespace FooCompany.Statistics.ViewModels
{
    public class SmsCountVM
    {
        [DisplayName("MSISDN")]
        public string Msisdn { get; set; }

        [DisplayName("Sms Count")]
        public int SmsCount { get; set; }
    }
}
