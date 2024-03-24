﻿// <auto-generated />
using System;
using IMDbLib.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IMDbLib.Migrations
{
    [DbContext(typeof(IMDb_Context))]
    [Migration("20240323085633_IMDb_Mig01")]
    partial class IMDb_Mig01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IMDbLib.Models.BlockBuster", b =>
                {
                    b.Property<string>("Nconst")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Tconst")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Nconst", "Tconst");

                    b.HasIndex("Tconst");

                    b.ToTable("PersonalBlockbusters");
                });

            modelBuilder.Entity("IMDbLib.Models.Genre", b =>
                {
                    b.Property<string>("GenreType")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("GenreType");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieBase", b =>
                {
                    b.Property<string>("Tconst")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("EndYear")
                        .HasColumnType("date");

                    b.Property<bool>("IsAdult")
                        .HasColumnType("bit");

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RuntimeMins")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("StartYear")
                        .HasColumnType("date");

                    b.Property<string>("TitleTypeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Tconst");

                    b.HasIndex("TitleTypeType");

                    b.ToTable("MovieBases");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieDirector", b =>
                {
                    b.Property<string>("Tconst")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nconst")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Tconst", "Nconst");

                    b.HasIndex("Nconst");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieGenre", b =>
                {
                    b.Property<string>("Tconst")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GenreType")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Tconst", "GenreType");

                    b.HasIndex("GenreType");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieWriter", b =>
                {
                    b.Property<string>("Tconst")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nconst")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Tconst", "Nconst");

                    b.HasIndex("Nconst");

                    b.ToTable("Writers");
                });

            modelBuilder.Entity("IMDbLib.Models.Person", b =>
                {
                    b.Property<string>("Nconst")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("BirthYear")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DeathYear")
                        .HasColumnType("date");

                    b.Property<string>("PrimaryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Nconst");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("IMDbLib.Models.PersonalCareer", b =>
                {
                    b.Property<string>("Nconst")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PrimProf")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Nconst", "PrimProf");

                    b.HasIndex("PrimProf");

                    b.ToTable("PersonalCareers");
                });

            modelBuilder.Entity("IMDbLib.Models.Profession", b =>
                {
                    b.Property<string>("PrimaryProfession")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PrimaryProfession");

                    b.ToTable("Professions");
                });

            modelBuilder.Entity("IMDbLib.Models.TitleType", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Type");

                    b.ToTable("TitleTypes");
                });

            modelBuilder.Entity("IMDbLib.Models.BlockBuster", b =>
                {
                    b.HasOne("IMDbLib.Models.Person", "Person")
                        .WithMany("BlockBusters")
                        .HasForeignKey("Nconst")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMDbLib.Models.MovieBase", "MovieBase")
                        .WithMany()
                        .HasForeignKey("Tconst")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieBase");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieBase", b =>
                {
                    b.HasOne("IMDbLib.Models.TitleType", "TitleType")
                        .WithMany()
                        .HasForeignKey("TitleTypeType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TitleType");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieDirector", b =>
                {
                    b.HasOne("IMDbLib.Models.Person", "Person")
                        .WithMany("Directors")
                        .HasForeignKey("Nconst")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMDbLib.Models.MovieBase", "MovieBase")
                        .WithMany("Directors")
                        .HasForeignKey("Tconst")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieBase");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieGenre", b =>
                {
                    b.HasOne("IMDbLib.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMDbLib.Models.MovieBase", "MovieBase")
                        .WithMany("MovieGenres")
                        .HasForeignKey("Tconst")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("MovieBase");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieWriter", b =>
                {
                    b.HasOne("IMDbLib.Models.Person", "Person")
                        .WithMany("Writers")
                        .HasForeignKey("Nconst")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMDbLib.Models.MovieBase", "MovieBase")
                        .WithMany("Writers")
                        .HasForeignKey("Tconst")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieBase");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("IMDbLib.Models.PersonalCareer", b =>
                {
                    b.HasOne("IMDbLib.Models.Person", "Person")
                        .WithMany("PersonalCareers")
                        .HasForeignKey("Nconst")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IMDbLib.Models.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("PrimProf")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Profession");
                });

            modelBuilder.Entity("IMDbLib.Models.MovieBase", b =>
                {
                    b.Navigation("Directors");

                    b.Navigation("MovieGenres");

                    b.Navigation("Writers");
                });

            modelBuilder.Entity("IMDbLib.Models.Person", b =>
                {
                    b.Navigation("BlockBusters");

                    b.Navigation("Directors");

                    b.Navigation("PersonalCareers");

                    b.Navigation("Writers");
                });
#pragma warning restore 612, 618
        }
    }
}
