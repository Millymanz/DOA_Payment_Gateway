using Xunit;
using Payments.Banking;
using Payments;
using System;
using Payments.Banking.Interfaces;
using FluentAssertions;
using System.Collections.Generic;
using Payments.DTO.Models;
using Payments.Storage.Repositories.Interfaces;
using UnitTests.Mock;

namespace UnitTests
{
    public class PaymentTests
    {

        IBankService _bankService;

        ICurrencyCodeRepository _currencyCodeRepository;

        public PaymentTests()
        {
            _bankService = new BankServiceMock(1.0);


            var currencyCodes = new List<CurrencyCode>();
            currencyCodes.Add(new CurrencyCode("EUR", "Euro"));
            currencyCodes.Add(new CurrencyCode("AUD", "Australian dollar"));
            currencyCodes.Add(new CurrencyCode("GBP", "Pound sterling"));

            _currencyCodeRepository = new MockCurrencyCodeRepository(currencyCodes);
        }

        [Fact]
        public async void Can_Make_Payment_With_Valid_Card()
        {
            var cred = new CreditCardInfo("4539438639352560", new ExpiryDate(10, 2021), 587);

            var merchant = new MockMerchant();               
            merchant.MerchantName = "Lockdown Technologies";
            merchant.MerchantId = Guid.NewGuid();

            var requestModel = new PaymentRequestModel();
            requestModel.CurrencyCode = "GBP";

            var paymentAmount = new PaymentAmount(requestModel.Amount, requestModel.CurrencyCode);

            var payment = Payment.Create(paymentAmount, cred, merchant.MerchantId, requestModel.ShopperId);

            var success = await payment.TryPay(_bankService, _currencyCodeRepository);

            success.Should().BeTrue();
            payment.State.Should().Be(PaymentState.Success);
        }

        [Fact]
        public async void Cannot_Make_Payment_With_Extreme_Card_Values()
        {
            var cred = new CreditCardInfo("0", new ExpiryDate(10, 2021), 587);
            var paymentAmount = new PaymentAmount(2581, "EUR");

            var payment = Payment.Create(paymentAmount, cred, Guid.NewGuid(), "testiddatashop_xxxxx");
            var success = await payment.TryPay(_bankService, _currencyCodeRepository);

            success.Should().BeFalse();
            payment.State.Should().Be(PaymentState.Failed);



            cred = new CreditCardInfo("1089621717951721798766516721957127951762172197572171257197217745464", new ExpiryDate(10, 2021), 587);
            paymentAmount = new PaymentAmount(2581, "EUR");

            payment = Payment.Create(paymentAmount, cred, Guid.NewGuid(), "testiddatashop_xxxxx");
            success = await payment.TryPay(_bankService, _currencyCodeRepository);

            success.Should().BeFalse();
            payment.State.Should().Be(PaymentState.Failed);



            cred = new CreditCardInfo("5302610734261183", new ExpiryDate(8, 1970), 587);
            paymentAmount = new PaymentAmount(2581, "EUR");

            payment = Payment.Create(paymentAmount, cred, Guid.NewGuid(), "testiddatashop_xxxxx");
            success = await payment.TryPay(_bankService, _currencyCodeRepository);

            success.Should().BeFalse();
            payment.State.Should().Be(PaymentState.Failed);



            cred = new CreditCardInfo("5302610734261183", new ExpiryDate(99, 2025), 587);
            paymentAmount = new PaymentAmount(2581, "EUR");

            payment = Payment.Create(paymentAmount, cred, Guid.NewGuid(), "testiddatashop_xxxxx");
            success = await payment.TryPay(_bankService, _currencyCodeRepository);

            success.Should().BeFalse();
            payment.State.Should().Be(PaymentState.Failed);


            cred = new CreditCardInfo("5302610734261183", new ExpiryDate(8, 2022), 444);
            paymentAmount = new PaymentAmount(-9852142, "GBP");

            payment = Payment.Create(paymentAmount, cred, Guid.NewGuid(), "testiddatashop_xxxxx");
            success = await payment.TryPay(_bankService, _currencyCodeRepository);

            success.Should().BeFalse();
            payment.State.Should().Be(PaymentState.Failed);
        }

        [Fact]
        public async void Cannot_Make_Payment_With_Unlisted_CurrencyCodes()
        {
            var cred = new CreditCardInfo("0", new ExpiryDate(10, 2021), 587);
            var paymentAmount = new PaymentAmount(2581, "XXX");

            var payment = Payment.Create(paymentAmount, cred, Guid.NewGuid(), "testiddatashop_xxxxx");
            var success = await payment.TryPay(_bankService, _currencyCodeRepository);

            success.Should().BeFalse();
            payment.State.Should().Be(PaymentState.Failed);
        }
    }
}