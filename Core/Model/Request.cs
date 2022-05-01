using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public enum RequestStatus { Received, Invalid, Pending, Declined, Success }
    public interface ISaveable
    {
        int? Id { get; }
        int Save(IRepository repo); //todo: should you return the saved object?
    }

    public interface IStatusable { RequestStatus Status { get; set; } }
    public interface IValidatable { bool IsValid { get; } }

    public interface IRequestable : ISaveable, IStatusable, IValidatable
    {
        int MerchantId { get; }
        ICard Card { get; }
        Decimal Amount { get; }
    }

    public class Request : IRequestable
    { //doc: almost immutable
        //todo: read boundary values from config
        private const decimal MIN_AMOUNT = 0.5m;
        private const int MAX_AMOUNT = 500;
        private Request() { }
        public int? Id { get; private set; }
        public int MerchantId { get; private set; }
        public ICard Card { get; private set; }
        public decimal Amount { get; private set; }
        public RequestStatus Status { get; set; }

        public static Request Create(int merchantId, ICard card, decimal amount)
        {
            var instance = new Request();
            instance.MerchantId = merchantId;
            instance.Card = card;
            instance.Amount = amount;
            instance.Status = RequestStatus.Received;
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

        public int Save(IRepository repo)
        {
            if (Id == null)
                Id = repo.Save(this);
            return Id.Value;
        }
    }
}