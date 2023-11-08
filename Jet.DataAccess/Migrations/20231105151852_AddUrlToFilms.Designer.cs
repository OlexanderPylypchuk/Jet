﻿// <auto-generated />
using Jet.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jet.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231105151852_AddUrlToFilms")]
    partial class AddUrlToFilms
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Jet.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("CategoryTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Sci-Fi"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Horror"
                        });
                });

            modelBuilder.Entity("Jet.Models.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("FilmTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Some text about Avatar",
                            ImgUrl = "",
                            Price = 340,
                            Producer = "Some dude",
                            Score = 9.8000000000000007,
                            Title = "Avatar"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "Some text about Avatar 2",
                            ImgUrl = "",
                            Price = 400,
                            Producer = "Some dude",
                            Score = 10.0,
                            Title = "Avatar 2"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Description = "Some text about Avatar: The last airbender",
                            ImgUrl = "",
                            Price = 340,
                            Producer = "Some dude 2",
                            Score = 8.6999999999999993,
                            Title = "Avatar: The last airbender"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Some text about Drive",
                            ImgUrl = "",
                            Price = 540,
                            Producer = "Some dude",
                            Score = 7.0,
                            Title = "Drive"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Description = "Some text about Drive",
                            ImgUrl = "",
                            Price = 540,
                            Producer = "Some dude",
                            Score = 7.0,
                            Title = "Drive"
                        });
                });

            modelBuilder.Entity("Jet.Models.Film", b =>
                {
                    b.HasOne("Jet.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}