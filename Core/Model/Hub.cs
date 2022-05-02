using System;

namespace Core
{
    public interface IHub
    {
        Response Process(IRequestable request);
    }

    public class Hub : IHub //todo: Find a better name for this responsibility
    {
        // todo: Merchant submits a request with fields such as:
        // card number, expiry month/date, amount, currency, and cvv.
        // validate request, store card information, forward req to bank, get response
        // Return either a successful or unsuccessful response
        private readonly IBank bank;
        public Hub(IBank b)
        {
            bank = b;
        }

        public Response Process(IRequestable request)
        {//doc: After setting request status no need to save because of in-memory repo
            if (request.IsValid)
                request.IsSuccess = bank.Pay(request);
            return Response.Create(request);
        }
    }
}