#region References
using Biblio.DAL.Interfaces;
using Biblio.DAL.Models.Book;
using Biblio.DAL.Repositories;
using Biblio.Mapping;
using FluentValidation;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
#endregion

var logger = LogManager.Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    #region Add services to the container.

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IBookRepository, BookRepository>();

    builder.Services.Configure<BookStoreDatabaseSettings>(builder.Configuration.GetSection("BooksDb"));

    builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

    builder.Services.AddScoped<IValidator<Book>, BookValidation>();

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
    builder.Host.UseNLog();

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
}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
