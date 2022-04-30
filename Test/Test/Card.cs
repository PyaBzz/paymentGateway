// using System;
// using Xunit;
// using Core;
// using Moq;

// namespace Test
// {
//     public class CardTest
//     {
//         private readonly string cardNo = "1234-5678-8765-4321";
//         private Card card;
//         private Order order;

//         public CardTest()
//         {
//             var expiry = new ExpiryDate.Create(2022, 2);
//             card = new Card(cardNo, expiry, Currency.GBP, 777);
//             order = new Order { Id = 1, MerchantId = 2, Card = card, Amount = 0.5m };
//         }

//         [Fact]
//         public void Create_Obscures_Card_Number()
//         {
//             Assert.False(order.Card.IsObscure);
//             var response = Response.Create(order, QueryStatus.Resolved);
//             Assert.True(response.Order.Card.IsObscure);
//         }

//         [Fact]
//         public void Create_Leaves_ReferenceType_InputParameters_Intact()
//         {
//             var response = Response.Create(order, QueryStatus.Resolved);
//             Assert.Same(card.Number, cardNo);
//         }
//     }
// }
