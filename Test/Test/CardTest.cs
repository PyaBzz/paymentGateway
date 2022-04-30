using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class CardTest
    {
        private const string OBSCURE_PART = "****-****-****";
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

        [Fact]
        public void GetObscureClone_ReturnsNewObject()
        {
            var card = Card.Create(numberDummy, dateMock, cvvDummy, Currency.GBP);
            var clone = card.GetObscureClone();
            Assert.NotSame(card, clone);
        }

        [Fact]
        public void GetObscureClone_ReturnsAnObscureCard()
        {
            var card = Card.Create(numberDummy, dateMock, cvvDummy, Currency.GBP);
            var clone = card.GetObscureClone();
            Assert.Equal(OBSCURE_PART, clone.Number.Substring(0, 14));
        }

        [Fact]
        public void GetObscureClone_CopiesPropertiesExceptNumber()
        {
            var card = Card.Create(numberDummy, dateMock, cvvDummy, Currency.GBP);
            var clone = card.GetObscureClone();
            Assert.Same(card.Expiry, clone.Expiry);
            Assert.Equal(card.Cvv, clone.Cvv);
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
