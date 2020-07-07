﻿// <auto-generated />
using System;
using Exebite.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Exebite.DataAccess.Migrations
{
    [DbContext(typeof(MealOrderingContext))]
    partial class MealOrderingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Exebite.DataAccess.Entities.CustomerEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance");

                    b.Property<DateTime>("Created");

                    b.Property<long>("DefaultLocationId");

                    b.Property<string>("GoogleUserId");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.HasIndex("DefaultLocationId");

                    b.HasIndex("GoogleUserId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.CustomerToFavouriteMealEntity", b =>
                {
                    b.Property<long>("CustomerId");

                    b.Property<long>("MealId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("CustomerId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("CustomerToFavouriteMeal");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.DailyMenuEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Note");

                    b.Property<long>("RestaurantId");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("DailyMenu");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.DailyMenuToMealEntity", b =>
                {
                    b.Property<long>("DailyMenuId");

                    b.Property<long>("MealId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("DailyMenuId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("DailyMenuToMeal");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.LocationEntity", b =>
                {
                    b.Property<long>("Id")
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
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsFromStandardMenu");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<decimal>("Price");

                    b.Property<long>("RestaurantId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.MealToCondimentEntity", b =>
                {
                    b.Property<long>("MealId");

                    b.Property<long>("CondimentId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("MealId", "CondimentId");

                    b.HasIndex("CondimentId");

                    b.ToTable("MealToCondiment");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.OrderEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<long>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("LastModified");

                    b.Property<long>("LocationId");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Date");

                    b.HasIndex("LocationId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.OrderToMealEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("LastModified");

                    b.Property<long>("MealId");

                    b.Property<string>("Note");

                    b.Property<long>("OrderId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderToMeal");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.PaymentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Created");

                    b.Property<long>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("LastModified");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.RestaurantEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contact");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastModified");

                    b.Property<string>("LogoUrl");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("OrderDue");

                    b.Property<string>("SheetId");

                    b.HasKey("Id");

                    b.HasIndex("SheetId");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.CustomerEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.LocationEntity", "DefaultLocation")
                        .WithMany()
                        .HasForeignKey("DefaultLocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.CustomerToFavouriteMealEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.CustomerEntity", "Customer")
                        .WithMany("FavouriteMeals")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exebite.DataAccess.Entities.MealEntity", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.DailyMenuEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("DailyMenus")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.DailyMenuToMealEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.DailyMenuEntity", "DailyMenu")
                        .WithMany("DailyMenuToMeals")
                        .HasForeignKey("DailyMenuId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Exebite.DataAccess.Entities.MealEntity", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.MealEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("Meals")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.MealToCondimentEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.MealEntity", "Condiment")
                        .WithMany()
                        .HasForeignKey("CondimentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exebite.DataAccess.Entities.MealEntity", "Meal")
                        .WithMany("Condiments")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.OrderEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.CustomerEntity", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Exebite.DataAccess.Entities.LocationEntity", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.OrderToMealEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.MealEntity", "Meal")
                        .WithMany("OrdersToMeals")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Exebite.DataAccess.Entities.OrderEntity", "Order")
                        .WithMany("OrdersToMeals")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Exebite.DataAccess.Entities.PaymentEntity", b =>
                {
                    b.HasOne("Exebite.DataAccess.Entities.CustomerEntity", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
