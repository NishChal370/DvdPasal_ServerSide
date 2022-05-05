using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class ActorController : ControllerBase
    {
        public readonly ApplicationDbContext _db;

        public ActorController(ApplicationDbContext databse)
        {
            _db = databse;
        }

        [HttpGet]
        public IEnumerable<object> GetAllActor()
        {
            return _db.Actors.Select(a => new {
                ActorId = a.ActorNumber,
                ActorName = $"{a.ActorName} {a.ActorLastName}"
            });
        }
    }
}
