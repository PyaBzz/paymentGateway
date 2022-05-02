namespace Core
{
    public interface IBank
    {
        //todo: add method signatures
    }
    public class FakeBank : IBank
    {
        //todo: Allows us to do the actual retrieval of money from the shopper’s card and payout
        // to the merchant. It also performs some validation of the card information and then sends the
        // payment details to the appropriate 3rd party organization for processing.
        // Simulating the bank
        // In your solution you should use the CKO bank simulator to simulate the Acquiring Bank (see diagram above).
        // Build a bank simulator to test your payment gateway API.
    }
}