using Xunit;
using PaymentGateway.Helpers;
using FluentAssertions;
using System;

namespace UnitTests
{
    public class PaymentUtilityTests
    {
        [Fact]
        public void Can_Mask_Credit_Card_No()
        {
            var outcome = PaymentUtility.MaskCreditCardNo("4715246426464247");

            outcome.Should().Be("************4247");
        }

        [Fact]
        public void Cannot_Mask_Credit_Card_With_Short_Values()
        {
            Action act = () => PaymentUtility.MaskCreditCardNo("387");

            act.Should().Throw<InvalidOperationException>();
        }
    }
}