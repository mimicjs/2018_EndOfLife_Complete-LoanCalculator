using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBasedLoanCalculator.Models.LoanCalculator;
using WebBasedLoanCalculator.Domain.Contracts;

namespace WebBasedLoanCalculator.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // Accepts user's inputs sent via query string in a list of LoanCalculatorViewModel objects,
        // Returns list of calculated loan repayments (weekly, monthly, full-term)
        [HttpPost("GetLoanRepayments")]
        public async Task<IActionResult> GetLoanRepayments([FromBody] List<LoanCalculatorViewModel> repaymentsToCalculate)
        {
            try
            {
                if (!ModelState.IsValid || repaymentsToCalculate.Count < 0)
                {
                    //if (!ModelState.IsValid)
                    //{
                    //    //Is able to produces unwanted error messages
                    //    var errors = ModelState.Select(x => x.Value.Errors).ToList();
                    //    return Json(new { Success = false, Message = errors });
                    //}
                    //else
                    //{
                        return Json(new { Success = false, Message = "Invalid input formatting" });
                    //}
                }

                List<LoanRepaymentsModel> repaymentsCalculated = await _loanService.CalculateLoanRepayments(repaymentsToCalculate);
                
                return Json(new { Success = true, repaymentsCalculated });
            }
            catch (Exception e)
            {
                return Json(new { Success = false, Message = "Internal Server Error" });
            }
        }

        
    }
}
