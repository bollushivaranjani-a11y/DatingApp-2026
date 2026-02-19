using System;
using API.Entities;

namespace API.Interfaces;

public interface IMemberRespository
{
    void Update(Member  member);
    
    Task<bool> SaveAllAsync();

    Task<IReadOnlyList<Member>> GetMembersAsync();

    Task<Member?> GetMemberbyIdAsync( string id);

    Task<IReadOnlyList<Photo>> GetPhotosFromMemberAsync(string memberId);


}
