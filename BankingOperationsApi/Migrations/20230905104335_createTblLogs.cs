using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingOperationsApi.Migrations
{
    /// <inheritdoc />
    public partial class createTblLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayaTransfer_LOG_REQ",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    LogDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    JsonReq = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicAppId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ServiceId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicReqId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayaTransfer_LOG_REQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SatnaTransfer_LOG_REQ",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    LogDateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    JsonReq = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicAppId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ServiceId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicReqId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatnaTransfer_LOG_REQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayaTransfer_LOG_RES",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ResCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HTTPStatusCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    JsonRes = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicReqId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ReqLogId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayaTransfer_LOG_RES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayaTransfer_LOG_RES_PayaTransfer_LOG_REQ_ReqLogId",
                        column: x => x.ReqLogId,
                        principalTable: "PayaTransfer_LOG_REQ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SatnaTransfer_LOG_RES",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ResCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HTTPStatusCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    JsonRes = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PublicReqId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ReqLogId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SatnaTransfer_LOG_RES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SatnaTransfer_LOG_RES_SatnaTransfer_LOG_REQ_ReqLogId",
                        column: x => x.ReqLogId,
                        principalTable: "SatnaTransfer_LOG_REQ",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayaTransfer_LOG_REQ_Id",
                table: "PayaTransfer_LOG_REQ",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayaTransfer_LOG_RES_Id",
                table: "PayaTransfer_LOG_RES",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayaTransfer_LOG_RES_ReqLogId",
                table: "PayaTransfer_LOG_RES",
                column: "ReqLogId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SatnaTransfer_LOG_REQ_Id",
                table: "SatnaTransfer_LOG_REQ",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SatnaTransfer_LOG_RES_Id",
                table: "SatnaTransfer_LOG_RES",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SatnaTransfer_LOG_RES_ReqLogId",
                table: "SatnaTransfer_LOG_RES",
                column: "ReqLogId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayaTransfer_LOG_RES");

            migrationBuilder.DropTable(
                name: "SatnaTransfer_LOG_RES");

            migrationBuilder.DropTable(
                name: "PayaTransfer_LOG_REQ");

            migrationBuilder.DropTable(
                name: "SatnaTransfer_LOG_REQ");
        }
    }
}
