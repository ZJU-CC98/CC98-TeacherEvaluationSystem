using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CC98.TeacherEvaluationSystem.Models;

namespace CC98.TeacherEvaluationSystem.Controllers
{
	public class HomeController : Controller
	{
		/// <summary>
		/// 日志记录程序。
		/// </summary>
		private ILogger<HomeController> Logger { get; }

		public HomeController(ILogger<HomeController> logger)
		{
			Logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
