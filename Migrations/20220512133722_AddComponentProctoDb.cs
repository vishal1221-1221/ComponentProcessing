using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComponentProcessing.API.Migrations
{
    public partial class AddComponentProctoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "componentProcessings",
                columns: table => new
                {
                    requestId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactNumber = table.Column<long>(type: "bigint", nullable: false),
                    componentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    componentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalCharges = table.Column<long>(type: "bigint", nullable: false),
                    packageCharges = table.Column<long>(type: "bigint", nullable: false),
                    deliveryCharges = table.Column<long>(type: "bigint", nullable: false),
                    dateOfDelivery = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_componentProcessings", x => x.requestId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "componentProcessings");
        }
    }
}
