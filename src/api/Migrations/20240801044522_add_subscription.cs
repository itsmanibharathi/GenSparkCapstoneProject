using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class add_subscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionPlanDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionPlanPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubscriptionPlanDuration = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanDurationType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.SubscriptionPlanId);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriptionPlans",
                columns: table => new
                {
                    UserSubscriptionPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriptionPlans", x => x.UserSubscriptionPlanId);
                    table.ForeignKey(
                        name: "FK_UserSubscriptionPlans_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "SubscriptionPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscriptionPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptionPlans_SubscriptionPlanId",
                table: "UserSubscriptionPlans",
                column: "SubscriptionPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptionPlans_UserId",
                table: "UserSubscriptionPlans",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSubscriptionPlans");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
