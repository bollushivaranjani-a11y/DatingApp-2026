using System;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Photo
{
    public int Id { get; set; }

    public required string URL { get; set; } = "";

    public string? PublicId { get; set; }
     
// Navigation 1 memeber can have maany photos
[JsonIgnore]
     public Member Member { get; set; } = null!;

     public string MemberId { get; set; } = null!;
}
