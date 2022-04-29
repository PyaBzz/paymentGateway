using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public enum OrderStatus { Pending, TimedOut, Declined, Success }

    public class Order
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