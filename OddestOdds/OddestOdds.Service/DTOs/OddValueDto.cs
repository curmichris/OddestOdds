using System;

namespace OddestOdds.Service.DTOs
{
    public class OddValueDto
    {
        public Guid Id { get; set; }
        public decimal HomeOddValue { get; set; }
        public decimal DrawOddValue { get; set; }
        public decimal AwayOddValue { get; set; }
    }
}
