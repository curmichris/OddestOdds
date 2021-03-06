﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OddestOdds.Infrastructure.Data;

namespace OddestOdds.Infrastructure.Migrations
{
    [DbContext(typeof(OddestOddsContext))]
    partial class OddestOddsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OddestOdds.Core.Entities.Odd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AwayTeamName")
                        .IsRequired();

                    b.Property<string>("HomeTeamName")
                        .IsRequired();

                    b.Property<string>("OddName")
                        .IsRequired();

                    b.Property<Guid?>("OddValueId");

                    b.HasKey("Id");

                    b.HasIndex("OddValueId");

                    b.ToTable("Odds");
                });

            modelBuilder.Entity("OddestOdds.Core.Entities.OddValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AwayOddValue")
                        .HasColumnType("decimal(15,2)");

                    b.Property<decimal>("DrawOddValue")
                        .HasColumnType("decimal(15,2)");

                    b.Property<decimal>("HomeOddValue")
                        .HasColumnType("decimal(15,2)");

                    b.HasKey("Id");

                    b.ToTable("OddValues");
                });

            modelBuilder.Entity("OddestOdds.Core.Entities.Odd", b =>
                {
                    b.HasOne("OddestOdds.Core.Entities.OddValue", "OddValues")
                        .WithMany()
                        .HasForeignKey("OddValueId");
                });
#pragma warning restore 612, 618
        }
    }
}
