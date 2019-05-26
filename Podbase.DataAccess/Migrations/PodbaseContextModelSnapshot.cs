﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Podbase.DataAccess;

namespace Podbase.DataAccess.Migrations
{
    [DbContext(typeof(PodbaseContext))]
    partial class PodbaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Podbase.Model.Account", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutMe");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FirstName = "Lars",
                            LastName = "Haugeli",
                            Password = "Test123",
                            Username = "larshaugeli"
                        },
                        new
                        {
                            UserId = 2,
                            FirstName = "Sansa",
                            LastName = "Stark",
                            Password = "Test123",
                            Username = "sansastark"
                        },
                        new
                        {
                            UserId = 3,
                            FirstName = "Arya",
                            LastName = "Stark",
                            Password = "Test123",
                            Username = "aryastark"
                        },
                        new
                        {
                            UserId = 4,
                            FirstName = "Ned",
                            LastName = "Stark",
                            Password = "Test123",
                            Username = "nedstark"
                        },
                        new
                        {
                            UserId = 5,
                            FirstName = "Bran",
                            LastName = "Stark",
                            Password = "Test123",
                            Username = "branstark"
                        });
                });

            modelBuilder.Entity("Podbase.Model.Friend", b =>
                {
                    b.Property<int>("ConnectionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FriendId");

                    b.Property<int>("UserId");

                    b.HasKey("ConnectionId");

                    b.ToTable("Friends");

                    b.HasData(
                        new
                        {
                            ConnectionId = 1,
                            FriendId = 2,
                            UserId = 1
                        },
                        new
                        {
                            ConnectionId = 2,
                            FriendId = 3,
                            UserId = 1
                        },
                        new
                        {
                            ConnectionId = 3,
                            FriendId = 4,
                            UserId = 1
                        },
                        new
                        {
                            ConnectionId = 4,
                            FriendId = 3,
                            UserId = 2
                        },
                        new
                        {
                            ConnectionId = 5,
                            FriendId = 5,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Podbase.Model.Podcast", b =>
                {
                    b.Property<int>("PodcastId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Creator");

                    b.Property<string>("Description");

                    b.Property<string>("Genre");

                    b.Property<string>("Name");

                    b.Property<int>("Rating");

                    b.Property<int>("UserId");

                    b.HasKey("PodcastId");

                    b.ToTable("Podcasts");

                    b.HasData(
                        new
                        {
                            PodcastId = 1,
                            Creator = "NRK",
                            Description = "Funny podcast",
                            Genre = "Comedy",
                            Name = "Radioresepsjonen",
                            Rating = 0,
                            UserId = 1
                        },
                        new
                        {
                            PodcastId = 2,
                            Creator = "P4",
                            Description = "Funny podcast",
                            Genre = "Comedy",
                            Name = "Misjonen",
                            Rating = 0,
                            UserId = 1
                        },
                        new
                        {
                            PodcastId = 3,
                            Creator = "P4",
                            Description = "Funny podcast",
                            Genre = "Comedy",
                            Name = "Misjonen",
                            Rating = 0,
                            UserId = 1
                        },
                        new
                        {
                            PodcastId = 4,
                            Creator = "This American Life",
                            Description = "Podcast about a murder",
                            Genre = "True Crime",
                            Name = "Serial",
                            Rating = 0,
                            UserId = 2
                        },
                        new
                        {
                            PodcastId = 5,
                            Creator = "This American Life",
                            Description = "Podcast about a murder",
                            Genre = "True Crime",
                            Name = "Serial",
                            Rating = 0,
                            UserId = 2
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
