using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace API.Controllers
{
 [Authorize]
    public class MembersController(IMemberRespository memberRespository) : BaseAPIController
    {
         
        [HttpGet] // localhost:5001/api/members
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            return Ok( await memberRespository.GetMembersAsync());
         

        }

        [Authorize]
        [HttpGet("{id}")] // localhost:5001/api/members/ram-id
        public async Task<ActionResult<Member>>  GetMemberbyId(string id)
        {
            var member = await memberRespository.GetMemberbyIdAsync(id) ;

            if(member == null) return NotFound();
            return member;
        }

        [HttpGet("{id}/Photos")]
        public async Task<ActionResult<IReadOnlyList<Photo>>> GetMemberPhoto(string id)
        {
            return Ok(await memberRespository.GetPhotosFromMemberAsync(id) );
        }
    }
}
