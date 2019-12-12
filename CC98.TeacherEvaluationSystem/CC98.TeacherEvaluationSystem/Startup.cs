using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
			services.AddControllersWithViews();
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
