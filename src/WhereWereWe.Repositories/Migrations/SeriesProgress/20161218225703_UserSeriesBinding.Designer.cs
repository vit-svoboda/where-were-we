using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WhereWereWe.Repositories.Contexts;

namespace WhereWereWe.Repositories.Migrations.SeriesProgress
{
    [DbContext(typeof(SeriesProgressContext))]
    [Migration("20161218225703_UserSeriesBinding")]
    partial class UserSeriesBinding
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

            modelBuilder.Entity("WhereWereWe.Repositories.Entities.SeriesProgress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Episode");

                    b.Property<int>("Season");

                    b.Property<Guid?>("SeriesId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.HasIndex("UserId");

                    b.ToTable("SeriesProgress");
                });

            modelBuilder.Entity("WhereWereWe.Repositories.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WhereWereWe.Repositories.Entities.SeriesProgress", b =>
                {
                    b.HasOne("WhereWereWe.Repositories.Entities.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId");

                    b.HasOne("WhereWereWe.Repositories.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
