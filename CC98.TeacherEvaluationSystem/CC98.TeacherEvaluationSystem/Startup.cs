using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CC98.Authentication.OpenIdConnect;
using CC98.TeacherEvaluationSystem.Data;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CC98.TeacherEvaluationSystem
{
	/// <summary>
	/// Ӧ�ó�����������͡�
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// ��ʼ�� <see cref="Startup"/> ���͵���ʵ����
		/// </summary>
		/// <param name="configuration">Ӧ�ó���������Ϣ��</param>
		[UsedImplicitly]
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Ӧ�ó���������Ϣ��
		/// </summary>
		private IConfiguration Configuration { get; }

		/// <summary>
		/// ������Ӧ�ó������
		/// </summary>
		/// <param name="services">Ӧ�ó������������</param>
		[UsedImplicitly]
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<TesDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("Tes"));
			});

			services.AddControllersWithViews();

			services.AddExternalSignInManager();
			services.AddAuthentication(IdentityConstants.ApplicationScheme)
				.AddCookie(IdentityConstants.ApplicationScheme, options =>
				{
					options.LoginPath = "/Account/LogOn";
					options.LogoutPath = "/Account/LogOff";
					options.AccessDeniedPath = "/Home/AccessDenied";

					options.Cookie.HttpOnly = true;
					options.Cookie.SecurePolicy = CookieSecurePolicy.None;
					options.Cookie.SameSite = SameSiteMode.Lax;
				})
				.AddCookie(IdentityConstants.ExternalScheme)
				.AddCC98(CC98Defaults.AuthenticationScheme, options =>
				{
					options.ClientId = Configuration["Authentication:CC98:ClientId"];
					options.ClientSecret = Configuration["Authentication:CC98:ClientSecret"];
					options.CallbackPath = "/signin-cc98";
					options.Scope.Add(OpenIdConnectScope.OpenIdProfile);
					options.ResponseType = OpenIdConnectResponseType.CodeIdTokenToken;
					options.SignInScheme = IdentityConstants.ExternalScheme;
				});

			services.AddSession();
			services.AddEnhancedTempData();
			services.AddOperationMessages();
		}

		/// <summary>
		/// ����Ӧ�ó����ܡ�
		/// </summary>
		/// <param name="app">Ӧ�ó������</param>
		/// <param name="env">������������</param>
		[UsedImplicitly]
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseSession();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
