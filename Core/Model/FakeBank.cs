namespace Core
{
    public interface IBank
    {
        bool Pay(IRequestable request);
    }
    public class FakeBank : IBank
    {
        private const int IMAGINARY_ACCOUNT_BALANCE = 200;
        //doc: Does the actual transaction.
        // It also performs some validation of the card information and then sends the
        // payment details to the appropriate 3rd party organization for processing.
        // Simulating the bank
        // In your solution you should use the CKO bank simulator to simulate the Acquiring Bank (see diagram above).
        // Build a bank simulator to test your payment gateway API.
        public bool Pay(IRequestable request)
        {
            return request.Amount <= 200; //doc: imaginary
        }
    }
}