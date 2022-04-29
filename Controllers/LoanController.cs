using DvD_Api.Data;
using DvD_Api.DTO;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoanController : ControllerBase
    {
        public readonly ApplicationDbContext _db;

        public LoanController(ApplicationDbContext database)
        {
            _db = database;
        }

        [HttpPost]
        public async Task<IActionResult> AddLoan(LoanDto loan)
        {

            if (!_db.Dvdcopies.Where(c => c.CopyNumber == loan.CopyNumber).Any())
            {
                return BadRequest($"DvD copy with id {loan.CopyNumber} does not exist");
            }

            var dvdTitle = _db.Dvdcopies
                    .Include(c => c.DvdnumberNavigation)
                    .Include(c => c.DvdnumberNavigation.CategoryNumberNavigation)
                    .Where(c => c.CopyNumber == loan.CopyNumber)
                    .First()
                    .DvdnumberNavigation;

            // Check if the DvD copy is already in loan
            var inLoanCopy = _db.Loans.Where(l => l.CopyNumber == loan.CopyNumber && l.DateReturned == null).FirstOrDefault();
            if (inLoanCopy != null)
            {
                return BadRequest($"DvD copy with id {loan.CopyNumber} already in loan");
            }

            var member = _db.Members.Where(m => m.MemberNumber == loan.MemberNumber).FirstOrDefault();

            if (member == null)
            {
                return BadRequest($"Member with id {loan.MemberNumber} does not exist");
            }

            if (dvdTitle.CategoryNumberNavigation.AgeRestricted && !member.IsOldEnough()) {
                return BadRequest($"Member with id {member.MemberNumber} is too young for this movie.");
            }

            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var loanType = loan.LoanType;
                if (loanType.LoanTypeNumber == 0)
                {
                    await _db.LoanTypes.AddAsync(loanType);
                    await _db.SaveChangesAsync();
                }
                else {
                    loanType = _db.LoanTypes.Where(t => t.LoanTypeNumber == loanType.LoanTypeNumber).FirstOrDefault();
                    if (loanType == null)
                    {
                        return BadRequest($"Loan Type with id {loanType.LoanTypeNumber} does not exist.");
                    }
                }

                var mLoan = new Loan
                {
                    LoanNumber = 0,
                    CopyNumber = loan.CopyNumber,
                    MemberNumber = loan.MemberNumber,
                    DateOut = loan.DateOut,
                    DateDue = loan.DateOut.AddDays(loanType.Duration),
                    TypeNumber = loanType.LoanTypeNumber
                };

                _db.Loans.Add(mLoan);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    LoanId = mLoan.LoanNumber,
                    DvdTitle = dvdTitle.DvdName,
                    StandardPrice = dvdTitle.StandardCharge,
                    MemberName = $"{member.FirstName} {member.LastName}",
                    DateOut = loan.DateOut,
                    DateDue = mLoan.DateDue,
                    LoanType = loanType.LoanTypeName
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IEnumerable<Loan> GetAllLoans()
        {
            return _db.Loans;
        }

        [HttpGet("notReturned")]
        public IEnumerable<object> GetNotReturnedLoansAsync()
        {
            return _db.Loans
                .Include(l => l.CopyNumberNavigation.DvdnumberNavigation)
                .Include(l => l.MemberNumberNavigation)
                .Include(l => l.TypeNumberNavigation)
                .Where(l => l.DateReturned == null)
                .Select(x => new
                {
                    LoanId = x.LoanNumber,
                    copyId = x.CopyNumber,
                    DvdTitle = x.CopyNumberNavigation.DvdnumberNavigation.DvdName,
                    MemberName = $"{x.MemberNumberNavigation.FirstName} {x.MemberNumberNavigation.LastName}",
                    DateOut = x.DateOut,
                    DateDue = x.DateDue,
                    LoanType = x.TypeNumberNavigation.LoanTypeName
                });
        }

        [HttpDelete]
        public IActionResult DeleteLoan(int loanId) { 
            var loanExists = _db.Loans.Where(l => l.LoanNumber == loanId).FirstOrDefault();
            if (loanExists == null) { 
                return NotFound();
            }

            _db.Loans.Remove(loanExists);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost("return/{loanId}")]
        public async Task<object> ReturnDvdCopy(int loanId)
        {
            var mLoan = _db.Loans
                .Include(l => l.CopyNumberNavigation)
                .Include(l => l.CopyNumberNavigation.DvdnumberNavigation)
                .Include(l => l.TypeNumberNavigation)
                .Include(l => l.MemberNumberNavigation)
                .FirstOrDefault(l => l.LoanNumber == loanId);

            if (mLoan == null)
            {
                return NotFound($"Loan with id {loanId} not found");
            }

            if (mLoan.DateReturned != null)
            {
                return BadRequest($"Dvd copy with id {mLoan.CopyNumber} already returned.");
            }

            mLoan.DateReturned = DateTime.Now;

            var daysOverDue = (DateTime.Now - mLoan.DateDue).Days;

            var penaltyTotal = 0m;
            if (daysOverDue > 0)
            {
                penaltyTotal = daysOverDue * mLoan.CopyNumberNavigation.DvdnumberNavigation.PenaltyCharge ?? 0;
            }

            var originalCharge = mLoan.CopyNumberNavigation.DvdnumberNavigation.StandardCharge;
            var totalCharge = penaltyTotal + originalCharge;

            _db.Loans.Update(mLoan);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                dvdTitle = mLoan.CopyNumberNavigation.DvdnumberNavigation.DvdName,
                memberName = $"{mLoan.MemberNumberNavigation.FirstName} {mLoan.MemberNumberNavigation.LastName}",
                dateLoaned = mLoan.DateOut.ToString("d"),
                dateDue = mLoan.DateDue.ToString("d"),
                dateReturned = mLoan.DateReturned.Value.ToString("d"),
                penaltyAmount = penaltyTotal,
                standardCharge = mLoan.CopyNumberNavigation.DvdnumberNavigation.StandardCharge,
                totalCharge = totalCharge,
            });
        }



        [HttpPost("searchMember")]
        public IEnumerable<Loan> SearchMembers(MemberSearchDto memberSearch)
        {
            return _db.Loans.Include(l => l.MemberNumberNavigation)
                .Where(l => memberSearch.IsLastName ? l.MemberNumberNavigation.LastName == memberSearch.SearchTerm              // Search by last name
                : l.MemberNumber.ToString() == memberSearch.SearchTerm)                                                        // Search by member Id
                .Where(l => l.DateOut.AddDays(31) >= DateTime.Now);
        }
    }
}
