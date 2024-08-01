using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class add_UserPropertyInteraction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPropertyInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    InteractionType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPropertyInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPropertyInteractions_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPropertyInteractions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 101,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 1, 16, 2, 5, 802, DateTimeKind.Local).AddTicks(3544));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 102,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 1, 16, 2, 5, 802, DateTimeKind.Local).AddTicks(3561));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 103,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 1, 16, 2, 5, 802, DateTimeKind.Local).AddTicks(3565));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 104,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 1, 16, 2, 5, 802, DateTimeKind.Local).AddTicks(3567));

            migrationBuilder.CreateIndex(
                name: "IX_UserPropertyInteractions_PropertyId",
                table: "UserPropertyInteractions",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPropertyInteractions_UserId",
                table: "UserPropertyInteractions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPropertyInteractions");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 101,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 1, 10, 41, 44, 519, DateTimeKind.Local).AddTicks(5676));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 102,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 1, 10, 41, 44, 519, DateTimeKind.Local).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 103,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 1, 10, 41, 44, 519, DateTimeKind.Local).AddTicks(5692));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 104,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 1, 10, 41, 44, 519, DateTimeKind.Local).AddTicks(5738));
        }
    }
}
