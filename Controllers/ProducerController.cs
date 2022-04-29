using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DvD_Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProducerController:ControllerBase
    {
        public readonly ApplicationDbContext _db;

        public ProducerController(ApplicationDbContext databse)
        {
            _db = databse;
        }

        [HttpGet]
        public IEnumerable<Producer> GetAllProducer() { 
            return _db.Producers;
        }
    }
}
