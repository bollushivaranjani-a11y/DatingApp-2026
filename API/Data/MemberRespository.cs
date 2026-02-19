using System;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MemberRespository(AppDBContext context) :  IMemberRespository 
{
    public async Task<Member?> GetMemberbyIdAsync(string id)
    {
      return await context.Members.FindAsync(id);
    }

    

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    { 
         return await context.Members
         .ToListAsync();
    }
 
    public async Task<IReadOnlyList<Photo>> GetPhotosFromMemberAsync(string memberId)
    {
       return await context.Members
       .Where(x => x.Id == memberId)
       .SelectMany(x => x.LstPhotos)
       .ToListAsync();
    }

   
    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }



    public void Update(Member member)
    {
        context.Entry(member).State = EntityState.Modified;
    }

    
}
