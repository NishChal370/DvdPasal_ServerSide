using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddCopy(Dvdcopy dvdCopy)
        {
            if (dvdCopy.CopyNumber != 0)
            {
                return BadRequest();
            }

            if (_db.Dvdtitles.FirstOrDefault(d => d.DvdNumber == dvdCopy.Dvdnumber) == null)
            {
                return BadRequest($"Dvd with id {dvdCopy.Dvdnumber} does not exist");
            }

            await _db.Dvdcopies.AddAsync(dvdCopy);
            await _db.SaveChangesAsync();
            return Ok($"Added new copy with id {dvdCopy.CopyNumber}");
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
                    dvdTitle = c.DvdnumberNavigation.DvdName
                    
                });
        }

        [HttpGet("{copyId}")]
        public object GetCopyById(int copyId)
        {
            var mCopy = _db.Dvdcopies.FirstOrDefault(c => c.CopyNumber == copyId);
            if (mCopy == null) { 
                return NoContent();
            }

            return new {
                copyId = mCopy.CopyNumber,
                datePurchased = mCopy.DatePurchased.ToString("d"),
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
    }
}
