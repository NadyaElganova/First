
using First.Extensions;
using First.Options;
using First.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Console.WriteLine("Key :" + builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("ApiKey"));
//Console.WriteLine("BaseUrl :" + builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("BaseUrl"));

//Console.WriteLine("Key :" + builder.Configuration["ConnectionStrings:ApiKey"]);
//Console.WriteLine("BaseUrl :" + builder.Configuration["ConnectionStrings: BaseUrl"]);

builder.Services.AddMemoryCache();

builder.Services.AddMovieService(options =>
{
    options.ApiKey = builder.Configuration["ConnectionStrings:ApiKey"];
    options.BaseUrl = builder.Configuration["ConnectionStrings:BaseUrl"];
});

builder.Services.AddSingleton<IRecentMovieStorage, RecentMovieStorage>();

//builder.Services.AddSingleton(); //для неизменяемых объектов
//builder.Services.AddScoped(); //легковесный запрос (курс доллара...)

builder.Services.AddHttpClient();

//Extensions
//builder.Services.AddMovieService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
