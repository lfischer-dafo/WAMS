using WAMS.Backend.Data;
using WAMS.Components;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using WAMS.Components.Model;
using System;

namespace WAMS
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         // Add services to the container.
         builder.Services.AddRazorComponents()
             .AddInteractiveServerComponents();

         string? connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
         builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
         builder.Services.AddCascadingAuthenticationState();
         //builder.Services.AddScoped<IdentityUserAccessor>();
         builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAutheticationStateProvider>();
         builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
            options.SignIn.RequireConfirmedAccount = false;
         }).AddDefaultTokenProviders();
         builder.Services.AddAuthentication(options =>
         {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
         }).AddIdentityCookies();
         builder.Services.AddCascadingAuthenticationState();

         var app = builder.Build();

         // Configure the HTTP request pipeline.
         if (!app.Environment.IsDevelopment())
         {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseHttpsRedirection();

         app.UseStaticFiles();
         app.UseAntiforgery();

         app.MapRazorComponents<App>()
             .AddInteractiveServerRenderMode();

         app.Run();
      }
   }
}
