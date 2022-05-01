using DvD_Api.Data;
using DvD_Api.DTO;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoanTypeController: ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public LoanTypeController(ApplicationDbContext database)
        {
            _db = database;
        }

        [HttpPost]
        public async Task<IActionResult> AddLoantype(LoanTypeDto loanType) {
            var newLoanType = new LoanType { 
                LoanTypeNumber = 0,
                LoanTypeName = loanType.TypeName,
                Duration = loanType.Duration,
            };

            await _db.LoanTypes.AddAsync(newLoanType);
            await _db.SaveChangesAsync();
            return Ok(newLoanType);
        }

        [HttpGet("forLoan")]
        public IEnumerable<object> GetLoanType() { 
            return _db.LoanTypes.Select(x => new {
                LoanTypeNumber = x.LoanTypeNumber,
                loanTypeName = x.LoanTypeName,
            });
        }
    }
}
