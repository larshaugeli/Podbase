using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podbase.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AboutMe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    ConnectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    FriendId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.ConnectionId);
                });

            migrationBuilder.CreateTable(
                name: "Podcasts",
                columns: table => new
                {
                    PodcastId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Creator = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podcasts", x => x.PodcastId);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "UserId", "AboutMe", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { 1, null, "Lars", "Haugeli", "Test123", "larshaugeli" },
                    { 2, null, "Sansa", "Stark", "Test123", "sansastark" },
                    { 3, null, "Arya", "Stark", "Test123", "aryastark" },
                    { 4, null, "Ned", "Stark", "Test123", "nedstark" },
                    { 5, null, "Bran", "Stark", "Test123", "branstark" }
                });

            migrationBuilder.InsertData(
                table: "Friends",
                columns: new[] { "ConnectionId", "FriendId", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 3, 1 },
                    { 3, 4, 1 },
                    { 4, 3, 2 },
                    { 5, 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "Podcasts",
                columns: new[] { "PodcastId", "Creator", "Description", "Genre", "Name", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, "NRK", "Funny podcast", "Comedy", "Radioresepsjonen", 0, 1 },
                    { 2, "P4", "Funny podcast", "Comedy", "Misjonen", 0, 1 },
                    { 3, "P4", "Funny podcast", "Comedy", "Misjonen", 0, 1 },
                    { 4, "This American Life", "Podcast about a murder", "True Crime", "Serial", 0, 2 },
                    { 5, "This American Life", "Podcast about a murder", "True Crime", "Serial", 0, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Podcasts");
        }
    }
}
