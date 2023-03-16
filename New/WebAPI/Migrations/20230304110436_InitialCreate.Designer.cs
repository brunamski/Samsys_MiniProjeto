﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WebAPI.Entities;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(BookDBContext))]
    [Migration("20230304110436_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebAPI.Models.Livro", b =>
                {
                    b.Property<string>("isbn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("preco")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("isbn");

                    b.ToTable("Livros");
                });
#pragma warning restore 612, 618
        }
    }
}
