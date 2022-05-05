using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class StudioController : ControllerBase
    {
        public readonly ApplicationDbContext _db;

        public StudioController(ApplicationDbContext databse)
        {
            _db = databse;
        }

        [HttpGet]
        public IEnumerable<Studio> GetAllStudio()
        {
            return _db.Studios;
        }
    }
}
