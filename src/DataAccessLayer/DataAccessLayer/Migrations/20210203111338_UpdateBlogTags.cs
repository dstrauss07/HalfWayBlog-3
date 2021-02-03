using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class UpdateBlogTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_BlogPost_BlogPostId",
                table: "BlogTag");

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "BlogTag",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_BlogPost_BlogPostId",
                table: "BlogTag",
                column: "BlogPostId",
                principalTable: "BlogPost",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_BlogPost_BlogPostId",
                table: "BlogTag");

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "BlogTag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_BlogPost_BlogPostId",
                table: "BlogTag",
                column: "BlogPostId",
                principalTable: "BlogPost",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
