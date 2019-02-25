using System;
using System.ComponentModel.DataAnnotations;

namespace OddestOdds.Web.ViewModels.Admin
{
    public class AdminOddsViewModel
    {
        public Guid Id { get; set; }
        public string OddsName { get; set; }

        [Display(Name = "Home Team Name")]
        [Required(ErrorMessage = "Please enter Home Team Name")]
        public string HomeTeamName { get; set; }

        [Display(Name = "Away Team Name")]
        [Required(ErrorMessage = "Please enter Away Team Name")]
        public string AwayTeamName { get; set; }
        public AdminOddValuesViewModel OddValues { get; set; }
    }
}
