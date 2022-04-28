using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Model
{ //todo: Separate classes into files
    public class Query
    {
        public int MerchantId { get; set; }
        public int PaymentId { get; set; }
    }
    public class Order
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public Card Card { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; } //todo: needed?
    }

    public class Card
    {
        //todo: Apply the CreditCardAttribute for validation
        public string Number { get; set; }
        public ExpiryDate Expiry { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency Currency { get; set; }
        public int CVV { get; set; }
    }

    public enum Currency
    {
        // [EnumMember(Value = "GBP")]
        GBP,
        // [EnumMember(Value = "USD")]
        USD,
        // [EnumMember(Value = "EUR")]
        EUR
    }

    public class ExpiryDate
    {
        public int Month { get; set; }
        public int Day { get; set; }
    }
}