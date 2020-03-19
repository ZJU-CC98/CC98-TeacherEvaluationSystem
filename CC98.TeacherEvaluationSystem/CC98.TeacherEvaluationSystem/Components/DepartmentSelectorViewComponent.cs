using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CC98.TeacherEvaluationSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace CC98.TeacherEvaluationSystem.Components
{
	/// <summary>
	/// 部门选择器组件。
	/// </summary>
	public class DepartmentSelectorViewComponent : ViewComponent
	{
		/// <summary>
		/// 初始化组件的新实例。
		/// </summary>
		/// <param name="dbContext">数据库上下文对象。</param>
		public DepartmentSelectorViewComponent(TesDbContext dbContext)
		{
			DbContext = dbContext;
		}

		/// <summary>
		/// 数据库上下文对象。
		/// </summary>
		private TesDbContext DbContext { get; }

		public async Task<IViewComponentResult> InvokeAsync(ModelExpression aspFor)
		{
			var cancellationToken = Request.HttpContext.RequestAborted;

			// 加载所有部门数据
			await DbContext.Departments.LoadAsync(cancellationToken);

			var items = from i in DbContext.Departments
						where i.ParentId == null
						select i;

			ViewBag.ModelFor = aspFor;

			return View(await items.ToArrayAsync(cancellationToken));
		}
	}
}
