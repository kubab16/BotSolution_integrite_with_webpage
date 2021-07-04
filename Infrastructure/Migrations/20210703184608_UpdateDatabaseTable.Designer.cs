﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210703184608_UpdateDatabaseTable")]
    partial class UpdateDatabaseTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("Infrastructure.Comands", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<bool>("Funny")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Information")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Miusic")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Moderation")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Comands");
                });

            modelBuilder.Entity("Infrastructure.EPisodeComments", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("comment")
                        .HasColumnType("longtext");

                    b.Property<int>("negative")
                        .HasColumnType("int");

                    b.Property<int>("positive")
                        .HasColumnType("int");

                    b.Property<ulong>("userId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("id");

                    b.ToTable("Ecomments");
                });

            modelBuilder.Entity("Infrastructure.Episode", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<ulong>("SeriesId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("episodes");
                });

            modelBuilder.Entity("Infrastructure.Language", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("File")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("Infrastructure.ModerationRole", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<int>("PermisionLevel")
                        .HasColumnType("int");

                    b.Property<ulong>("RoleId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("id");

                    b.ToTable("ModerationRoles");
                });

            modelBuilder.Entity("Infrastructure.Punishment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Finished")
                        .HasColumnType("tinyint(1)");

                    b.Property<ulong>("GuidId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("ModeratorId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Reason")
                        .HasColumnType("longtext");

                    b.Property<string>("TypeOfPunishment")
                        .HasColumnType("longtext");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("id");

                    b.ToTable("Punishment");
                });

            modelBuilder.Entity("Infrastructure.Series", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("image")
                        .HasColumnType("longblob");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.Property<int>("permisionLvl")
                        .HasColumnType("int");

                    b.Property<string>("tags")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("series");
                });

            modelBuilder.Entity("Infrastructure.SeriesComments", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("comment")
                        .HasColumnType("longtext");

                    b.Property<int>("negative")
                        .HasColumnType("int");

                    b.Property<int>("positive")
                        .HasColumnType("int");

                    b.Property<ulong>("userId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("id");

                    b.ToTable("Scomments");
                });

            modelBuilder.Entity("Infrastructure.SeriesRating", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<double>("raiting")
                        .HasColumnType("double");

                    b.Property<ulong>("seriesId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("userId")
                        .HasColumnType("bigint unsigned");

                    b.HasKey("id");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("Infrastructure.Server", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("AutoRoleId")
                        .HasColumnType("bigint unsigned");

                    b.Property<byte[]>("Background")
                        .HasColumnType("longblob");

                    b.Property<string>("Banned")
                        .HasColumnType("longtext");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Goodbye")
                        .HasColumnType("longtext");

                    b.Property<int?>("Langueid")
                        .HasColumnType("int");

                    b.Property<ulong>("Logs")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Prefix")
                        .HasColumnType("longtext");

                    b.Property<ulong>("Tmute")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("Vmute")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Welcome")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Langueid");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Infrastructure.User", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("DiscordAccountId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("avatar")
                        .HasColumnType("longblob");

                    b.Property<string>("name")
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("pass")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Infrastructure.WathedEpisode", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SeriesId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("wathedSeries");
                });

            modelBuilder.Entity("Infrastructure.Webhooks", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("IdServer")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Webhooks");
                });

            modelBuilder.Entity("Infrastructure.player", b =>
                {
                    b.Property<ulong>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("EpisodeID")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("link")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("players");
                });

            modelBuilder.Entity("Infrastructure.Server", b =>
                {
                    b.HasOne("Infrastructure.Language", "Langue")
                        .WithMany()
                        .HasForeignKey("Langueid");

                    b.Navigation("Langue");
                });
#pragma warning restore 612, 618
        }
    }
}
