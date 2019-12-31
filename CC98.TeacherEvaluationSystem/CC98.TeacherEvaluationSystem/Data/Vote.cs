using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.TeacherEvaluationSystem.Data
{
	/// <summary>
	/// 表示一个对评论的投票。
	/// </summary>
	public class Vote
	{
		/// <summary>
		/// 获取或设置投票关联的评价的用户的标识。
		/// </summary>
		public int MarkUserId { get; set; }
		/// <summary>
		/// 获取或设置投票关联的评价的教师的标识。
		/// </summary>
		public int MarkTeacherId { get; set; }

		/// <summary>
		/// 获取或设置投票的用户的标识。
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// 获取或设置投票值。
		/// </summary>
		public VoteState State { get; set; }

		/// <summary>
		/// 获取或设置该投票关联的评价。
		/// </summary>
		[ForeignKey("MarkTeacherId, MarkUserId")]
		public Mark Mark { get; set; }
	}
}