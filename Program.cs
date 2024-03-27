using API_Mail.Extend;
using Microsoft.EntityFrameworkCore;
using SmartHome.Models.Entity;
using System;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// mail
// mail
builder.Services.AddOptions();                                         // Kích hoạt Options
var mailsettings = builder.Configuration.GetSection("MailSettings");  // đọc config
builder.Services.Configure<MailSettings>(mailsettings);
builder.Services.AddTransient<ISendMailService, SendMailService>();
builder.Services.AddSession();
// thiet lapdich vu sql
var connectString = builder.Configuration.GetConnectionString("AppDb"); // chuuoi ket noi
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<SmartHomeContext>(o =>
{
    o.UseSqlServer(connectString).LogTo(Console.WriteLine, LogLevel.None);
});
var app = builder.Build();
app.UseSession();
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
