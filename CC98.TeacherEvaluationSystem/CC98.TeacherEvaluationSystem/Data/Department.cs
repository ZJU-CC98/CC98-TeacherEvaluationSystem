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
		/// 获取或设置该部门包含的教师的集合。
		/// </summary>
		[InverseProperty(nameof(Teacher.Department))]
		public virtual IList<Teacher> Teachers { get; set; } = new Collection<Teacher>();
	}
}