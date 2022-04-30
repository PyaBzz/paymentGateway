using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{ //todo: write documentation about doc comments
    public enum Currency { GBP, USD, EUR } // [EnumMember(Value = "GBP")]

    public interface IExpirable { bool IsExpired { get; } }

    public interface IObscurable { ICard GetObscureClone(); }

    public interface ICard : IExpirable, IObscurable
    {
        //todo: Calculate Id from Number+Expiry+CVV why?
        string Number { get; }
        IDate Expiry { get; }
        int Cvv { get; }
        Currency Currency { get; }
    }

    public class Card : ICard
    {//doc: immutable
        private Card() { }
        private const string OBSCURE_PART = "****-****-****";
        //todo: PIN ?
        public string Number { get; private set; }
        public IDate Expiry { get; private set; }
        [JsonConverter(typeof(JsonStringEnumConverter))] //todo: could apply to the enum itself?
        public int Cvv { get; private set; }
        public Currency Currency { get; private set; }
        public bool IsExpired => Expiry.IsPassed;

        public ICard GetObscureClone()
        {
            var obscureNumber = OBSCURE_PART + "-" + Number.Substring(14);
            return Card.Create(obscureNumber, Expiry, Cvv, Currency);
        }

        public static Card Create(string number, IDate date, int cvv, Currency currency)
        {
            //todo: Validate number
            var instance = new Card
            {
                Number = number,
                Expiry = date,
                Currency = currency,
                Cvv = cvv
            };
            return instance;
        }
    }
}