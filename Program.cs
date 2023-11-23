using Microsoft.EntityFrameworkCore;
using SmartHome.Models.Entity;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// thiet lapdich vu sql
var connectString = builder.Configuration.GetConnectionString("AppDb"); // chuuoi ket noi

builder.Services.AddDbContext<SmartHomeContext>(o =>
{
    o.UseSqlServer(connectString).LogTo(Console.WriteLine, LogLevel.None);
});
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
