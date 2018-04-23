using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FooCompany.Statistics.Models
{
    public class Call : CustomerActivity
    {
        [Required]
        [Column("duration")]
        public int Duration { get; set; }
    }
}
