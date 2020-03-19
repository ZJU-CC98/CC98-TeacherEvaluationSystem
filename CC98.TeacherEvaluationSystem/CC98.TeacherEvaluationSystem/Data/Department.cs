using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CC98.TeacherEvaluationSystem.Data
{
	/// <summary>
	/// 表示一个部门。
	/// </summary>
	public class Department
	{
		/// <summary>
		/// 获取或设置该部门的标识。
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// 获取或设置该部门的标识。
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 获取或设置该部门的描述。
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// 获取或设置该部门的上级部门的标识。
		/// </summary>
		public int? ParentId { get; set; }

		/// <summary>
		/// 获取或设置该部门的上级部门。
		/// </summary>
		[ForeignKey(nameof(ParentId))]
		public Department? Parent { get; set; }

		/// <summary>
		/// 获取或设置该部门的下级部门的标识。
		/// </summary>
		[InverseProperty(nameof(Parent))]
		public virtual IList<Department> Children { get; set; } = new Collection<Department>();

		/// <summary>
		/// 获取或设置该部门包含的教师的集合。
		/// </summary>
		[InverseProperty(nameof(Teacher.Department))]
		public virtual IList<Teacher> Teachers { get; set; } = new Collection<Teacher>();
	}
}