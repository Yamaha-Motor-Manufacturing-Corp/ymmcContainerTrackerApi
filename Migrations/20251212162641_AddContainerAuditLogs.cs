using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YmmcContainerTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddContainerAuditLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "RtrnCotnr");

            migrationBuilder.CreateTable(
                name: "ContainerAuditLogs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedFields = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerAuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RtrnCotnr_Main",
                schema: "RtrnCotnr",
                columns: table => new
                {
                    Item_No = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Packing_Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Prefix_Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Container_Number = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Outside_Length = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Outside_Width = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Outside_Height = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Collapsed_Height = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Pack_Quantity = table.Column<int>(type: "int", nullable: true),
                    Alternate_ID = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RtrnCotnr_Main", x => x.Item_No);
                });

            migrationBuilder.CreateTable(
                name: "RtrnCotnr_Stage",
                schema: "RtrnCotnr",
                columns: table => new
                {
                    Item_No = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Packing_Code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Prefix_Code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Container_Number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Outside_Length = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Outside_Width = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Outside_Height = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Collapsed_Height = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Weight = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Pack_Quantity = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Alternate_ID = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Username);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerAuditLogs_Action",
                schema: "dbo",
                table: "ContainerAuditLogs",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerAuditLogs_ItemNo",
                schema: "dbo",
                table: "ContainerAuditLogs",
                column: "ItemNo");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerAuditLogs_Timestamp",
                schema: "dbo",
                table: "ContainerAuditLogs",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerAuditLogs_Username",
                schema: "dbo",
                table: "ContainerAuditLogs",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_RtrnCotnr_Main_Item_No",
                schema: "RtrnCotnr",
                table: "RtrnCotnr_Main",
                column: "Item_No",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerAuditLogs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RtrnCotnr_Main",
                schema: "RtrnCotnr");

            migrationBuilder.DropTable(
                name: "RtrnCotnr_Stage",
                schema: "RtrnCotnr");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dbo");
        }
    }
}
