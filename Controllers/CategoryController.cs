using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class DvdCategoryController : ControllerBase
    {
        public readonly ApplicationDbContext _db;

        public DvdCategoryController(ApplicationDbContext databse)
        {
            _db = databse;
        }

        [HttpGet]
        public IEnumerable<Dvdcategory> GetAllDvdcategory()
        {
            return _db.Dvdcategories;
        }
    }
}
