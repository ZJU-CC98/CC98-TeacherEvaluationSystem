using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using CC98.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sakura.AspNetCore;
using Sakura.AspNetCore.Authentication;

namespace CC98.TeacherEvaluationSystem.Controllers
{
	/// <summary>
	/// 提供对用户登录和注销的支持。
	/// </summary>
	public class AccountController : Controller
	{
		/// <summary>
		/// 初始化 <see cref="AccountController"/> 的新实例。
		/// </summary>
		/// <param name="externalSignInManager">外部登录验证服务。</param>
		public AccountController(ExternalSignInManager externalSignInManager, IOperationMessageAccessor operationMessageAccessor)
		{
			ExternalSignInManager = externalSignInManager;
			OperationMessageAccessor = operationMessageAccessor;
		}

		/// <summary>
		/// 外部登录验证服务。
		/// </summary>
		private ExternalSignInManager ExternalSignInManager { get; }

		/// <summary>
		/// 操作消息访问服务。
		/// </summary>
		private IOperationMessageAccessor OperationMessageAccessor { get; }

		/// <summary>
		/// 请求进行登录。
		/// </summary>
		/// <param name="returnUrl">登录后要返回的 URL 地址。</param>
		/// <returns>操作结果。</returns>
		[AllowAnonymous]
		public IActionResult LogOn(string returnUrl)
		{
			var authProperties = new AuthenticationProperties
			{
				RedirectUri = Url.Action("LogOnCallback", "Account", new { returnUrl })
			};

			return Challenge(authProperties, CC98Defaults.AuthenticationScheme);
		}

		/// <summary>
		/// 登录后执行回调。
		/// </summary>
		/// <param name="returnUrl">登录完成后要跳转的地址。</param>
		/// <returns>操作结果。</returns>
		[AllowAnonymous]
		public async Task<IActionResult> LogOnCallback(string returnUrl)
		{
			var principal = await ExternalSignInManager.SignInFromExternalCookieAsync();

			if (principal?.Identities == null)
			{
				OperationMessageAccessor.Messages.Add(OperationMessageLevel.Success, "登录失败",
					"这有可能是你拒绝了授权或者登录系统发生故障引起的。请再试一次并确定你同意了授权请求。如果你持续看到这个问题，请联系管理员。");

				return RedirectToAction("Index", "Home");
			}

			// 防止跳转到外部地址
			if (!Url.IsLocalUrl(returnUrl))
			{
				returnUrl = Url.Action("Index", "Home");
			}

			return Redirect(returnUrl);
		}

		/// <summary>
		/// 执行注销操作。
		/// </summary>
		/// <returns>操作结果。</returns>
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOff()
		{
			await ExternalSignInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}