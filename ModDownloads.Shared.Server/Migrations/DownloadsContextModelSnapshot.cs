﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModDownloads.Shared.Server.Context;

namespace ModDownloads.Shared.Server.Migrations
{
    [DbContext(typeof(DownloadsContext))]
    partial class DownloadsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview.19074.3");

            modelBuilder.Entity("ModDownloads.Shared.Entities.Download", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Downloads");

                    b.Property<int?>("ModID");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("ID");

                    b.HasIndex("ModID");

                    b.ToTable("Download");
                });

            modelBuilder.Entity("ModDownloads.Shared.Entities.Mod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("URL")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Mod");
                });

            modelBuilder.Entity("ModDownloads.Shared.Entities.Download", b =>
                {
                    b.HasOne("ModDownloads.Shared.Entities.Mod", "Mod")
                        .WithMany("Downloads")
                        .HasForeignKey("ModID");
                });
#pragma warning restore 612, 618
        }
    }
}
