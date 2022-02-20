#region references
using Consul;
using Microsoft.AspNetCore.Hosting.Server.Features;
using ServiceA.Services;

#endregion

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConsulClient, ConsulClient>(serviceProvider => new(configOverride =>
{
    configOverride.Address = new(builder.Configuration.GetConnectionString("Consul"));
}));
builder.Services.AddSingleton<IHostedService, LaunchService>();
#endregion

var app = builder.Build();

#region Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion