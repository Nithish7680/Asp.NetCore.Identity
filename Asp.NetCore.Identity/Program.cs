using Asp.NetCore.Identity.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme) 
    .AddBearerToken(IdentityConstants.BearerScheme); 

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>() 
    .AddDefaultTokenProviders() 
    .AddApiEndpoints(); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();
app.UseAuthentication();
app.UseAuthorization();

//app.MapGet("/", () => "Hello World!");

app.Run();
