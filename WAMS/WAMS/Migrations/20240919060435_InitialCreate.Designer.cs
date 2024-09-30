﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WAMS.Backend.Data;

#nullable disable

namespace WAMS.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240919060435_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ClassUser", b =>
                {
                    b.Property<int>("ClassesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("ClassesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("ClassUser");
                });

            modelBuilder.Entity("WAMS.Backend.Model.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("TimetableId")
                        .HasColumnType("int");

                    b.Property<int>("Weekday")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TimetableId");

                    b.ToTable("Day");
                });

            modelBuilder.Entity("WAMS.Backend.Model.UserPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Policy")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("WAMS.Components.Model.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("TimetableId")
                        .HasColumnType("int");

                    b.Property<string>("Year")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("TimetableId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("WAMS.Components.Model.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClassId")
                        .HasColumnType("int");

                    b.Property<int?>("DayId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("DayId");

                    b.HasIndex("RoomId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("WAMS.Components.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("WAMS.Components.Model.Timetable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Timetable");
                });

            modelBuilder.Entity("WAMS.Components.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookedRoomId")
                        .HasColumnType("int");

                    b.Property<int?>("CurrentRoomId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("MailAdress")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BookedRoomId");

                    b.HasIndex("CurrentRoomId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClassUser", b =>
                {
                    b.HasOne("WAMS.Components.Model.Class", null)
                        .WithMany()
                        .HasForeignKey("ClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WAMS.Components.Model.User", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WAMS.Backend.Model.Day", b =>
                {
                    b.HasOne("WAMS.Components.Model.Timetable", "Timetable")
                        .WithMany("Days")
                        .HasForeignKey("TimetableId");

                    b.Navigation("Timetable");
                });

            modelBuilder.Entity("WAMS.Components.Model.Class", b =>
                {
                    b.HasOne("WAMS.Components.Model.Timetable", "Timetable")
                        .WithMany()
                        .HasForeignKey("TimetableId");

                    b.Navigation("Timetable");
                });

            modelBuilder.Entity("WAMS.Components.Model.Course", b =>
                {
                    b.HasOne("WAMS.Components.Model.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassId");

                    b.HasOne("WAMS.Backend.Model.Day", null)
                        .WithMany("Courses")
                        .HasForeignKey("DayId");

                    b.HasOne("WAMS.Components.Model.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.HasOne("WAMS.Components.Model.User", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");

                    b.Navigation("Class");

                    b.Navigation("Room");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("WAMS.Components.Model.User", b =>
                {
                    b.HasOne("WAMS.Components.Model.Room", "BookedRoom")
                        .WithMany()
                        .HasForeignKey("BookedRoomId");

                    b.HasOne("WAMS.Components.Model.Room", "CurrentRoom")
                        .WithMany()
                        .HasForeignKey("CurrentRoomId");

                    b.Navigation("BookedRoom");

                    b.Navigation("CurrentRoom");
                });

            modelBuilder.Entity("WAMS.Backend.Model.Day", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("WAMS.Components.Model.Timetable", b =>
                {
                    b.Navigation("Days");
                });
#pragma warning restore 612, 618
        }
    }
}
