using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FooCompany.Statistics.Models
{
    public abstract class CustomerActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(11)]
        [Column("msisdn", Order = 0)]
        public string Msisdn { get; set; }
        
        [Required]
        [Column("date", Order = 1)]
        public DateTime Date { get; set; }
    }
}
