using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core
{
    public class Report : Response
    {//doc: This is a simple immutable Dto
        private const string OBSCURE_PART = "****-****-****";
        public int MerchantId { get; private set; }
        public Decimal Amount { get; private set; }
        public string Card_Number { get; private set; }
        public int Card_Cvv { get; private set; }
        public Currency Card_Currency { get; private set; }
        public int Card_Expiry_Year { get; private set; }
        public int Card_Expiry_Month { get; private set; }

        public Report(IRequestable req) : base(req)
        {
            this.MerchantId = req.MerchantId;
            this.Amount = req.Amount;
            this.Card_Number = OBSCURE_PART + "-" + req.Card.Number.Substring(14); //todo: unit test
            this.Card_Cvv = req.Card.Cvv;
            this.Card_Currency = req.Card.Currency;
            this.Card_Expiry_Year = req.Card.Expiry.Year;
            this.Card_Expiry_Month = req.Card.Expiry.Month;
        }
    }
}