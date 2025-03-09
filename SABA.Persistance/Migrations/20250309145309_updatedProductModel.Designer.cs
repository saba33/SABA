﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SABA.Persistance.DataContext;

#nullable disable

namespace SABA.Persistance.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250309145309_updatedProductModel")]
    partial class updatedProductModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductSale", b =>
                {
                    b.Property<int>("SalesSaleId")
                        .HasColumnType("int");

                    b.Property<int>("SoldProductsProductId")
                        .HasColumnType("int");

                    b.HasKey("SalesSaleId", "SoldProductsProductId");

                    b.HasIndex("SoldProductsProductId");

                    b.ToTable("ProductSale");
                });

            modelBuilder.Entity("SABA.Core.Models.ProductModel.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductType")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SABA.Core.Models.RecomendationModel.Recommendation", b =>
                {
                    b.Property<int>("RecommendationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecommendationId"));

                    b.Property<int>("RecommendedUserId")
                        .HasColumnType("int");

                    b.Property<int>("RecommenderId")
                        .HasColumnType("int");

                    b.HasKey("RecommendationId");

                    b.HasIndex("RecommendedUserId");

                    b.HasIndex("RecommenderId");

                    b.ToTable("Recommendations");
                });

            modelBuilder.Entity("SABA.Core.Models.SaleModel.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleId"));

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SaleId");

                    b.HasIndex("UserId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("SABA.Core.Models.UserModel.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("RecommenderId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RecommenderId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProductSale", b =>
                {
                    b.HasOne("SABA.Core.Models.SaleModel.Sale", null)
                        .WithMany()
                        .HasForeignKey("SalesSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SABA.Core.Models.ProductModel.Product", null)
                        .WithMany()
                        .HasForeignKey("SoldProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SABA.Core.Models.RecomendationModel.Recommendation", b =>
                {
                    b.HasOne("SABA.Core.Models.UserModel.User", "RecommendedUser")
                        .WithMany()
                        .HasForeignKey("RecommendedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SABA.Core.Models.UserModel.User", "Recommender")
                        .WithMany("Recommendations")
                        .HasForeignKey("RecommenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RecommendedUser");

                    b.Navigation("Recommender");
                });

            modelBuilder.Entity("SABA.Core.Models.SaleModel.Sale", b =>
                {
                    b.HasOne("SABA.Core.Models.UserModel.User", "Distributor")
                        .WithMany("Sales")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Distributor");
                });

            modelBuilder.Entity("SABA.Core.Models.UserModel.User", b =>
                {
                    b.HasOne("SABA.Core.Models.UserModel.User", "Recommender")
                        .WithMany("RecommendedUsers")
                        .HasForeignKey("RecommenderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Recommender");
                });

            modelBuilder.Entity("SABA.Core.Models.UserModel.User", b =>
                {
                    b.Navigation("Recommendations");

                    b.Navigation("RecommendedUsers");

                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
