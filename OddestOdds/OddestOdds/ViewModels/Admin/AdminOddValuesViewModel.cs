using System;
using System.ComponentModel.DataAnnotations;

namespace OddestOdds.Web.ViewModels.Admin
{
    public class AdminOddValuesViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Home Odd Value")]
        [Required(ErrorMessage = "Please enter a home odd value")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid home odd value")]
        public decimal HomeOddValue { get; set; }

        [Display(Name = "Draw Odd Value")]
        [Required(ErrorMessage = "Please enter a draw odd value")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid draw odd value")]
        public decimal DrawOddValue { get; set; }


        [Display(Name = "Away Odd Value")]
        [Required(ErrorMessage = "Please enter a away odd value")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid away odd value")]
        public decimal AwayOddValue { get; set; }
    }
}
