using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilySync.Registry.Migrations
{
    /// <inheritdoc />
    public partial class Members_Add_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memebers_Families_FamilyId",
                table: "Memebers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memebers",
                table: "Memebers");

            migrationBuilder.RenameTable(
                name: "Memebers",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_Memebers_FamilyId",
                table: "Members",
                newName: "IX_Members_FamilyId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Members",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                columns: new[] { "MemberId", "FamilyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Families_FamilyId",
                table: "Members",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Families_FamilyId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Memebers");

            migrationBuilder.RenameIndex(
                name: "IX_Members_FamilyId",
                table: "Memebers",
                newName: "IX_Memebers_FamilyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memebers",
                table: "Memebers",
                columns: new[] { "MemberId", "FamilyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Memebers_Families_FamilyId",
                table: "Memebers",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
