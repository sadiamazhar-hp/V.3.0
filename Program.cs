using Microsoft.EntityFrameworkCore;
using V._3._0.App_Data;
using V._3._0.Controllers;
using V._3._0.Interfaces;
using V._3._0.Methods;
using V._3._0.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddScoped<IPatients, PatientData>();


//builder.Services.AddScoped<IHosData, HosData>();
builder.Services.AddDbContext<HospitalData>
    (options => options.UseSqlServer("Data Source=HAFIZMUHAMMADHA\\SQLEXPRESS;Initial Catalog=V.3.0;Integrated Security=True;TrustServerCertificate=True"));
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
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=login}/{id?}");

app.Run();
