﻿// <auto-generated />
using EroMangaDatabase.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EroMangaDatabase.Migrations
{
    [DbContext(typeof(DataBase_Version3))]
    partial class DataBase_Version3ModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32");

            modelBuilder.Entity("EroMangaDatabase.Entities.FilteredImage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Hash")
                        .HasColumnType("TEXT");

                    b.Property<long>("ZipEntryLength")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("FilteredImages");
                });

            modelBuilder.Entity("EroMangaDatabase.Entities.MangaFolder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("MangaFolders");
                });

            modelBuilder.Entity("EroMangaDatabase.Entities.ReadingInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AbsolutePath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsContentTranslated")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MangaName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MangaName_Translated")
                        .HasColumnType("TEXT");

                    b.Property<int>("PageAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReadingPosition")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TagsAddedByUser")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("ReadingInfos");
                });

            modelBuilder.Entity("EroMangaDatabase.Entities.TagCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Keywords")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("TagCategorys");
                });

            modelBuilder.Entity("EroMangaDatabase.Entities.UWPAccessIStorage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccessToken")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFileOrFolder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("UWPAccessIStorages");
                });
#pragma warning restore 612, 618
        }
    }
}
