using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public interface IValidatable { bool IsValid { get; } }
    public interface ISuccessible { bool? IsSuccess { get; set; } }

    public interface IRequestable : IValidatable, ISuccessible
    {
        int Id { get; }
        int MerchantId { get; }
        ICard Card { get; }
        Decimal Amount { get; }
    }

    public class Request : IRequestable
    { //doc: almost immutable
        private const decimal MIN_AMOUNT = 0.5m; //doc: read boundary values from config
        private const int MAX_AMOUNT = 500;
        private Request() { }
        public int Id { get; private set; }
        public int MerchantId { get; private set; }
        public ICard Card { get; private set; }
        public decimal Amount { get; private set; }
        public bool? IsSuccess { get; set; }

        public static Request Create(int merchantId, ICard card, decimal amount, IRepository<Request> repo)
        {
            //doc: Factory method because the object state is mostly readonly from outside also
            // In real life repository operations are async which cannot take place in constructor
            var instance = new Request();
            instance.MerchantId = merchantId;
            instance.Card = card;
            instance.Amount = amount;
            instance.Id = repo.Save(instance); //doc: Encapsulate all state changes including Id-setter
            return instance;
        }

        public bool IsValid
        {
            get
            {
                if (MerchantId < 0)
                    return false;
                if (Card.IsExpired)
                    return false;
                if (Amount < MIN_AMOUNT || Amount > MAX_AMOUNT)
                    return false;
                return true;
            }
        }
    }
}