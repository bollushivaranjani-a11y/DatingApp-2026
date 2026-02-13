using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(AppDBContext appDBContext, ITokenService tokenService) : BaseAPIController
{
    [HttpPost( "register" )] // api/account/register
public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {

        if(await EmailExits(registerDTO.Email)) return BadRequest("Email already exists");
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            DisplayName = registerDTO.DisplayName,
            Email = registerDTO.Email,
           PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
           PasswordSalt = hmac.Key
        };

        appDBContext.Users.Add( user );

        await appDBContext.SaveChangesAsync();

          return user.userextn(tokenService);
    }

    private async Task<bool> EmailExits(string email)
    {
        return await appDBContext.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower() );

        
   }

[HttpPost("login")]
   public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
     var user = await appDBContext.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);
     
     if(user == null) return Unauthorized("Invalid emailid");

     using var hmac = new HMACSHA512(user.PasswordSalt);

     var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

     for(var i =0; i < computedhash.Length; i ++)
        {
            if(computedhash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Invalid Password");
            }
        }

   // return  AppUserExtension.userextn(user,tokenService);

   return user.userextn(tokenService);

    }
}

