﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payroll_Mohamed_Bayoumi.Context;

#nullable disable

namespace Payroll_Mohamed_Bayoumi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241205183801_AttendanceRecord")]
    partial class AttendanceRecord
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.AbsencePenalty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AbsenceDays")
                        .HasColumnType("int");

                    b.Property<bool>("IsBonus")
                        .HasColumnType("bit");

                    b.Property<double>("PenaltyPercentage")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("AbsencePenalties");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.AttendanceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AbsenceDays")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AttendanceRecords");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.DepartmentIncentive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<double>("IncentivePercentage")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId")
                        .IsUnique();

                    b.ToTable("DepartmentIncentives");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HiringDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("JobGrade")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("JobGrade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.SeniorityIncentive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("IncentivePercentage")
                        .HasColumnType("float");

                    b.Property<int>("YearsOfService")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SeniorityIncentives");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.AttendanceRecord", b =>
                {
                    b.HasOne("Payroll_Mohamed_Bayoumi.Models.Employee", "Employee")
                        .WithMany("AttendanceRecords")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.DepartmentIncentive", b =>
                {
                    b.HasOne("Payroll_Mohamed_Bayoumi.Models.Department", "Department")
                        .WithOne("DepartmentIncentive")
                        .HasForeignKey("Payroll_Mohamed_Bayoumi.Models.DepartmentIncentive", "DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.Employee", b =>
                {
                    b.HasOne("Payroll_Mohamed_Bayoumi.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.Department", b =>
                {
                    b.Navigation("DepartmentIncentive");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Payroll_Mohamed_Bayoumi.Models.Employee", b =>
                {
                    b.Navigation("AttendanceRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
