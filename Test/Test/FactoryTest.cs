using System;
using Xunit;
using Core;
using Moq;

namespace Test
{
    public class FactoryTest
    {
        private readonly IRepository<Request> repo;
        private readonly Factory instance;
        public FactoryTest()
        {
            repo = new FakeRepo<Request>();
            instance = new Factory(repo);
        }
        //todo: Randomise parameters
        private readonly Dto dto = new Dto
        {
            MerchantId = 1,
            Amount = 0.7m,
            Card_Number = "1234-5678-8765-4321",
            Card_Cvv = 777,
            Card_Currency = Currency.GBP,
            Card_Expiry_Year = 2023,
            Card_Expiry_Month = 5
        };

        [Fact]
        public void Make_Makes()
        {
            var result = instance.Make(dto);
            Assert.IsType<Request>(result);
        }
    }
}