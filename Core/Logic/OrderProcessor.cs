// using System;

// namespace Core
// {
//     public interface IOrderProcessor
//     {
//         TransacStatus Do(Transaction order);
//     }

//     public class OrderProcessor : IOrderProcessor
//     {
//         // todo:
//         // Merchant submits a request.
//         // Request fields such as the card number, expiry month/date, amount, currency, and cvv.
//         // Validate request
//         // Store card information
//         // Forward request to bank
//         // Get response from bank
//         // Return either a successful or unsuccessful response
//         public TransacStatus Do(Transaction order)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }