#region references
using Consul;
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
builder.Services.AddHostedService<LaunchService>();
builder.Services.AddHealthChecks();
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

app.UseHealthChecks("/healthz");

app.Run();
#endregion