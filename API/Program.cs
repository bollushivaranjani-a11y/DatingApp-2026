using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddDbContext<AppDBContext>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// register service then add it in middleware
builder.Services.AddCors();
// angular app is on localhost:4200 and API is on localhost:5001 to allow 
//cross origin add this service

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
              var TokenKey = builder.Configuration["TokenKey"] ??
            throw new Exception ("TokenKey not found - program.cs") ;   
            options.TokenValidationParameters = new  TokenValidationParameters
            {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey)),
              ValidateIssuer=   false,
              ValidateAudience = false  
            };
    } );

var app = builder.Build();

// Configure the HTTP request pipeline. 

app.UseCors( x => x.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200","https://localhost:4200") );

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
