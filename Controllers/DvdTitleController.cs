using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DvdTitleController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public DvdTitleController(ApplicationDbContext database)
        {
            _db = database;
        }


        [HttpPost]
        public async Task<IActionResult> AddDvdTitle(Dvdtitle dvdTitle)
        {
            if (dvdTitle.DvdNumber == 0)
            {

                using var transaction = _db.Database.BeginTransaction();

                try
                {
                    var dvdProducer = dvdTitle.ProducerNumberNavigation;
                    if (dvdProducer.ProducerNumber == 0)
                    {
                        await _db.Producers.AddAsync(dvdProducer);
                        await _db.SaveChangesAsync();
                    }

                    dvdTitle.ProducerNumberNavigation = null;
                    dvdTitle.ProducerNumber = dvdProducer.ProducerNumber;

                    var dvdStudio = dvdTitle.StudioNumberNavigation;

                    if (dvdStudio.StudioNumber == 0)
                    {
                        await _db.Studios.AddAsync(dvdStudio);
                        await _db.SaveChangesAsync();
                    }

                    dvdTitle.StudioNumber = dvdStudio.StudioNumber;
                    dvdTitle.StudioNumberNavigation = null;

                    var dvdCategory = dvdTitle.CategoryNumberNavigation;
                    if (dvdCategory.CategoryNumber == 0)
                    {
                        await _db.Dvdcategories.AddAsync(dvdCategory);
                        await _db.SaveChangesAsync();
                    }

                    dvdTitle.CategoryNumber = dvdCategory.CategoryNumber;
                    dvdTitle.CategoryNumberNavigation = null;

                    var actorsList = dvdTitle.ActorNumbers;
                    var actorIdList = new List<int>();

                    foreach (var actor in actorsList)
                    {
                        var mActor = actor;
                        if (mActor.ActorNumber == 0)
                        {
                            await _db.Actors.AddAsync(mActor);
                            await _db.SaveChangesAsync();
                        }
                        actorIdList.Add(mActor.ActorNumber);
                    }

                    dvdTitle.ActorNumbers = _db.Actors.Where(a => actorIdList.Contains(a.ActorNumber)).ToList();

                    _db.Dvdtitles.Add(dvdTitle);

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok($"Added new DvD with id {dvdTitle.DvdNumber}");
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Could not add DvD. Contact Admin!");

                }
            }

            return BadRequest();
        }


        [HttpGet]
        public List<Dvdtitle> GetAllDvd() { 
            return _db.Dvdtitles
                .Include(d => d.DvDimages)
                .Include(d => d.ActorNumbers)
                .Include(d => d.CategoryNumberNavigation)
                .Include(d => d.ProducerNumberNavigation)
                .Include(d => d.StudioNumberNavigation).ToList();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDvd(int id) {
            var dvdExists = await _db.Dvdtitles.FirstOrDefaultAsync(d => d.DvdNumber == id);
            if (dvdExists == null) {
                return NotFound();
            }

            _db.Dvdtitles.Remove(dvdExists);
            await _db.SaveChangesAsync();

            return Ok();
        }

    }
}
