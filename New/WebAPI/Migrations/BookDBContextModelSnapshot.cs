﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Entities;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class BookDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebAPI.Infrastructure.Entities.Author", b =>
                {
                    b.Property<long>("authorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("authorId"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("authorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("WebAPI.Infrastructure.Entities.Book", b =>
                {
                    b.Property<string>("isbn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("authorId")
                        .HasColumnType("bigint");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("isbn");

                    b.HasIndex("authorId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("WebAPI.Infrastructure.Entities.Book", b =>
                {
                    b.HasOne("WebAPI.Infrastructure.Entities.Author", "Author")
                        .WithMany()
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });
#pragma warning restore 612, 618
        }
    }
}
