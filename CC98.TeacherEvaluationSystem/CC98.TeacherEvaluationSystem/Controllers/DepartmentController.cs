using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CC98.TeacherEvaluationSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;

namespace CC98.TeacherEvaluationSystem.Controllers
{
	/// <summary>
	/// 提供对部门的相关操作。
	/// </summary>
	public class DepartmentController : Controller
	{
		public DepartmentController(TesDbContext dbContext, IOperationMessageAccessor messageAccessor)
		{
			DbContext = dbContext;
			MessageAccessor = messageAccessor;
		}

		/// <summary>
		/// 数据库上下文对象。
		/// </summary>
		private TesDbContext DbContext { get; }

		/// <summary>
		/// 消息访问服务。
		/// </summary>
		private IOperationMessageAccessor MessageAccessor { get; }

		/// <summary>
		/// 显示部门列表。
		/// </summary>
		/// <param name="page">页码。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index(int page = 1, CancellationToken cancellationToken = default)
		{
			var items = from i in DbContext.Departments
						orderby i.Name
						select i;

			return View(await items.ToPagedListAsync(12, page, cancellationToken));
		}

		/// <summary>
		/// 显示管理页面。
		/// </summary>
		/// <param name="page">页码。</param>
		/// <returns>用于取消操作的令牌。</returns>
		[HttpGet]
		[Authorize(Policies.Edit)]
		public async Task<IActionResult> Manage(int page = 1, CancellationToken cancellationToken = default)
		{
			var items = from i in DbContext.Departments
						orderby i.Name
						select i;

			return View(await items.ToPagedListAsync(12, page, cancellationToken));
		}

		/// <summary>
		/// 显示创建部门界面。
		/// </summary>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[Authorize(Policies.Edit)]
		public IActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// 执行创建部门操作。
		/// </summary>
		/// <param name="model">数据模型。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[Authorize(Policies.Edit)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Department model, CancellationToken cancellationToken = default)
		{
			await CheckDepartmentRelationshipAsync(model, cancellationToken);

			if (ModelState.IsValid)
			{
				DbContext.Departments.Add(model);

				try
				{
					await DbContext.SaveChangesAsync(cancellationToken);
					MessageAccessor.Messages.Add(OperationMessageLevel.Success, "操作成功",
						string.Format(CultureInfo.CurrentUICulture, "你已经成功添加了部门 {0}", model.Name));

					return RedirectToAction("Index", "Department");

				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError(string.Empty, ex.GetBaseMessage());
				}
			}

			return View(model);
		}

		/// <summary>
		/// 显示部门编辑页面。
		/// </summary>
		/// <param name="id">要编辑的部门的标识。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		[Authorize(Policies.Edit)]
		public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
		{
			var item = await DbContext.Departments.FindAsync(new object[] { id }, cancellationToken);

			if (item == null)
			{
				return NotFound();
			}

			return View(item);
		}

		/// <summary>
		/// 执行编辑部门操作。
		/// </summary>
		/// <param name="model">数据模型。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpPost]
		[Authorize(Policies.Edit)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Department model, CancellationToken cancellationToken = default)
		{
			await CheckDepartmentRelationshipAsync(model, cancellationToken);

			if (ModelState.IsValid)
			{
				DbContext.Departments.Update(model);

				try
				{
					await DbContext.SaveChangesAsync(cancellationToken);
					MessageAccessor.Messages.Add(OperationMessageLevel.Success, "操作成功",
						string.Format(CultureInfo.CurrentUICulture, "你已经成功编辑了部门 {0}", model.Name));

					return RedirectToAction("Index", "Department");

				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError(string.Empty, ex.GetBaseMessage());
				}
			}

			return View(model);
		}

		/// <summary>
		/// 查看部门详细信息。
		/// </summary>
		/// <param name="id">部门标识。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		[HttpGet]
		public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken = default)
		{
			var item = await
				(from i in DbContext.Departments
				 where i.Id == id
				 select i)
					.Include(p => p.Children)
					.Include(p => p.Teachers)
					.SingleOrDefaultAsync(cancellationToken);

			if (item == null)
			{
				return NotFound();
			}

			return View(item);
		}

		/// <summary>
		/// 检查部门数据以确保不存在循环上级。
		/// </summary>
		/// <param name="model">数据模型。</param>
		/// <param name="cancellationToken">用于取消操作的令牌。</param>
		/// <returns>操作结果。</returns>
		private async Task CheckDepartmentRelationshipAsync(Department model, CancellationToken cancellationToken)
		{
			var currentId = model.ParentId;
			while (currentId != null)
			{
				if (currentId == model.Id)
				{
					ModelState.AddModelError(string.Empty, "部门之间存在循环上级关系。");
					return;
				}

				var parent = await DbContext.Departments.FindAsync(new object[] { currentId }, cancellationToken);
				currentId = parent.ParentId;
			}
		}
	}
}