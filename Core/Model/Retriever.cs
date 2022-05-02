using System;

namespace Core
{ //todo: Separate classes into files
    public interface IRetriever
    {
        Report Get(Query query);
    }
    public class Retriever : IRetriever
    {
        // todo:
        // Allow a merchant to retrieve details of a previously made payment using its identifier.
        // The response should include a masked card number and card details along with a
        // status code which indicates the result of the payment.
        private readonly IRepository<Request> repo;
        public Retriever(IRepository<Request> r)
        {
            repo = r;
        }
        public Report Get(Query query)
        {
            // todo: check if the payment belonged to the merchant
            var request = repo.Get(query.RequestId);
            throw new NotImplementedException();
        }
    }
}