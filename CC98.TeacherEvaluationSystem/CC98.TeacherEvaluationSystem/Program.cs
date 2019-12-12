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
	/// Ӧ�ó���������͡�
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// Ӧ�ó������ڵ㷽����
		/// </summary>
		/// <param name="args">Ӧ�ó��������������</param>
		[UsedImplicitly]
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		/// <summary>
		/// ����Ӧ�ó���������
		/// </summary>
		/// <param name="args">Ӧ�ó��������������</param>
		/// <returns>���ڳ���Ӧ�ó���� <see cref="IHostBuilder"/> ���Ͷ���</returns>
		private static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
