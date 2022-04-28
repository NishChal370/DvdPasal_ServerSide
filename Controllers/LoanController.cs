using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;

// TODO create a data transfer object

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("aip/[Controller]")]
    public class LoanController : ControllerBase
    {
        public readonly ApplicationDbContext _db;

        public LoanController(ApplicationDbContext database)
        {
            _db = database;
        }

        [HttpPost]
        public async Task<IActionResult> AddLoan(Loan loan)
        {
            // TODO do checks for member as well. 
            if (loan.LoanNumber != 0)
            {
                return BadRequest();
            }

            if (!_db.Dvdcopies.Where(c => c.CopyNumber == loan.CopyNumber).Any())
            {
                return BadRequest($"DvD copy with id {loan.CopyNumber} does not exist");
            }

            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var loanType = loan.TypeNumberNavigation;
                if (loanType.LoanTypeNumber == 0)
                {
                    await _db.LoanTypes.AddAsync(loanType);
                    await _db.SaveChangesAsync();
                }

                loan.TypeNumberNavigation = null;
                loan.TypeNumber = loanType.LoanTypeNumber;

                _db.Loans.Add(loan);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IEnumerable<Loan> GetAllLoans() {
            return _db.Loans;
        }
    }
}
