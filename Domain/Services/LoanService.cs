using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBasedLoanCalculator.Models.LoanCalculator;
using WebBasedLoanCalculator.Domain.Contracts;

namespace WebBasedLoanCalculator.Domain.Services
{
    public class LoanService : ILoanService
    {
        //Accepts & Iterates through a list of Loan elements (including loan principal, term, interest
        //Calls CalculateMonthlyRepayment(), ConvertMonthlyToWeeklyRepayment(), ConvertMonthlyToFullTermRepayment()
        //Returns a list of Loan Repayments (weekly, monthly, full-term)
        public async Task<List<LoanRepaymentsModel>> CalculateLoanRepayments(List<LoanCalculatorViewModel> repaymentsToCalculate) {
            List<LoanRepaymentsModel> repaymentsCalculated = new List<LoanRepaymentsModel>();
            foreach (LoanCalculatorViewModel repaymentToCalculate in repaymentsToCalculate)
            {
                decimal monthlyRepayment = await CalculateMonthlyRepayment(repaymentToCalculate);
                decimal weeklyRepayment = await ConvertMonthlyToWeeklyRepayment(monthlyRepayment);
                decimal fullTermRepayment = await ConvertMonthlyToFullTermRepayment(monthlyRepayment, repaymentToCalculate.LoanTerm);
                LoanRepaymentsModel repaymentCalculated = new LoanRepaymentsModel
                {
                    WeeklyRepayment = weeklyRepayment,
                    MonthlyRepayment = monthlyRepayment,
                    FullTermRepayment = fullTermRepayment
                };
                repaymentsCalculated.Add(repaymentCalculated);
            }
            return repaymentsCalculated;
        }

        //Accepts Loan elements 
        //Calculates its monthly repayment using the Loan Payment Formula
        //Note: Involves service fees i.e. $240 + $1.80 * loan's term
        public async Task<decimal> CalculateMonthlyRepayment(LoanCalculatorViewModel repaymentToCalculate)
        {
            int term = repaymentToCalculate.LoanTerm;
            decimal principal = repaymentToCalculate.LoanPrincipal + 240m + 1.8m * term;
            decimal interest = repaymentToCalculate.LoanInterestRate / 100;
            decimal repayment = (principal * interest / 12) / (1 - (decimal)Math.Pow(decimal.ToDouble(1 + (interest / 12)), Convert.ToDouble(-term)));
            return Math.Round(repayment, 2);
        }
        
        //Accepts calculated Monthly repayment
        //Calculates Weekly repayment by dividing the monthly repayment evenly throughout each week of the year
        public async Task<decimal> ConvertMonthlyToWeeklyRepayment(decimal monthlyRepayment)
        {
            decimal weeklyRepayment = monthlyRepayment * 12 / 52;
            return Math.Round(weeklyRepayment, 2);
        }

        //Accepts calculated Monthly repayment & Loan's term
        //Calculates Full-term repayment by multiplying Monthly Repayment with Loan's Term
        public async Task<decimal> ConvertMonthlyToFullTermRepayment(decimal monthlyRepayment, int loanTerm)
        {
            decimal fullTermRepayment = monthlyRepayment * loanTerm;
            return Math.Round(fullTermRepayment, 2);
        }
    }
}
