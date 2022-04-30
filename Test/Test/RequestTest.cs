using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class TransactionTest
    {
        private const decimal MIN_AMOUNT = 0.5m; //doc: Assumption
        private const int MAX_AMOUNT = 500; //doc: Assumption
        private int merchantIdDummy = 7;
        private decimal amountDummy = 1.23m;
        private Mock<ICard> cardMocker = new Mock<ICard>();
        private ICard cardMock => cardMocker.Object;

        [Fact]
        public void Create_InitialisesState()
        {
            var instance = Request.Create(merchantIdDummy, cardMock, amountDummy);
            Assert.Equal(instance.MerchantId, merchantIdDummy);
            Assert.Equal(instance.Card, cardMock);
            Assert.Equal(instance.Amount, amountDummy);
            Assert.Equal(instance.Status, RequestStatus.Received);
        }

        [Fact]
        public void IsValid_IsFalse_IfMerchantIdNegative()
        {
            var instance = Request.Create(-1, cardMock, amountDummy);
            Assert.False(instance.IsValid);
        }

        [Fact]
        public void IsValid_IsFalse_IfCardExpired()
        {
            cardMocker.SetupGet(x => x.IsExpired).Returns(true);
            var instance = Request.Create(merchantIdDummy, cardMock, amountDummy);
            Assert.False(instance.IsValid);
        }

        [Fact]
        public void IsValid_IsFalse_IfAmountBelowMinimum()
        {
            var instance = Request.Create(merchantIdDummy, cardMock, MIN_AMOUNT - 0.01m);
            Assert.False(instance.IsValid);
        }

        [Fact]
        public void IsValid_IsFalse_IfAmountExceedsMaximum()
        {
            var instance = Request.Create(merchantIdDummy, cardMock, MAX_AMOUNT + 0.01m);
            Assert.False(instance.IsValid);
        }

        [Fact]
        public void IsValid_IsTrue_Otherwise()
        {
            var instance = Request.Create(merchantIdDummy, cardMock, amountDummy);
            Assert.True(instance.IsValid);
        }
    }
}