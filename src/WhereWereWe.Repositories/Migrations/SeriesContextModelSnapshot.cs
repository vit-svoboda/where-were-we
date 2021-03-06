﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using WhereWereWe.Repositories.Contexts;

namespace WhereWereWe.Repositories.Migrations
{
    [DbContext(typeof(SeriesContext))]
    partial class SeriesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WhereWereWe.Repositories.Entities.Series", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EpisodesPerSeason");

                    b.Property<string>("Name");

                    b.Property<int>("Seasons");

                    b.HasKey("Id");

                    b.ToTable("Series");
                });
        }
    }
}
