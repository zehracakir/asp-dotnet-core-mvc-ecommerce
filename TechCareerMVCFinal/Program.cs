using Microsoft.EntityFrameworkCore;
using TechCareerMVCFinal.Data;
using TechCareerMVCFinal.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DefaultConnection'ı bulup bakacak
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));

// _kiyafetTuruRepository nesnesini burda olusturduk --> Dependency injenction
builder.Services.AddScoped<IKiyafetTuruRepository, KiyafetTuruRepository>();


// _kiyafetRepository nesnesini burda olusturduk --> Dependency injenction
builder.Services.AddScoped<IKiyafetRepository, KiyafetRepository>();

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
// pipeline i  burasi.
