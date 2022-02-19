#region references
using Elasticsearch.Net;
using Nest;
#endregion


var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(GetClient());

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#region methods

static ElasticClient GetClient()
{
    var pool = new SingleNodeConnectionPool(new("http://localhost:9200"));
    var settings = new ConnectionSettings(pool).DefaultIndex("books");
    return new(settings);
}

#endregion