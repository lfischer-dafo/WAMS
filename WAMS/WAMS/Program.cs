using WAMS.Backend.Data;
using WAMS.Components;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using WAMS.Components.Model;
using System;
using WAMS.Backend.Constants;

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
			builder.Services.AddAuthorization(config =>
			{
				foreach (var userPolicy in UserPolicy.GetPolicies()) {
					config.AddPolicy(userPolicy, cfg => cfg.RequireClaim(userPolicy, "true"));
				}
			});
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.Cookie.Name = "auth_token";
				options.LoginPath = "/";
				options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
			});

			builder.Services.AddCascadingAuthenticationState();

			builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

			string? connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
			builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

			builder.Services.AddControllers(); // Registriere die API-Controller

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment()) {
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseAntiforgery();

			app.MapControllers();  // Add this line to map API controllers

			app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
			app.Run();
		}

	}
}
