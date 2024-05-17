using Microsoft.EntityFrameworkCore;
using TechCareerMVCFinal.Data;
using TechCareerMVCFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DefaultConnection'ı bulup bakacak
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));

// AddDefaultTokenProvideeer() email kaynakli hata vermesin dyie sonuna ekledim 
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); ;

//Scoffold Identity icin Razor page leri kullanacagim.
builder.Services.AddRazorPages();

//Scoped Injenction
// _kiyafetTuruRepository nesnesini burda olusturduk --> Dependency injenction
builder.Services.AddScoped<IKiyafetTuruRepository, KiyafetTuruRepository>();


// _kiyafetRepository nesnesini burda olusturduk --> Dependency injenction
builder.Services.AddScoped<IKiyafetRepository, KiyafetRepository>();

//_siparisVermeRepository nesnesini burda olusturduk --> Dependency injenction
builder.Services.AddScoped<ISiparisVermeRepository, SiparisVermeRepository>();

//_emailSender gerektiginde alinsin kullanilsin diye Dependemcy Injecmtion mekanizmama ekledim
builder.Services.AddScoped<IEmailSender, EmailSender>();

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
// app i razor page map edecegimi soyledim
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
// pipeline i  burasi.
