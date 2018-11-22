﻿// <auto-generated />
using System;
using Exebite.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exebite.DataAccess.Migrations
{
    [DbContext(typeof(FoodOrderingContext))]
    [Migration("20180726115934_GoogleIdIndex")]
    partial class GoogleIdIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Exebite.DataAccess.Entities.CustomerAliasesEntities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("CustomerAliases");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance");

                    b.Property<DateTime>("Created");

                    b.Property<string>("GoogleUserId");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("LocationId");

                    b.Property<string>("Name");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("GoogleUserId");

                    b.HasIndex("LocationId");

                    b.HasIndex("RoleId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.DailyMenuEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("DailyMenu");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.FoodEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<int?>("DailyMenuId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsInactive");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("RestaurantId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("DailyMenuId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.FoodEntityMealEntities", b =>
                {
                    b.Property<int>("FoodEntityId");

                    b.Property<int>("MealEntityId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("FoodEntityId", "MealEntityId");

                    b.HasIndex("MealEntityId");

                    b.ToTable("FoodToMeal");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.FoodEntityRecipeEntity", b =>
                {
                    b.Property<int>("FoodEntityId");

                    b.Property<int>("RecepieEntityId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("FoodEntityId", "RecepieEntityId");

                    b.HasIndex("RecepieEntityId");

                    b.ToTable("FoodToRecipe");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.LocationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.MealEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("MealId");

                    b.Property<string>("Note");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Date");

                    b.HasIndex("MealId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.PaymentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Created");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.RecipeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<int>("MainCourseId");

                    b.Property<int>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("MainCourseId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.RestaurantEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.CustomerAliasesEntities", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.CustomerEntity", "Customer")
                        .WithMany("Aliases")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exebite.DataAccess.Entities.RestaurantEntity", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.CustomerEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.LocationEntity", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exebite.DataAccess.Entities.RoleEntity", "Role")
                        .WithMany("Customers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.DailyMenuEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.RestaurantEntity", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.FoodEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.DailyMenuEntity", "DailyMenu")
                        .WithMany("Foods")
                        .HasForeignKey("DailyMenuId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Exebite.DataAccess.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("Foods")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.FoodEntityMealEntities", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.FoodEntity", "FoodEntity")
                        .WithMany("FoodEntityMealEntity")
                        .HasForeignKey("FoodEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exebite.DataAccess.Entities.MealEntity", "MealEntity")
                        .WithMany("FoodEntityMealEntities")
                        .HasForeignKey("MealEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.FoodEntityRecipeEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.FoodEntity", "FoodEntity")
                        .WithMany("FoodEntityRecipeEntities")
                        .HasForeignKey("FoodEntityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Exebite.DataAccess.Entities.RecipeEntity", "RecipeEntity")
                        .WithMany("FoodEntityRecipeEntities")
                        .HasForeignKey("RecepieEntityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.OrderEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.CustomerEntity", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exebite.DataAccess.Entities.MealEntity", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.PaymentEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.CustomerEntity", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.RecipeEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.FoodEntity", "MainCourse")
                        .WithMany()
                        .HasForeignKey("MainCourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exebite.DataAccess.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("Recipes")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
