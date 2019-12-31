using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CC98.TeacherEvaluationSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;

namespace CC98.TeacherEvaluationSystem.Controllers
{
	/// <summary>
	/// 提供教师相关功能。
	/// </summary>
	public class TeacherController : Controller
	{
		public TeacherController(TesDbContext dbContext, IOperationMessageAccessor messageAccessor)
		{
			DbContext = dbContext;
			MessageAccessor = messageAccessor;
		}

		/// <summary>
		/// 数据上下文对象。
		/// </summary>
		private TesDbContext DbContext { get; }

		private IOperationMessageAccessor MessageAccessor { get; }

		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// 显示创建页面。
		/// </summary>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[Authorize(Policies.Edit)]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Policies.Edit)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Teacher model)
		{
			HandleModel();

			if (ModelState.IsValid)
			{
				DbContext.Teachers.Add(model);

				try
				{
					await DbContext.SaveChangesAsync();
					MessageAccessor.Messages.Add(OperationMessageLevel.Success, "操作成功",
						string.Format(CultureInfo.CurrentUICulture, "你已经添加了教师 {0}", model.Name));
					return RedirectToAction("Manage", "Teacher");
				}
				catch (DbUpdateException ex)
				{
					Console.WriteLine(ex);
					throw;
				}
			}

			return View(model);
		}

		public IActionResult Edit()
		{
			return View();
		}


		/// <summary>
		/// 处理模型以去掉不必要的数据检查。
		/// </summary>
		private void HandleModel()
		{
			ModelState.Remove(nameof(Teacher.Department));
		}
	}
}