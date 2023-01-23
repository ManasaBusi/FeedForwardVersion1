using FeedForwardRepository.Abstract;
using FeedForwardRepository.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IFeedback_Repository, Feedback_repository>();
builder.Services.AddSingleton<IAuth_Repository, Auth_repository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "/LoginActivity/LoginPage";
                    option.AccessDeniedPath = "/LoginActivity/LoginPage";
                });

builder.Services.AddSession(option => { option.IdleTimeout = TimeSpan.FromSeconds(120); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
//pattern: "{controller=UserDetail}/{action=UserRegistration}/{id?}");
//pattern: "{controller=UserDetail}/{action=LoginPage}/{id?}");
pattern: "{controller=UserDetail}/{action=LoginPage}/{id?}");

app.Run();
