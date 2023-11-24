using Microsoft.EntityFrameworkCore;
using POE_TEST_ONE.Data;
using Microsoft.AspNetCore.Identity;
using POE_TEST_ONE.Areas.Identity.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

//Here we are saying we want to add entity framework core to the project
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<AuthDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDefaultIdentity<ApplicationUser>()
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddSignInManager<SignInManager<ApplicationUser>>();

//Add support for razor pages to the application
builder.Services.AddRazorPages();

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
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Module}/{action=Index}/{id?}");

app.MapRazorPages();



app.Run();
