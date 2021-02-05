using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class UpdatedTagCategoryActual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogTagApplied",
                columns: table => new
                {
                    BlogTagAppliedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    BlogTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTagApplied", x => x.BlogTagAppliedId);
                    table.ForeignKey(
                        name: "FK_BlogTagApplied_BlogPost_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPost",
                        principalColumn: "BlogPostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTagApplied_BlogTag_BlogTagId",
                        column: x => x.BlogTagId,
                        principalTable: "BlogTag",
                        principalColumn: "BlogTagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagApplied_BlogPostId",
                table: "BlogTagApplied",
                column: "BlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagApplied_BlogTagId",
                table: "BlogTagApplied",
                column: "BlogTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTagApplied");
        }
    }
}
