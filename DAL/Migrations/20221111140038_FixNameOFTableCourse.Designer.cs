﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20221111140038_FixNameOFTableCourse")]
    partial class FixNameOFTableCourse
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.Aggregates.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Coursename")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("coursename");

                    b.Property<string>("Cousecode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Course_code");

                    b.Property<Guid?>("LectureId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("lecture_id");

                    b.HasKey("Id");

                    b.ToTable("course");
                });

            modelBuilder.Entity("DAL.Aggregates.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("full_name");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9cc73429-7f96-41d1-a82a-bb0db4e7d3a4"),
                            Email = "admin@pro.org",
                            FullName = "Super User Admin",
                            Password = "$2a$11$nGACkMSCIElR1rc.NbDF7udj2jjCx2SfUq9DKM4HMuAlmfIeZZMcK",
                            Role = "admin",
                            Username = "sudo"
                        },
                        new
                        {
                            Id = new Guid("1f284195-86a2-4f4a-9611-35bb8f0c97f8"),
                            Email = "sample4@sample.sample",
                            FullName = "Sample User Four",
                            Password = "sampass4",
                            Username = "sample4"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
