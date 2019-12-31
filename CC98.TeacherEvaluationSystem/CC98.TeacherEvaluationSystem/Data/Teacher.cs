using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.TeacherEvaluationSystem.Data
{
	/// <summary>
	/// 表示一个教师。
	/// </summary>
	public class Teacher
	{
		/// <summary>
		/// 获取或设置该教师的标识。
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// 获取或设置该教师的名称。
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// 获取或设置该教师的描述。
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// 获取或设置该教师关联到的部门的标识。
		/// </summary>
		public int? DepartmentId { get; set; }

		/// <summary>
		/// 获取或设置该教师关联到的部门。
		/// </summary>
		[ForeignKey(nameof(DepartmentId))]
		public Department? Department { get; set; }

		/// <summary>
		/// 获取或设置该教师关联的评价的集合。
		/// </summary>
		[InverseProperty(nameof(Mark.Teacher))]
		public virtual IList<Mark> Marks { get; set; } = new Collection<Mark>();
	}
}