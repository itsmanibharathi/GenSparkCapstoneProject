using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class updatesub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableSellerViewCount",
                table: "UserSubscriptionPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSellerViewCount",
                table: "UserSubscriptionPlans");

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 101,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 23, 19, 14, 462, DateTimeKind.Local).AddTicks(7508));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 102,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 23, 19, 14, 462, DateTimeKind.Local).AddTicks(7522));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 103,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 23, 19, 14, 462, DateTimeKind.Local).AddTicks(7525));

            migrationBuilder.UpdateData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 104,
                column: "CreatedAt",
                value: new DateTime(2024, 8, 4, 23, 19, 14, 462, DateTimeKind.Local).AddTicks(7526));
        }
    }
}
