using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OddestOdds.Core.Shared;
using OddestOdds.Infrastructure.Entities;

namespace OddestOdds.Core.Entities
{
    public class Odd : BaseEntity
    {
        [Required]
        public string OddName { get; set; }

        [Required]
        public string HomeTeamName { get; set; }

        [Required]
        public string AwayTeamName { get; set; }

        [ForeignKey("OddValueId")]
        public OddValue OddValues { get; set; }
    }
}
