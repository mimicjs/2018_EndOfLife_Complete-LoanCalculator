using System.ComponentModel.DataAnnotations;

namespace WebBasedLoanCalculator.Models.LoanCalculator
{
    public class LoanCalculatorViewModel
    {
        [Required(ErrorMessage = "Loan Principal is required")]
        [Range(1000, 20001, ErrorMessage = "Loan Principal is invalid. Choose between 1000 - 20,000")]
        public int LoanPrincipal { get; set; }
        [Required(ErrorMessage = "Loan Term is required")]
        [Range(6, 61, ErrorMessage = "Loan Term is invalid. Choose between 6 months - 60 months")]
        public int LoanTerm { get; set; }
        [Required(ErrorMessage = "Interest Rate is required")]
        [Range(8, 21, ErrorMessage = "Interest Rate is invalid. Choose between 8% - 20%")]
        public decimal LoanInterestRate { get; set; }
    }
}
