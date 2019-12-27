using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CC98.TeacherEvaluationSystem.Data
{
	/// <summary>
	/// 教师评价系统数据库上下文对象。
	/// </summary>
	public class TesDbContext : DbContext
	{
		public TesDbContext(DbContextOptions<TesDbContext> options)
			: base(options)
		{

		}

		/// <summary>
		/// 获取或设置数据库中包含的部门的集合。
		/// </summary>
		public virtual DbSet<Department> Departments { get; set; }

		/// <summary>
		/// 获取或设置数据库中包含的教师的集合。
		/// </summary>
		public virtual DbSet<Teacher> Teachers { get; set; }

		/// <summary>
		/// 获取或设置数据库中包含的评价的集合。
		/// </summary>
		public virtual DbSet<Mark> Marks { get; set; }

		/// <summary>
		/// 获取或设置数据库中包含的投票的集合。
		/// </summary>
		public virtual DbSet<Vote> Votes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Mark>().HasKey(i => new { i.TeacherId, i.UserId });
			modelBuilder.Entity<Mark>().HasIndex(I => I.UserId);
			modelBuilder.Entity<Mark>().HasIndex(i => i.TeacherId);

			modelBuilder.Entity<Vote>().HasKey(i => new { i.MarkTeacherId, i.MarkUserId, i.UserId });
		}
	}

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

	/// <summary>
	/// 定义投票状态。
	/// </summary>
	public enum VoteState
	{
		/// <summary>
		/// 未投票。
		/// </summary>
		None = 0,
		/// <summary>
		/// 赞成。
		/// </summary>
		Agree = 1,
		/// <summary>
		/// 反对。
		/// </summary>
		Disagree = 2
	}
}
