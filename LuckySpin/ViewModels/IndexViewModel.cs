using System;
using System.ComponentModel.DataAnnotations;
namespace LuckySpin.ViewModels
{
    public class IndexViewModel
    {
        [Display(Prompt = "  First Name")]
        [Required(ErrorMessage = "Please enter your Name")]
        public String FirstName { get; set; }

        [Range(1, 9, ErrorMessage = "Choose a number")]
        public Int16 Luck { get; set; }

        [Display(Prompt = "  Start with $3 to $10")]
        [DataType(DataType.Currency)]
        [Range(3.0, 10.0, ErrorMessage = "Amount must be between $3 and $10.")]
        public Decimal StartingBalance { get; set; }
    }
}
