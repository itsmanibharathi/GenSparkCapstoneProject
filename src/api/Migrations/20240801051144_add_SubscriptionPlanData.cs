using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class add_SubscriptionPlanData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "SubscriptionPlans",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "SubscriptionPlanId", "CreatedAt", "IsActive", "SubscriptionPlanDescription", "SubscriptionPlanDuration", "SubscriptionPlanDurationType", "SubscriptionPlanName", "SubscriptionPlanPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { 101, new DateTime(2024, 8, 1, 10, 41, 44, 519, DateTimeKind.Local).AddTicks(5676), true, "New User View Contact Subscription Plan", 3, 1, "Free Trial On Contact Me", 0m, null },
                    { 102, new DateTime(2024, 8, 1, 10, 41, 44, 519, DateTimeKind.Local).AddTicks(5689), true, "View Owner info", 2, 0, "Free Trial On View Owner info", 0m, null },
                    { 103, new DateTime(2024, 8, 1, 10, 41, 44, 519, DateTimeKind.Local).AddTicks(5692), true, "Share your contact info to the Owner", 30, 1, "Contact Me", 100m, null },
                    { 104, new DateTime(2024, 8, 1, 10, 41, 44, 519, DateTimeKind.Local).AddTicks(5738), true, "View Owner info for 10 Property", 10, 0, "View Owner info", 100m, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "SubscriptionPlans",
                keyColumn: "SubscriptionPlanId",
                keyValue: 104);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "SubscriptionPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
