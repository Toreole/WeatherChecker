﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherChecker.Data;

#nullable disable

namespace WeatherChecker.Migrations
{
    [DbContext(typeof(WeatherDBContext))]
    partial class WeatherDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("WeatherChecker.Data.ActualWeatherData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Humidity")
                        .HasColumnType("float");

                    b.Property<float>("Temperature")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("WeatherCode")
                        .HasColumnType("int");

                    b.Property<float>("Windspeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Timestamp");

                    b.ToTable("MeasuredWeatherData");
                });

            modelBuilder.Entity("WeatherChecker.Data.ForecastWeatherData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("ForecastTimestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("Humidity")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("PredictionTimestamp")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("Temperature")
                        .HasColumnType("float");

                    b.Property<int>("WeatherCode")
                        .HasColumnType("int");

                    b.Property<float>("Windspeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ForecastTimestamp");

                    b.HasIndex("Id");

                    b.HasIndex("PredictionTimestamp");

                    b.ToTable("Forecasts");
                });
#pragma warning restore 612, 618
        }
    }
}
