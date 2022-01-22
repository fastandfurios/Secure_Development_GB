using Debit_Cards_No_EF_Project.DAL.Interfaces;
using Debit_Cards_No_EF_Project.DAL.Repositories;
using FluentMigrator.Runner;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IDebitCardRepository, DebitCardRepository>();
builder.Services.AddSingleton<IConnection, Connection>();

var service = builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(configure => configure.AddSQLite()
        .WithGlobalConnectionString("Data Source=Cards.db;Version=3;Pooling=true;Max Pool Size=100;")
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(configure => configure.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

var runner = service.CreateScope().ServiceProvider.GetRequiredService<IMigrationRunner>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

runner.MigrateUp();

app.Run();