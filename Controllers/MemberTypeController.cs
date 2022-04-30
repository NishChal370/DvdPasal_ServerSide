using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MemberTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MemberTypeController(ApplicationDbContext database)
        {
            _db = database;
        }

        [HttpGet]
        public IEnumerable<MembershipCategory> GetAllCategory() {
            return _db.MembershipCategories;
        }
    }
}
