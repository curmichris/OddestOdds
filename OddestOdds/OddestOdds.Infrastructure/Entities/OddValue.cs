using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OddestOdds.Core.Shared;

namespace OddestOdds.Infrastructure.Entities
{
    public class OddValue : BaseEntity
    {
        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal HomeOddValue { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal DrawOddValue { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal AwayOddValue { get; set; }
    }
}
