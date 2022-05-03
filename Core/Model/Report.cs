using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public class Report
    {//doc: This is a simple immutable Dto with a built-in factory
        private Report() { }
        private const string OBSCURE_PART = "****-****-****";
        public Status Status { get; private set; }
        public int Id { get; private set; }
        public int MerchantId { get; private set; }
        public Decimal Amount { get; private set; }
        public string Card_Number { get; private set; }
        public int Card_Cvv { get; private set; }
        public Currency Card_Currency { get; private set; }
        public int Card_Expiry_Year { get; private set; }
        public int Card_Expiry_Month { get; private set; }

        public static Report Create(IRequestable req)
        {
            var instance = new Report();
            if (req.IsValid)
                instance.Status = req.IsSuccess.Value ? Status.Success : Status.Declined;
            else
                instance.Status = Status.Invalid;
            instance.MerchantId = req.MerchantId;
            instance.Amount = req.Amount;
            instance.Card_Number = OBSCURE_PART + "-" + req.Card.Number.Substring(14); //todo: unit test
            instance.Card_Cvv = req.Card.Cvv;
            instance.Card_Currency = req.Card.Currency;
            instance.Card_Expiry_Year = req.Card.Expiry.Year;
            instance.Card_Expiry_Month = req.Card.Expiry.Month;
            return instance;
        }
    }
}