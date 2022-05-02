using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class CardTest
    {
        private string numberDummy = "1234-5678-8765-4321";
        private int cvvDummy = 777;
        private Mock<IDate> dateMocker = new Mock<IDate>();
        private IDate dateMock => dateMocker.Object;

        [Fact]
        public void Expires_IfDateIsPassed()
        {
            dateMocker.SetupGet(x => x.IsPassed).Returns(true);
            var card = Card.Create(null, dateMock, cvvDummy, Currency.GBP);
            Assert.True(card.IsExpired);
        }

        [Fact]
        public void DoesNotExpire_IfDateNotPassed()
        {
            dateMocker.SetupGet(x => x.IsPassed).Returns(false);
            var card = Card.Create(null, dateMock, cvvDummy, Currency.GBP);
            Assert.False(card.IsExpired);
        }

        [Fact(Skip = "todo")]
        public void Create_ValidatesCardNumber()
        {
        }

        [Fact(Skip = "todo")]
        public void Create_ValidatesCvv()
        {
        }
    }
}
