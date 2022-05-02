using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class HubTest
    {
        private readonly Hub instance;
        private Mock<IRequestable> requestMocker = new Mock<IRequestable>();
        private IRequestable requestMock => requestMocker.Object;

        public HubTest()
        {
            var bank = new FakeBank();
            instance = new Hub(bank);
        }

        [Fact]
        public void Forward_ReturnsInvalidStatus_IfInvalidRequest()
        {
            requestMocker.SetupGet(x => x.IsValid).Returns(false);
            var result = instance.Forward(requestMock);
            Assert.Equal(RequestStatus.Invalid, result);
        }

        [Fact]
        public void Forward_OnlyAcceptsFreshRequests()
        {
            requestMocker.SetupGet(x => x.Status).Returns(RequestStatus.Invalid);
            Assert.Throws<ArgumentException>(
                () => instance.Forward(requestMock)
            );
            requestMocker.SetupGet(x => x.Status).Returns(RequestStatus.Declined);
            Assert.Throws<ArgumentException>(
                () => instance.Forward(requestMock)
            );
            requestMocker.SetupGet(x => x.Status).Returns(RequestStatus.Pending);
            Assert.Throws<ArgumentException>(
                () => instance.Forward(requestMock)
            );
            requestMocker.SetupGet(x => x.Status).Returns(RequestStatus.Success);
            Assert.Throws<ArgumentException>(
                () => instance.Forward(requestMock)
            );
        }

        [Fact]
        public void Forward_SavesRequest()
        {

        }
    }
}