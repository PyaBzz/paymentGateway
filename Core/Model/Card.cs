using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{ //todo: write documentation about doc comments
    public enum Currency { GBP, USD, EUR } // [EnumMember(Value = "GBP")]
    public class Card
    {//doc: immutable
        private const string OBSCURE_PART = "****-****-****";
        //todo: Apply the CreditCardAttribute for validation
        public string Number { get; private set; }
        public IDate Expiry { get; private set; }
        [JsonConverter(typeof(JsonStringEnumConverter))] //todo: could apply to the enum itself?
        public Currency Currency { get; private set; }
        public int CVV { get; private set; }
        public bool IsObscure => Number.Substring(0, 14) == OBSCURE_PART;

        public Card(string number, IDate expiry, int cvv, Currency currency)
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
}