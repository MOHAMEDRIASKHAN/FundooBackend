﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RespositryLayer.Context;

#nullable disable

namespace RespositryLayer.Migrations
{
    [DbContext(typeof(FundooDBContext))]
    [Migration("20221224131330_fundooonote")]
    partial class fundooonote
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RespositryLayer.Entity.CollabTable", b =>
                {
                    b.Property<long>("CollabID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CollabID"), 1L, 1);

                    b.Property<string>("CollabEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modifiedat")
                        .HasColumnType("datetime2");

                    b.Property<long>("NoteID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("CollabID");

                    b.HasIndex("NoteID");

                    b.HasIndex("UserID");

                    b.ToTable("CollabDetailTables");
                });

            modelBuilder.Entity("RespositryLayer.Entity.LabelTable", b =>
                {
                    b.Property<long>("LabelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LabelID"), 1L, 1);

                    b.Property<string>("LabelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NoteID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("LabelID");

                    b.HasIndex("NoteID");

                    b.HasIndex("UserID");

                    b.ToTable("LabelDetailTables");
                });

            modelBuilder.Entity("RespositryLayer.Entity.NoteTable", b =>
                {
                    b.Property<long>("NoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("NoteID"), 1L, 1);

                    b.Property<bool>("Archieve")
                        .HasColumnType("bit");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("PinNotes")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Reminder")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Trash")
                        .HasColumnType("bit");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("NoteID");

                    b.HasIndex("UserID");

                    b.ToTable("NoteDetailTables");
                });

            modelBuilder.Entity("RespositryLayer.Entity.UserTable", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserID"), 1L, 1);

                    b.Property<string>("EmailID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("UserDetailTables");
                });

            modelBuilder.Entity("RespositryLayer.Entity.CollabTable", b =>
                {
                    b.HasOne("RespositryLayer.Entity.NoteTable", "NoteDetailTables")
                        .WithMany()
                        .HasForeignKey("NoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RespositryLayer.Entity.UserTable", "UserDetailTables")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NoteDetailTables");

                    b.Navigation("UserDetailTables");
                });

            modelBuilder.Entity("RespositryLayer.Entity.LabelTable", b =>
                {
                    b.HasOne("RespositryLayer.Entity.NoteTable", "NoteDetailTables")
                        .WithMany()
                        .HasForeignKey("NoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RespositryLayer.Entity.UserTable", "UserDetailTables")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NoteDetailTables");

                    b.Navigation("UserDetailTables");
                });

            modelBuilder.Entity("RespositryLayer.Entity.NoteTable", b =>
                {
                    b.HasOne("RespositryLayer.Entity.UserTable", "UserDetailTables")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDetailTables");
                });
#pragma warning restore 612, 618
        }
    }
}
