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
            Email = user.Email,
            Id = user.Id,
            Displayname= user.DisplayName,
            TokenKey = tokenService.CreateToken(user)
        };
    }
}
