using System.Text;
using Debit_Cards_Project.DAL.Context;
using Debit_Cards_Project.DAL.Interfaces;
using Debit_Cards_Project.DAL.Models.User.Login;
using Debit_Cards_Project.DAL.Repositories;
using Debit_Cards_Project.Domain;
using Debit_Cards_Project.Infrastructure.Security;
using Debit_Cards_Project.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMediatR(typeof(LoginHandler).Assembly);

builder.Services.AddScoped<IDebitCardRepository, DebitCardRepository>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

builder.Services.AddDbContext<DebitCardsDb>(op => 
    op.UseNpgsql(builder.Configuration.GetConnectionString("Cards")));

builder.Services.AddDbContext<UsersDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Users")));

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

builder.Services.AddMvc(option =>
{
    option.EnableEndpointRouting = false;
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    option.Filters.Add(new AuthorizeFilter(policy));
});

var identity_builder = builder.Services.AddIdentityCore<AppUser>();
var identityBuilder = new IdentityBuilder(identity_builder.UserType, identity_builder.Services);

identityBuilder.AddEntityFrameworkStores<UsersDb>();
identityBuilder.AddSignInManager<SignInManager<AppUser>>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]));
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(option =>
    {
        option.RequireHttpsMetadata = false;
        option.SaveToken = true;
        option.TokenValidationParameters = new()
        {
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = false,
            ClockSkew = TimeSpan.Zero,
        };
    });

#endregion

#region configuring Swagger/OpenAPI
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion


var app = builder.Build();

#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

#endregion

