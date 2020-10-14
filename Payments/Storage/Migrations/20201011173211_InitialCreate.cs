using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payments.Storage.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyCodes");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "Payments");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "CurrencyCodes",
               columns: table => new
               {
                   CodeId = table.Column<Guid>(nullable: false),
                   Code = table.Column<string>(maxLength: 5, nullable: false),
                   Currency = table.Column<string>(maxLength: 300, nullable: true),
                   TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_CurrencyCodes", x => x.CodeId);
               });


            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    MerchantId = table.Column<Guid>(nullable: false),
                    ApiKey = table.Column<string>(maxLength: 100, nullable: true),
                    MerchantName = table.Column<string>(maxLength: 200, nullable: true),                   
                    IsDisabled = table.Column<bool>(nullable: false),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.MerchantId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(nullable: false),                    
                    CreateDate = table.Column<DateTime>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    State = table.Column<string>(maxLength: 200, nullable: false),
                    PaymentAmount_Amount = table.Column<decimal>(nullable: true),
                    PaymentAmount_CurrencyCode = table.Column<string>(maxLength: 5, nullable: true),
                    CreditCardInfo_CardNo = table.Column<string>(maxLength: 100, nullable: true),
                    CreditCardInfo_ExpiryDate_Month = table.Column<int>(nullable: true),
                    CreditCardInfo_ExpiryDate_Year = table.Column<int>(nullable: true),
                    CreditCardInfo_CVV = table.Column<int>(maxLength: 5, nullable: true),
                    PaymentOrderId = table.Column<string>(nullable: true),
                    MerchantId = table.Column<Guid>(nullable: false),
                    ShopperId = table.Column<string>(maxLength: 200, nullable: true),
                    TimeStamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });
        }
    }
}
