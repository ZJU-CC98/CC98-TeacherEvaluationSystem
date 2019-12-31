using System;
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
}
