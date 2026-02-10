using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDBContext appDBContext) : ControllerBase
    {
        [HttpGet] // localhost:5001/api/members
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var  members = await  appDBContext.Users.ToListAsync();
            return members;

        }

        [HttpGet("{id}")] // localhost:5001/api/members/ram-id
        public async Task<ActionResult<AppUser>>  GetMemberbyId(string id)
        {
            var member = await appDBContext.Users.FindAsync(id) ;

            if(member == null) return NotFound();
            return member;
        }
    }
}
