using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{ //todo: write documentation about doc comments
    public class Card
    {//doc: immutable
        private const string OBSCURE_PART = "****-****-****";
        //todo: Apply the CreditCardAttribute for validation
        public string Number { get; private set; }
        public ExpiryDate Expiry { get; }
        [JsonConverter(typeof(JsonStringEnumConverter))] //todo: could apply to the enum itself?
        public Currency Currency { get; }
        public int CVV { get; }
        public bool IsObscure => Number.Substring(0, 14) == OBSCURE_PART;

        public Card(string number, ExpiryDate expiry, Currency currency, int cvv)
        {
            Number = number;
            Expiry = expiry;
            Currency = currency;
            CVV = cvv;
        }

        public Card ObscureClone()
        {
            var clone = (Card)MemberwiseClone();
            clone.Obscure();
            return clone;
        }

        public void Obscure() => Number = OBSCURE_PART + "-" + Number.Substring(14);
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
    {//todo: make immutable
        public ExpiryDate(int month, int day)
        {
            Month = month;
            Day = day;
        }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}