using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBasedLoanCalculator.Models.LoanCalculator;

namespace WebBasedLoanCalculator.Domain.Contracts
{
    public interface ILoanService
    {
        #region LoanCalculator
        Task<List<LoanRepaymentsModel>> CalculateLoanRepayments(List<LoanCalculatorViewModel> repaymentsToCalculate);
        Task<decimal> CalculateMonthlyRepayment(LoanCalculatorViewModel repaymentToCalculate);
        Task<decimal> ConvertMonthlyToWeeklyRepayment(decimal monthlyRepayment);
        Task<decimal> ConvertMonthlyToFullTermRepayment(decimal monthlyRepayment, int loanTerm);
        #endregion
    }
}
