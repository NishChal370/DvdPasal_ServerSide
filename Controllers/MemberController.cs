using DvD_Api.Data;
using DvD_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DvD_Api.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MemberController(ApplicationDbContext database)
        {
            _db = database;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(Member member)
        {
            // Only add new member. 
            if (member.MemberNumber == 0)
            {
                using var transaction = _db.Database.BeginTransaction();
                // Add both member and category or none.
                try
                {
                    var membershipCatagory = member.CategoryNumberNavigation;
                    if (membershipCatagory.McategoryNumber == 0)
                    {
                        // Add the category first and get a new id.
                        await _db.MembershipCategories.AddAsync(membershipCatagory);
                        await _db.SaveChangesAsync();
                    }

                    // No need to insert the category again.
                    member.CategoryNumberNavigation = null;

                    member.CategoryNumber = membershipCatagory.McategoryNumber;

                    await _db.Members.AddAsync(member);

                    await _db.SaveChangesAsync();
                    transaction.Commit();

                    return Ok($"Added new member with id {member.MemberNumber}");
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Could not add member. Contact Admin!");
                }
            }

            return BadRequest();
        }

        [HttpGet]
        public IEnumerable<Member> GetAllMembers() {
            return _db.Members.Include(m => m.CategoryNumberNavigation); 
        }

        [HttpGet("{memberId}")]
        public async Task<Member> GetMember(int memberId) {
            return await _db.Members
                .Include(m => m.CategoryNumberNavigation)
                .FirstOrDefaultAsync(m => m.MemberNumber == memberId);
        }

        [HttpGet("search/{lastName}")]
        public async Task<List<Member>> GetMemberByLastName(string lastName) {

            // TODO replace with 'like' rather than direct comparison. 
            return await _db.Members
                .Include(m => m.CategoryNumberNavigation)
                .Where(m => m.LastName == lastName)
                .ToListAsync();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMember(int memberId, Member member) {
            if (memberId != member.MemberNumber)
            {
                return BadRequest();
            }
            var memberExists = _db.Members.Where(m => m.MemberNumber == memberId).Any();
            if (memberExists) {
                _db.Members.Update(member);
                await _db.SaveChangesAsync();

                return Ok();

            }

            return NotFound($"Member with id {memberId} not found!");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMember(int memberId) { 
            var memberExists = await _db.Members.Where(m => m.MemberNumber == memberId).FirstOrDefaultAsync();
            if (memberExists == null)
            {
                return NotFound($"Member with id {memberId} not found!");
            }

            _db.Members.Remove(memberExists);
            await _db.SaveChangesAsync();

            return Ok();

        }

    }
}
