using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace CC98.TeacherEvaluationSystem
{
	/// <summary>
	/// 提供辅助方法。该类型为静态类型。
	/// </summary>
	public static class Utility
	{
		/// <summary>
		/// 判断给定用户是否满足给定的策略。
		/// </summary>
		/// <param name="authorizationService">用于进行授权验证的授权服务。</param>
		/// <param name="user">要验证的用户。</param>
		/// <param name="policyName">要验证的策略名称。</param>
		/// <returns>表示异步操作的任务。操作结果包含一个值，指示给定的用户是否满足给定的策略。</returns>
		public static async Task<bool> MatchesAsync(this IAuthorizationService authorizationService, ClaimsPrincipal user, string policyName)
		{
			var result = await authorizationService.AuthorizeAsync(user, policyName);
			return result.Succeeded;
		}

		/// <summary>
		/// 判断给定用户是否满足给定的多个策略之一。
		/// </summary>
		/// <param name="authorizationService">用于进行授权验证的授权服务。</param>
		/// <param name="user">要验证的用户。</param>
		/// <param name="policyNames">要验证的一个或多个策略的名称。</param>
		/// <returns>表示异步操作的任务。操作结果包含一个值，指示给定的用户是否满足至少其中一个策略。</returns>
		public static async Task<bool> MatchesAnyAsync(this IAuthorizationService authorizationService,
			ClaimsPrincipal user, params string[] policyNames)
		{
			foreach (var policyName in policyNames)
			{
				if (await authorizationService.MatchesAsync(user, policyName))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 获取或设置一个值，指示 HTTP 上下文的当前用户是否满足给定策略。
		/// </summary>
		/// <param name="httpContext">HTTP 上下文对象。</param>
		/// <param name="policyName">要验证的策略的名称。</param>
		/// <returns>表示异步操作的任务。操作结果包含一个值，指示 HTTP 上下文的当前用户是否满足给定的策略。</returns>
		public static Task<bool> UserMatchesAsync(this HttpContext httpContext, string policyName)
		{
			var authorizationService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
			return authorizationService.MatchesAsync(httpContext.User, policyName);
		}

		/// <summary>
		/// 获取或设置一个值，指示 HTTP 上下文的当前用户是否满足给定策略。
		/// </summary>
		/// <param name="httpContext">HTTP 上下文对象。</param>
		/// <param name="policyNames">要验证的一个或多个策略的名称。</param>
		/// <returns>表示异步操作的任务。操作结果包含一个值，指示 HTTP 上下文的当前用户是否满足至少其中一个策略。</returns>
		public static Task<bool> UserMatchesAnyAsync(this HttpContext httpContext, params string[] policyNames)
		{
			var authorizationService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
			return authorizationService.MatchesAnyAsync(httpContext.User, policyNames);

		}

		/// <summary>
		/// 获取或设置一个值，指示 Razor 页面的当前用户是否满足给定策略。
		/// </summary>
		/// <param name="page">Razor 页面对象。</param>
		/// <param name="policyName">要验证的策略的名称。</param>
		/// <returns>表示异步操作的任务。操作结果包含一个值，指示 Razor 页面的当前用户是否满足给定的策略。</returns>
		public static Task<bool> UserMatchesAsync(this RazorPageBase page, string policyName) =>
			page.ViewContext.HttpContext.UserMatchesAsync(policyName);

		/// <summary>
		/// 获取或设置一个值，指示 Razor 页面的当前用户是否满足给定策略。
		/// </summary>
		/// <param name="page">Razor 页面对象。</param>
		/// <param name="policyNames">要验证的一个或多个策略的名称。</param>
		/// <returns>表示异步操作的任务。操作结果包含一个值，指示 Razor 页面的当前用户是否满足至少其中一个策略。</returns>
		public static Task<bool> UserMatchesAnyAsync(this RazorPageBase page, params string[] policyNames) =>
			page.ViewContext.HttpContext.UserMatchesAnyAsync(policyNames);

		/// <summary>
		/// 获取或设置一个值，指示控制器的当前用户是否满足给定策略。
		/// </summary>
		/// <param name="controller">控制器对象。</param>
		/// <param name="policyName">要验证的策略的名称。</param>
		/// <returns>表示异步操作的任务。操作结果包含一个值，指示控制器的当前用户是否满足给定的策略。</returns>
		public static Task<bool> UserMatchesAsync(this ControllerBase controller, string policyName) =>
			controller.HttpContext.UserMatchesAsync(policyName);

		/// <summary>
		/// 获取或设置一个值，指示控制器的当前用户是否满足给定策略。
		/// </summary>
		/// <param name="controller">控制器对象。</param>
		/// <param name="policyNames">要验证的一个或多个策略的名称。</param>
		/// <returns>表示异步操作的任务。操作结果包含一个值，指示控制器的当前用户是否满足至少其中一个策略。</returns>
		public static Task<bool> UserMatchesAnyAsync(this ControllerBase controller, params string[] policyNames) =>
			controller.HttpContext.UserMatchesAnyAsync(policyNames);
	}
}
