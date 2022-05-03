using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class ReportTest
    {
        private readonly Mock<IRequestable> requestMocker = new Mock<IRequestable>();
        private IRequestable requestMock => requestMocker.Object; //todo: drop Mock from all mock object names
        public ReportTest()
        {
            //todo: Randomise parameters
            requestMocker.SetupGet(x => x.MerchantId).Returns(1);
            requestMocker.SetupGet(x => x.Amount).Returns(0.7m);
            requestMocker.SetupGet(x => x.Card.Number).Returns("1234-5678-8765-4321");
            requestMocker.SetupGet(x => x.Card.Cvv).Returns(777);
            requestMocker.SetupGet(x => x.Card.Currency).Returns(Currency.GBP);
            requestMocker.SetupGet(x => x.Card.Expiry.Year).Returns(2023);
            requestMocker.SetupGet(x => x.Card.Expiry.Month).Returns(5);
        }
        //todo: Randomise parameters

        [Fact]
        public void Create_SetsStatusToInvalid_IfRequestIsInvalid()
        {
            requestMocker.SetupGet(x => x.Id).Returns(13);
            requestMocker.SetupGet(x => x.IsValid).Returns(false);

            var result = Report.Create(requestMock);
            Assert.Equal(Status.Invalid, result.Status);
        }

        [Fact]
        public void Create_SetsStatusToInvalid_IfRequestIsInvalid_RegardlessOfSuccess()
        {
            requestMocker.SetupGet(x => x.Id).Returns(13);
            requestMocker.SetupGet(x => x.IsValid).Returns(false);

            requestMocker.SetupGet(x => x.IsSuccess).Returns(true);
            var result0 = Report.Create(requestMock);

            requestMocker.SetupGet(x => x.IsSuccess).Returns(false);
            var result1 = Report.Create(requestMock);

            Assert.Equal(result0.Status, result1.Status);
        }

        [Fact]
        public void Create_SetsStatusToSuccess_IfRequestIsSuccess()
        {
            requestMocker.SetupGet(x => x.Id).Returns(13);
            requestMocker.SetupGet(x => x.IsValid).Returns(true);
            requestMocker.SetupGet(x => x.IsSuccess).Returns(true);
            var result = Report.Create(requestMock);
            Assert.Equal(Status.Success, result.Status);
        }

        [Fact]
        public void Create_SetsStatusToDeclined_IfRequestIsNotSuccess()
        {
            requestMocker.SetupGet(x => x.Id).Returns(13);
            requestMocker.SetupGet(x => x.IsValid).Returns(true);
            requestMocker.SetupGet(x => x.IsSuccess).Returns(false);
            var result = Report.Create(requestMock);
            Assert.Equal(Status.Declined, result.Status);
        }
    }
}