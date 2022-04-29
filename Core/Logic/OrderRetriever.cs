using System;

namespace Core
{ //todo: Separate classes into files
    public interface IOrderRetriever //todo: add method signatures
    {
        Response Get(Query query);
    }
    public class OrderRetriever : IOrderRetriever
    {
        // todo:
        // Allow a merchant to retrieve details of a previously made payment using its identifier.
        // The response should include a masked card number and card details along with a
        // status code which indicates the result of the payment.
        public Response Get(Query query)
        {
            // todo: check if the payment belonged to the merchant
            throw new NotImplementedException();
        }
    }
}