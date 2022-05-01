using DvD_Api.Data;
using DvD_Api.DTO;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DvdCopyController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public DvdCopyController(ApplicationDbContext database)
        {
            _db = database;
        }

        [HttpPost]
        public async Task<IActionResult> AddCopy(CopyDto dvdCopy)
        {

            if (_db.Dvdtitles.FirstOrDefault(d => d.DvdNumber == dvdCopy.DvdId) == null)
            {
                return BadRequest($"Dvd with id {dvdCopy.DvdId} does not exist");
            }

            for (int i = 0; i < dvdCopy.DvdCount; i++)
            {

                var mCopy = new Dvdcopy
                {
                    CopyNumber = 0,
                    Dvdnumber = dvdCopy.DvdId,
                    DatePurchased = dvdCopy.DatePurchased,
                };

                await _db.Dvdcopies.AddAsync(mCopy);
            }
            
            await _db.SaveChangesAsync();
            return Ok($"Added {dvdCopy.DvdCount} copies of dvd id {dvdCopy.DvdId}.");
        }

        [HttpGet]
        public IEnumerable<object> GetAllCopy()
        {
            return _db.Dvdcopies
                .Include(c => c.DvdnumberNavigation)
                .Select(c => new
                {
                    copyId = c.CopyNumber,
                    datePurchased = c.DatePurchased.ToString("d"),
                    dvdId = c.Dvdnumber,
                    dvdTitle = c.DvdnumberNavigation.DvdName

                });
        }



        [HttpGet("available")]
        public IEnumerable<Dvdcopy> GetAvailableCopy()
        {
            return _db.Dvdcopies
                .Include(c => c.Loans)
                .Where(c => c.Loans.Count() < 1 || c.Loans.All(l => l.DateReturned != null));
        }


        [HttpGet("old")]
        public IEnumerable<Dvdcopy> GetOldCopies()
        {
            return _db.Dvdcopies.Where(c => c.DatePurchased.AddDays(365) < DateTime.Now);
        }

        [HttpGet("{copyId}")]
        public object GetCopyById(int copyId)
        {
            var mCopy = _db.Dvdcopies.FirstOrDefault(c => c.CopyNumber == copyId);
            if (mCopy == null)
            {
                return NoContent();
            }

            return new
            {
                copyId = mCopy.CopyNumber,
                datePurchased = mCopy.DatePurchased.ToString("d"),
                dvdId = mCopy.Dvdnumber,
                dvdTitle = mCopy.DvdnumberNavigation.DvdName
            };
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCopy(int copyId)
        {
            var copyExists = _db.Dvdcopies.FirstOrDefault(c => c.CopyNumber == copyId);

            if (copyExists == null)
            {
                return NotFound();
            }

            _db.Dvdcopies.Remove(copyExists);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("forLoan")]
        public IEnumerable<object> GetCopyForLoan() { 
            return _db.Dvdcopies.Include(c => c.DvdnumberNavigation).ThenInclude(c => c.DvDimages)
                .Include(c => c.DvdnumberNavigation.CategoryNumberNavigation).Include(c => c.Loans)
                .Where(c => c.Loans.Count() < 1 || c.Loans.All(l => l.DateReturned != null)).ToList()
                .DistinctBy(c => c.Dvdnumber).Select(c => new { 
                    DvdTitle = c.DvdnumberNavigation.DvdName,
                    Price = c.DvdnumberNavigation.StandardCharge,
                    AgeRestricted = c.DvdnumberNavigation.CategoryNumberNavigation.AgeRestricted,
                    Category = c.DvdnumberNavigation.CategoryNumberNavigation.CategoryDescription,
                    CopyId = c.CopyNumber,
                    DvdImage = c.DvdnumberNavigation.DvDimages.FirstOrDefault().Image64
                });
        }


        [HttpGet("lastLoan/{copyId}")]
        public object GetLastLoan(int copyId)
        {
            var lastLoan = _db.Loans
                .Include(l => l.MemberNumberNavigation)
                .Include(l => l.CopyNumberNavigation.DvdnumberNavigation)
                .OrderBy(l => l.DateOut).Where(l => l.CopyNumber == copyId)
                .LastOrDefault();

            return lastLoan == null ? lastLoan : new { 
                DvDTitle = lastLoan.CopyNumberNavigation.DvdnumberNavigation.DvdName,
                DvDId = lastLoan.CopyNumberNavigation.Dvdnumber,
                LoanedBy = $"{lastLoan.MemberNumberNavigation.FirstName} {lastLoan.MemberNumberNavigation.LastName}",
                DateOut = lastLoan.DateOut.ToString("d"),
                DateDue = lastLoan.DateDue.ToString("d"),
                DateReturned = lastLoan.DateReturned != null ? lastLoan.DateReturned.Value.ToString("d") : "Not Returned",
            };
        }

    }
}
