using System.Security.Claims;
using API.Data;
using API.DTOs;
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
            Console.WriteLine(memberRespository.GetMembersAsync());
                        Console.WriteLine("===================================");
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

        [HttpPut]
        public async Task<ActionResult> UpdateMember(MemberUpdateDTO memberUpdateDTO)
        {
            var memberid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(memberid == null) return BadRequest("Oops! No id found in token");

            var member = await memberRespository.GetMemberForUpdate(memberid);

            if(member == null) return BadRequest("Could not get member");

            member.DisplayName= memberUpdateDTO.DisplayName ?? member.DisplayName;
            member.Description= memberUpdateDTO.Description ?? member.Description;
            member.City= memberUpdateDTO.City ?? member.City;
            member.Country= memberUpdateDTO.Country ?? member.Country;


    member.User.DisplayName = memberUpdateDTO.DisplayName ?? member.User.DisplayName;
    //  memberRespository.Update(member); // optional

     if(await memberRespository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Failed to Update Member");
  
        }
    }
}
