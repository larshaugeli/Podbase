using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podbase.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountPodcasts",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    PodcastId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPodcasts", x => x.PodcastId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Podcasts",
                columns: table => new
                {
                    PodcastId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Creator = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    AccountPodcastPodcastId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podcasts", x => x.PodcastId);
                    table.ForeignKey(
                        name: "FK_Podcasts_AccountPodcasts_AccountPodcastPodcastId",
                        column: x => x.AccountPodcastPodcastId,
                        principalTable: "AccountPodcasts",
                        principalColumn: "PodcastId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccountPodcasts",
                columns: new[] { "PodcastId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "UserId", "FirstName", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "Lars", "Haugeli", "Sofimjau123", "larshaugeli" },
                    { 2, "Sofi", "Mjaupus", "Sofimjau123", "sofimjaupus" }
                });

            migrationBuilder.InsertData(
                table: "Podcasts",
                columns: new[] { "PodcastId", "AccountPodcastPodcastId", "Creator", "Description", "Genre", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, null, "NRK", "Gøy", "Humor", "Radioresepsjonen", 0 },
                    { 2, null, "P4", "Hehe", "Humor", "Misjonen", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Podcasts_AccountPodcastPodcastId",
                table: "Podcasts",
                column: "AccountPodcastPodcastId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Podcasts");

            migrationBuilder.DropTable(
                name: "AccountPodcasts");
        }
    }
}
