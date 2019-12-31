using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.TeacherEvaluationSystem.Data
{
	/// <summary>
	/// 表示用户的一个评价。
	/// </summary>
	public class Mark
	{
		/// <summary>
		/// 获取或设置评价关联的用户的标识。
		/// </summary>
		public int UserId { get; set; }
		/// <summary>
		/// 获取或设置评价关联的老师的标识。
		/// </summary>
		public int TeacherId { get; set; }

		/// <summary>
		/// 获取或设置该评价关联的老师。
		/// </summary>
		[ForeignKey(nameof(TeacherId))]
		public Teacher Teacher { get; set; }

		/// <summary>
		/// 用户的给分。
		/// </summary>
		[Range(0, int.MaxValue)]
		public int Score { get; set; }

		/// <summary>
		/// 用户的评论。
		/// </summary>
		public string? Comment { get; set; }

		/// <summary>
		/// 获取或设置评论的投票。
		/// </summary>
		[InverseProperty(nameof(Vote.Mark))]
		public virtual IList<Vote> Votes { get; set; } = new Collection<Vote>();
	}
}