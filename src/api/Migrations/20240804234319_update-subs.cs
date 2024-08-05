using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class updatesubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscriptionPlanDurationType",
                table: "UserSubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 101,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 5, 13, 18, 875, DateTimeKind.Local).AddTicks(2912));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 102,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 5, 13, 18, 875, DateTimeKind.Local).AddTicks(2924));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 103,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 5, 13, 18, 875, DateTimeKind.Local).AddTicks(2926));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 104,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 5, 13, 18, 875, DateTimeKind.Local).AddTicks(2927));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionPlanDurationType",
                table: "UserSubscriptionPlans");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 101,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 4, 58, 48, 662, DateTimeKind.Local).AddTicks(3496));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 102,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 4, 58, 48, 662, DateTimeKind.Local).AddTicks(3511));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 103,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 4, 58, 48, 662, DateTimeKind.Local).AddTicks(3515));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 104,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 5, 4, 58, 48, 662, DateTimeKind.Local).AddTicks(3516));
        }
    }
}
