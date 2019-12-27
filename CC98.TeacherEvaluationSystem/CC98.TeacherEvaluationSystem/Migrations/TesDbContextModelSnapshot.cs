﻿// <auto-generated />
using System;
using CC98.TeacherEvaluationSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CC98.TeacherEvaluationSystem.Migrations
{
    [DbContext(typeof(TesDbContext))]
    partial class TesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CC98.TeacherEvaluationSystem.Data.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CC98.TeacherEvaluationSystem.Data.Mark", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("TeacherId", "UserId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("UserId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("CC98.TeacherEvaluationSystem.Data.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("CC98.TeacherEvaluationSystem.Data.Vote", b =>
                {
                    b.Property<int>("MarkTeacherId")
                        .HasColumnType("int");

                    b.Property<int>("MarkUserId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("MarkTeacherId", "MarkUserId", "UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("CC98.TeacherEvaluationSystem.Data.Mark", b =>
                {
                    b.HasOne("CC98.TeacherEvaluationSystem.Data.Teacher", "Teacher")
                        .WithMany("Marks")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CC98.TeacherEvaluationSystem.Data.Teacher", b =>
                {
                    b.HasOne("CC98.TeacherEvaluationSystem.Data.Department", "Department")
                        .WithMany("Teachers")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("CC98.TeacherEvaluationSystem.Data.Vote", b =>
                {
                    b.HasOne("CC98.TeacherEvaluationSystem.Data.Mark", "Mark")
                        .WithMany("Votes")
                        .HasForeignKey("MarkTeacherId", "MarkUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
