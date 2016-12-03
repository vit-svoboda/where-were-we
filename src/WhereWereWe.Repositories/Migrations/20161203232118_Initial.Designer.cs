using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WhereWereWe.Repositories;

namespace WhereWereWe.Repositories.Migrations
{
    [DbContext(typeof(SeriesContext))]
    [Migration("20161203232118_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
