using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public enum OrderStatus { Pending, TimedOut, Declined, Success }

    public interface IOrder
    {
        Card Card { get; }
        Order ObscureClone();
    }
    public class Order : IOrder //todo: rename to payment
    { //todo: make immutable
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public Card Card { get; set; }
        public decimal Amount { get; set; }
        public Order ObscureClone()
        {
            var clone = (Order)MemberwiseClone();
            clone.Card = clone.Card.ObscureClone();
            return clone;
        }
    }
}