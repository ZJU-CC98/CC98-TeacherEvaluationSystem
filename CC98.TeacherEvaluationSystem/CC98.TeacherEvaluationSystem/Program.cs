using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CC98.TeacherEvaluationSystem
{
	/// <summary>
	/// 应用程序的主类型。
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// 应用程序的入口点方法。
		/// </summary>
		/// <param name="args">应用程序的启动参数。</param>
		[UsedImplicitly]
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// 创建应用程序宿主。
		/// </summary>
		/// <param name="args">应用程序的启动参数。</param>
		/// <returns>用于承载应用程序的 <see cref="IHostBuilder"/> 类型对象。</returns>
		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
