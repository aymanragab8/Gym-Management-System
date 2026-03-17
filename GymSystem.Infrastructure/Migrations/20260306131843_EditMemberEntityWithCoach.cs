using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditMemberEntityWithCoach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "CoachId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_CoachId",
                table: "Members",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Members_CoachId",
                table: "Members",
                column: "CoachId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Members_CoachId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_CoachId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
