using System;
using System.Linq;
using FooCompany.Statistics.Models;

namespace FooCompany.Statistics.Data
{
    public class DataInitializer
    {
        public static void CreateTestData(CustomerActivityStatisticsContext context)
        {
            context.Database.EnsureCreated();

            if (context.Activities.Any())
            {
                return;
            }

            var rnd = new Random(42);
            var seedDate = DateTime.Now;

            for (var i = 0; i < 100; i++)
            {
                context.Activities.Add(GenerateCall(rnd, seedDate));
                context.Activities.Add(GenerateSms(rnd, seedDate));
            }
            
            context.SaveChanges();
        }

        private static Call GenerateCall(Random rnd, DateTime seedDate)
        {
         var abc =  new Call
            {
                Msisdn = $"3706990000{rnd.Next(0, 9)}",
                Date = seedDate.AddDays(rnd.Next(-14, 0)).AddMinutes(rnd.Next(0, 59)),
                Duration = rnd.Next(20, 1200)
            };

            return abc;
        }


        private static Sms GenerateSms(Random rnd, DateTime seedDate) =>
            new Sms
            {
                Msisdn = $"3706990000{rnd.Next(0, 9)}",
                Date = seedDate.AddDays(rnd.Next(-14, 0)).AddMinutes(rnd.Next(0, 59))
            };
    }
}
