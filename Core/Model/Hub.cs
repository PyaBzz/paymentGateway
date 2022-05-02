using System;

namespace Core
{
    public enum RequestStatus { Received, Invalid, Pending, Declined, Success }
    public interface IHub
    {
        RequestStatus Forward(IRequestable request);
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

        public RequestStatus Forward(IRequestable request)
        {
            if (request.Status != RequestStatus.Received)
                throw new ArgumentException("Request must be in 'Received' state");
            return RequestStatus.Invalid;
        }
    }
}