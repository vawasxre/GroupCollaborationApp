using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GroupCollabApp.Data;
using GroupCollabApp.Areas.Identity.Data;
using MudBlazor.Services;
using MudBlazor;
using GroupCollabApp.Views.Shared;
using GroupCollabApp.Models;
using GroupCollabApp.Models.Hubs;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var connectionString = builder.Configuration.GetConnectionString("GroupCollabAppAuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'GroupCollabAppAuthDbContextConnection' not found.");

builder.Services.AddDbContext<GroupCollabAppAuthDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<GroupCollabAppAuthDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "1045134047102-sq14m9dpf1irk0a0jvb6uqfioqg0ge0v.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
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

app.MapRazorPages();

app.MapHub<ChatHub>("/chatHub");




app.Run();
