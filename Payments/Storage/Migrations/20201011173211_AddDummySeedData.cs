using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payments.Storage.Migrations
{
    public partial class AddDummySeedData : Migration
    {
        private static readonly Guid _dummyMerchantId1 = Guid.Parse("854935D9-6696-4352-846B-A0AAB0D1A4B2");
        private static readonly Guid _dummyMerchantId2 = Guid.Parse("10A77A08-42CD-4838-8A3E-82D931C7D858");
        private static readonly Guid _dummyMerchantId3 = Guid.Parse("889F37D6-7668-4424-92EA-F390DB3EBADC");
        private static readonly Guid _dummyMerchantId4 = Guid.Parse("4641E3C5-C986-4B51-8ED7-FE218A12E97B");
        private static readonly Guid _dummyMerchantId5 = Guid.Parse("27FB86C4-C852-4FB3-939F-286D99589A30");


        private static readonly Guid _dummyPaymentId1 = Guid.Parse("CF8A1637-5A06-4179-8BB2-7E6056A344E5");

        private static readonly Guid _dummyCodeId1 = Guid.Parse("3B0FC00E-AFE7-4D75-9B98-C455964E235C");
        private static readonly Guid _dummyCodeId2 = Guid.Parse("07A29C72-43F5-407E-94BB-D02F2C75FDE8");
        private static readonly Guid _dummyCodeId3 = Guid.Parse("B62C3EC1-A5EB-4F97-8C2F-37F19C869464");
        private static readonly Guid _dummyCodeId4 = Guid.Parse("169D797E-3C34-4AA5-900C-9B728DEC0FA9");
        private static readonly Guid _dummyCodeId5 = Guid.Parse("CB5BEEAD-5C0D-47FF-9871-7E82AF852EAC");

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Merchants", "MerchantId", _dummyMerchantId1);
            migrationBuilder.DeleteData(table: "Merchants", "MerchantId", _dummyMerchantId2);
            migrationBuilder.DeleteData(table: "Merchants", "MerchantId", _dummyMerchantId3);
            migrationBuilder.DeleteData(table: "Merchants", "MerchantId", _dummyMerchantId4);
            migrationBuilder.DeleteData(table: "Merchants", "MerchantId", _dummyMerchantId5);

            migrationBuilder.DeleteData(table: "CurrencyCodes", "CodeId", _dummyCodeId1);
            migrationBuilder.DeleteData(table: "CurrencyCodes", "CodeId", _dummyCodeId2);
            migrationBuilder.DeleteData(table: "CurrencyCodes", "CodeId", _dummyCodeId3);
            migrationBuilder.DeleteData(table: "CurrencyCodes", "CodeId", _dummyCodeId4);
            migrationBuilder.DeleteData(table: "CurrencyCodes", "CodeId", _dummyCodeId5);

            migrationBuilder.DeleteData(table: "Payments", keyColumn: "PaymentId", keyValue: _dummyPaymentId1);
        }

        private void CurrencyCodes(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.InsertData(table: "CurrencyCodes",
                columns: new[]
                {
                    "CodeId",
                    "Code",
                    "Currency"
                },
                values: new object[]
                {
                    _dummyCodeId1, "EUR", "Euro"
                });

                migrationBuilder.InsertData(table: "CurrencyCodes",
                columns: new[]
                {
                    "CodeId",
                    "Code",
                    "Currency"
                },
                values: new object[]
                {
                    _dummyCodeId2, "USD", "US Dollar"
                });

                migrationBuilder.InsertData(table: "CurrencyCodes",
                columns: new[]
                {
                    "CodeId",
                    "Code",
                    "Currency"
                },
                values: new object[]
                {
                    _dummyCodeId3, "GBP", "British Pound Sterling"
                });

                migrationBuilder.InsertData(table: "CurrencyCodes",
                columns: new[]
                {
                    "CodeId",
                    "Code",
                    "Currency"
                },
                values: new object[]
                {
                    _dummyCodeId4, "CHF", "Swiss Franc"
                });


                migrationBuilder.InsertData(table: "CurrencyCodes",
                columns: new[]
                {
                    "CodeId",
                    "Code",
                    "Currency"
                },
                values: new object[]
                {
                    _dummyCodeId5, "SGD", "Singapore Dollar"
                });
        }


        protected override void Up(MigrationBuilder migrationBuilder)
        {

            CurrencyCodes(migrationBuilder);

            migrationBuilder.InsertData(table: "Merchants",
                columns: new[]
                {
                    "MerchantId",
                    "ApiKey",
                    "MerchantName",
                    "IsDisabled"
                },
                values: new object[]
                {
                    _dummyMerchantId1, "oWq04oaqA64HZzIxXzWw", "ForeTech", false
                });

            migrationBuilder.InsertData(table: "Merchants",
                columns: new[] {
                    "MerchantId",
                    "ApiKey",
                    "MerchantName",
                    "IsDisabled"
                },
                values: new object[]
                {
                    _dummyMerchantId2, "Cxs557f7OMPM9o7iHjcH", "Crumbley Energy", false
                });

            migrationBuilder.InsertData(table: "Merchants",
                columns: new[] {
                    "MerchantId",
                    "ApiKey",
                    "MerchantName",
                    "IsDisabled"
                },
                values: new object[]
                {
                    _dummyMerchantId3, "6UEP1Q71gA6S2XO9S7dP", "Alliance Corp",  false
                });

            migrationBuilder.InsertData(table: "Merchants",
                columns: new[] {
                    "MerchantId",
                    "ApiKey",
                    "MerchantName",
                    "IsDisabled"
                },
                values: new object[]
                {
                    _dummyMerchantId4, "6UEP1Q71gA6S2XO9S7dP", "Roman Britain Limited",  false
                });

            migrationBuilder.InsertData(table: "Merchants",
                columns: new[] {
                    "MerchantId",
                    "ApiKey",
                    "MerchantName",
                    "IsDisabled"
                },
                values: new object[]
                {
                    _dummyMerchantId5, "6UEP1Q71gA6S2XO9S7dP", "Giant Cake Deliveries",  false
                });


            migrationBuilder.InsertData(table: "Payments",
                columns: new[]
                {
                    "PaymentId",
                    "CreateDate",
                    "PaymentDate",
                    "State",
                    "PaymentAmount_Amount",
                    "PaymentAmount_CurrencyCode",
                    "CreditCardInfo_CardNo",
                    "CreditCardInfo_ExpiryDate_Month",
                    "CreditCardInfo_ExpiryDate_Year",
                    "CreditCardInfo_CVV",
                    "PaymentOrderId",
                    "MerchantId",
                    "ShopperId"
                },
                values: new object[]
                {
                    _dummyPaymentId1, DateTime.UtcNow.AddHours(-3), DateTime.UtcNow.AddHours(-3).AddSeconds(10), "Success",
                    500, "EUR", "4012888888881881", 2, 2023, 889, Guid.NewGuid().ToString(), _dummyMerchantId3, "XX-2323534535-N"
                });
        }
    }
}