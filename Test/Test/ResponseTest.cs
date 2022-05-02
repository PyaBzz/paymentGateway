using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class ResponseTest
    {
        private readonly Mock<IRequestable> requestMocker = new Mock<IRequestable>();
        private IRequestable requestMock => requestMocker.Object; //todo: drop Mock from all mock object names
        public ResponseTest()
        {
        }
        //todo: Randomise parameters

        [Fact]
        public void Create_SetsStatusToInvalid_IfRequestIsInvalid()
        {
            requestMocker.SetupGet(x => x.Id).Returns(13);
            requestMocker.SetupGet(x => x.IsValid).Returns(false);

            var result = new Response(requestMock);
            Assert.Equal(Status.Invalid, result.Status);
        }

        [Fact]
        public void Create_SetsStatusToInvalid_IfRequestIsInvalid_RegardlessOfSuccess()
        {
            requestMocker.SetupGet(x => x.Id).Returns(13);
            requestMocker.SetupGet(x => x.IsValid).Returns(false);

            requestMocker.SetupGet(x => x.IsSuccess).Returns(true);
            var result0 = new Response(requestMock);

            requestMocker.SetupGet(x => x.IsSuccess).Returns(false);
            var result1 = new Response(requestMock);

            Assert.Equal(result0.Status, result1.Status);
        }

        [Fact]
        public void Create_SetsStatusToSuccess_IfRequestIsSuccess()
        {
            requestMocker.SetupGet(x => x.Id).Returns(13);
            requestMocker.SetupGet(x => x.IsValid).Returns(true);
            requestMocker.SetupGet(x => x.IsSuccess).Returns(true);
            var result = new Response(requestMock);
            Assert.Equal(Status.Success, result.Status);
        }

        [Fact]
        public void Create_SetsStatusToDeclined_IfRequestIsNotSuccess()
        {
            requestMocker.SetupGet(x => x.Id).Returns(13);
            requestMocker.SetupGet(x => x.IsValid).Returns(true);
            requestMocker.SetupGet(x => x.IsSuccess).Returns(false);
            var result = new Response(requestMock);
            Assert.Equal(Status.Declined, result.Status);
        }
    }
}