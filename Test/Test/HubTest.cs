using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class HubTest
    {
        private readonly Hub instance;
        private readonly Mock<IRequestable> requestMocker = new Mock<IRequestable>();
        private IRequestable requestMock => requestMocker.Object; //todo: drop Mock from all mock object names
        private readonly Mock<IBank> bankMocker = new Mock<IBank>();
        private IBank bankMock => bankMocker.Object;

        public HubTest()
        {
            instance = new Hub(bankMock);
        }

        [Fact]
        public void Process_ReturnsInvalidStatus_IfRequestIsInvalid()
        {
            requestMocker.SetupGet(x => x.IsValid).Returns(false);
            var response = instance.Process(requestMock);
            Assert.Equal(Status.Invalid, response.Status);
        }

        // [Fact]
        // public void Process_OnlyAcceptsFreshRequests()
        // {
        //     requestMocker.SetupGet(x => x.Status).Returns(Status.Invalid);
        //     Assert.Throws<ArgumentException>(
        //         () => instance.Process(requestMock)
        //     );
        //     requestMocker.SetupGet(x => x.Status).Returns(Status.Declined);
        //     Assert.Throws<ArgumentException>(
        //         () => instance.Process(requestMock)
        //     );
        //     requestMocker.SetupGet(x => x.Status).Returns(Status.Pending);
        //     Assert.Throws<ArgumentException>(
        //         () => instance.Process(requestMock)
        //     );
        //     requestMocker.SetupGet(x => x.Status).Returns(Status.Success);
        //     Assert.Throws<ArgumentException>(
        //         () => instance.Process(requestMock)
        //     );
        // }

        // [Fact]
        // public void Process_ForwardsToBank()
        // {

        // }
    }
}