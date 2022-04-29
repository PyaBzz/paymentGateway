using System;
using Xunit;
using Core;

namespace Test
{
    public class ResponseFactoryTest
    {
        private readonly string cardNo = "1234-5678-8765-4321";
        private Card card;
        private Order order;

        public ResponseFactoryTest()
        {
            var expiry = new ExpiryDate(2, 4);
            card = new Card(cardNo, expiry, Currency.GBP, 777);
            order = new Order
            {
                Id = 1,
                MerchantId = 2,
                Card = card,
                Amount = 0.5m
            };
        }

        [Fact]
        public void Get_Obscures_Card_Number()
        {
            var response = new Response(order, QueryStatus.Resolved);

            Assert.True(response.Order.Card.IsObscure);
        }

        [Fact]
        public void Get_Doesnt_Alter_ReferenceType_InputParameters()
        {
            var response = new Response(order, QueryStatus.Resolved);
            Assert.Same(card.Number, cardNo);
        }
    }
}
