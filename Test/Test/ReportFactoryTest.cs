using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class ReportFactoryTest
    {
        //todo: Randomise parameters
        private const string OBSCURE_PART = "****-****-****"; //doc: Rely on separate values for testing
        private const string BLANK_CARD_NO = "-";
        private const int requestIdDummy = 13;
        private const int merchantIdDummy = 1;
        private readonly Mock<IRequestable> requestMocker = new Mock<IRequestable>();
        private IRequestable requestMock => requestMocker.Object;
        private Mock<IRepository<IRequestable>> repoMocker = new Mock<IRepository<IRequestable>>();
        private IRepository<IRequestable> repoMock => repoMocker.Object;
        private ReportFactory instance;
        public ReportFactoryTest()
        {
            requestMocker.SetupGet(x => x.Id).Returns(requestIdDummy);
            requestMocker.SetupGet(x => x.MerchantId).Returns(merchantIdDummy);
            requestMocker.SetupGet(x => x.Amount).Returns(0.7m);
            requestMocker.SetupGet(x => x.Card.Number).Returns("1234-5678-8765-4321");
            requestMocker.SetupGet(x => x.Card.Cvv).Returns(777);
            requestMocker.SetupGet(x => x.Card.Currency).Returns(Currency.GBP);
            requestMocker.SetupGet(x => x.Card.Expiry.Year).Returns(2023);
            requestMocker.SetupGet(x => x.Card.Expiry.Month).Returns(5);

            repoMocker.Setup(x => x.Get(It.IsAny<int>())).Returns(requestMock);
            instance = new ReportFactory(repoMock);
        }

        [Fact]
        public void Make_SetsStatusToInvalid_IfRequestIsInvalid()
        {
            requestMocker.SetupGet(x => x.IsValid).Returns(false);
            var query = new Query { MerchantId = merchantIdDummy, RequestId = requestIdDummy };
            var result = instance.Make(query);
            Assert.Equal(Status.Invalid, result.Status);
        }

        [Fact]
        public void Make_SetsStatusToInvalid_IfRequestIsInvalid_RegardlessOfSuccess()
        {
            requestMocker.SetupGet(x => x.IsValid).Returns(false);
            var query = new Query { MerchantId = merchantIdDummy, RequestId = requestIdDummy };

            requestMocker.SetupGet(x => x.IsSuccess).Returns(true);
            var result0 = instance.Make(query);

            requestMocker.SetupGet(x => x.IsSuccess).Returns(false);
            var result1 = instance.Make(query);

            Assert.Equal(result0.Status, result1.Status);
        }

        [Fact]
        public void Make_SetsStatusToSuccess_IfRequestIsSuccess()
        {
            requestMocker.SetupGet(x => x.IsValid).Returns(true);
            requestMocker.SetupGet(x => x.IsSuccess).Returns(true);
            var query = new Query { MerchantId = merchantIdDummy, RequestId = requestIdDummy };
            var result = instance.Make(query);
            Assert.Equal(Status.Success, result.Status);
        }

        [Fact]
        public void Make_SetsStatusToDeclined_IfRequestIsNotSuccess()
        {
            requestMocker.SetupGet(x => x.IsValid).Returns(true);
            requestMocker.SetupGet(x => x.IsSuccess).Returns(false);
            var query = new Query { MerchantId = merchantIdDummy, RequestId = requestIdDummy };
            var result = instance.Make(query);
            Assert.Equal(Status.Declined, result.Status);
        }

        [Fact]
        public void Make_ObscuresCardNumber()
        {
            requestMocker.SetupGet(x => x.IsValid).Returns(true);
            requestMocker.SetupGet(x => x.IsSuccess).Returns(true);
            var query = new Query { MerchantId = merchantIdDummy, RequestId = requestIdDummy };
            var result = instance.Make(query);
            Assert.Equal(result.Card_Number.Substring(0, 14), OBSCURE_PART);
        }

        [Fact]
        public void Make_ReturnsBlank_IfRequestNotFound()
        {
            repoMocker.Setup(x => x.Get(It.IsAny<int>())).Returns((IRequestable)null);
            var query = new Query { MerchantId = merchantIdDummy + 1, RequestId = requestIdDummy };
            var result = instance.Make(query);
            Assert.Equal(result.Card_Number, BLANK_CARD_NO);
        }

        [Fact]
        public void Make_ReturnsBlank_IfRequestNotBelongToMerchant()
        {
            requestMocker.SetupGet(x => x.IsValid).Returns(true);
            requestMocker.SetupGet(x => x.IsSuccess).Returns(true);
            var query = new Query { MerchantId = merchantIdDummy + 1, RequestId = requestIdDummy };
            var result = instance.Make(query);
            Assert.Equal(result.Card_Number, BLANK_CARD_NO);
        }
    }
}