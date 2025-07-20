using Asp.NetCore.Identity.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var key = builder.Configuration["Jwt:Key"] ?? "super_secret_key_12345";

builder.Services.AddAuthorization();
//builder.Services.AddAuthentication()
//    .AddCookie(IdentityConstants.ApplicationScheme) 
//    .AddBearerToken(IdentityConstants.BearerScheme); 

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});


builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>() 
    .AddDefaultTokenProviders() 
    .AddSignInManager();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<User>();
app.UseAuthentication();
app.UseAuthorization();

//app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
