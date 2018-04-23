using Microsoft.EntityFrameworkCore;
using FooCompany.Statistics.Models;

namespace FooCompany.Statistics.Data
{
    public class CustomerActivityStatisticsContext : DbContext
    {
        public CustomerActivityStatisticsContext(DbContextOptions<CustomerActivityStatisticsContext> options) : base(options)
        {
        }

        public DbSet<CustomerActivity> Activities { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Sms> Sms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerActivity>().ToTable("CustomerActivity");

            modelBuilder.Entity<CustomerActivity>()
                .HasDiscriminator<string>("type")
                .HasValue<Call>("call")
                .HasValue<Sms>("sms");

            modelBuilder.Entity<CustomerActivity>()
                .HasIndex(ca => new { ca.Date, ca.Msisdn });
        }

        public DbSet<FooCompany.Statistics.Models.Call> Call { get; set; }
    }
}
