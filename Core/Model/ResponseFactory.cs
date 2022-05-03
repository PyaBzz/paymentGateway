using System;

namespace Core
{
    public interface IResponseFactory
    {
        Response Process(IRequestable request);
    }

    public class ResponseFactory : IResponseFactory //todo: Find a better name for this responsibility
    {
        // todo: Merchant submits a request with fields such as:
        // card number, expiry month/date, amount, currency, and cvv.
        // validate request, store card information, forward req to bank, get response
        // Return either a successful or unsuccessful response
        private readonly IBank bank;
        public ResponseFactory(IBank b)
        {
            bank = b;
        }

        public Response Process(IRequestable request)
        {//doc: After setting request status no need to save because of in-memory repo
            if (request.IsSuccess.HasValue)
                throw new ArgumentException("Can process fresh requests only!");

            bool? success = null;
            if (request.IsValid) success = bank.Pay(request);
            request.IsSuccess = success;

            Status responseStatus;
            if (request.IsValid == false)
                responseStatus = Status.Invalid;
            else
                responseStatus = success.Value ? Status.Success : Status.Declined;

            return new Response(request.Id, responseStatus);
        }
    }
}