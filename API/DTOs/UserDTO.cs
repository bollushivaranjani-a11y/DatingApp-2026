using System;

namespace API.DTOs;

public class UserDTO
{

public required string Id { get; set; }

public required string Email { get; set; }

public required string Displayname { get; set; }

public required string TokenKey { get; set; }

public string? ImageUrl  { get; set; }
}
