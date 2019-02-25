using System;

namespace OddestOdds.Service.DTOs
{
    public class OddDto
    {
        public Guid Id { get; set; }
        public string OddName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public OddValueDto OddValues { get; set; }
    }
}
