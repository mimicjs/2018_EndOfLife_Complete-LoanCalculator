using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBasedLoanCalculator.Models.LoanCalculator
{
    public class LoanRepaymentsModel
    {
        public decimal WeeklyRepayment { get; set; }
        public decimal MonthlyRepayment { get; set; }
        public decimal FullTermRepayment { get; set; }
    }
}
