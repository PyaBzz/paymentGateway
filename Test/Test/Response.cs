using System;
using Xunit;
using Core;

namespace Test
{
    public class ResponseTest
    {
        private readonly string cardNo = "1234-5678-8765-4321";
        private Card card;
        private Order order;

        public ResponseTest()
        {
            var expiry = new ExpiryDate(2, 4);
            card = new Card(cardNo, expiry, Currency.GBP, 777);
            order = new Order { Id = 1, MerchantId = 2, Card = card, Amount = 0.5m };
        }

        [Fact]
        public void Create_Obscures_Card_Number()
        {
            var response = Response.Create(order, QueryStatus.Resolved);
            Assert.True(response.Order.Card.IsObscure);
        }

        [Fact]
        public void Create_Leaves_ReferenceType_InputParameters_Intact()
        {
            var response = Response.Create(order, QueryStatus.Resolved);
            Assert.Same(card.Number, cardNo);
        }
    }
}
