using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CC98.TeacherEvaluationSystem
{
	/// <summary>
	/// 定义应用程序策略。该类型为静态类型。
	/// </summary>
	public static class Policies
	{
		/// <summary>
		/// 编辑列表。
		/// </summary>
		public const string Edit = nameof(Edit);

		/// <summary>
		/// 审核发言。
		/// </summary>
		public const string Review = nameof(Review);

		/// <summary>
		/// 管理系统。
		/// </summary>
		public const string Admin = nameof(Admin);

		/// <summary>
		/// 定义应用程序角色。该类型为静态类型。
		/// </summary>
		public static class Roles
		{
			/// <summary>
			/// 教师信息编辑者。
			/// </summary>
			public const string Editor = "Teacher Editors";
			/// <summary>
			/// 教师信息审核者。
			/// </summary>
			public const string Reviewer = "Teacher Reviewers";

			/// <summary>
			/// 教师系统操作员。
			/// </summary>
			public const string Operator = "Teacher Operators";

			/// <summary>
			/// 教师信息管理员。
			/// </summary>
			public const string Administrators = "Teacher Administrators";

			/// <summary>
			/// 通用管理员角色。
			/// </summary>
			public const string GeneralAdministrators = "Administrators";
		}
	}
}
