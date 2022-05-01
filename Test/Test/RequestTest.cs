using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class RequestTest
    {
        private const decimal MIN_AMOUNT = 0.5m; //doc: Assumption
        private const decimal MAX_AMOUNT = 500m; //doc: Assumption
        private int merchantIdDummy;
        private decimal amountDummy;
        private Mock<ICard> cardMocker = new Mock<ICard>();
        private ICard cardMock => cardMocker.Object;
        private Mock<IRepository<Request>> repoMocker = new Mock<IRepository<Request>>();
        private IRepository<Request> repoMock => repoMocker.Object;
        private int requestIdDummy;
        public RequestTest()
        {
            Random rng = new Random(); //todo: randomise dummy values
            merchantIdDummy = rng.Next(0, int.MaxValue - 200);
            requestIdDummy = rng.Next(0, int.MaxValue - 200);
            amountDummy = MIN_AMOUNT + new decimal(rng.NextDouble()) * (MAX_AMOUNT - MIN_AMOUNT);
        }

        [Fact]
        public void Create_InitialisesState()
        {
            var instance = Request.Create(merchantIdDummy, cardMock, amountDummy);
            Assert.Null(instance.Id);
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

        [Fact]
        public void Save_SetsId_IfNull()
        {
            var instance = Request.Create(merchantIdDummy, cardMock, amountDummy);
            repoMocker.Setup(x => x.Save(instance)).Returns(requestIdDummy);
            instance.Save(repoMock);
            Assert.Equal(requestIdDummy, instance.Id);
        }

        [Fact]
        public void Save_RetainsId_IfNotNull()
        {
            var instance = Request.Create(merchantIdDummy, cardMock, amountDummy);
            repoMocker.Setup(x => x.Save(instance)).Returns(requestIdDummy);
            instance.Save(repoMock);
            Assert.Equal(requestIdDummy, instance.Id);
            repoMocker.Setup(x => x.Save(instance)).Returns(requestIdDummy + 1);
            instance.Save(repoMock);
            Assert.Equal(requestIdDummy, instance.Id);
        }
    }
}