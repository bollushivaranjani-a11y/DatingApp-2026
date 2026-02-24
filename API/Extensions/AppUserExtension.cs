using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtension
{
 public static UserDTO userextn (this AppUser user , ITokenService tokenService )
    {
        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            Displayname= user.DisplayName,
            ImageUrl = user.ImageUrl,
            TokenKey = tokenService.CreateToken(user)       
      
        };

        
    }
}
