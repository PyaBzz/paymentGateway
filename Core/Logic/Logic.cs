namespace Logic
{ //todo: Separate classes into files
    public class QueryProcessor
    {
        // todo: merchantId should come in as a parameter to see if the payment belonged to them
        // A merchant should be able to retrieve the details of a previously made payment. The next section
        // will discuss each of these in more detail.
        // The second requirement for the payment gateway is to allow a merchant to retrieve details of a
        // previously made payment using its identifier. Doing this will help the merchant with their reconciliation
        // and reporting needs. The response should include a masked card number and card details along with a
        // status code which indicates the result of the payment.
    }

    public class OrderProcessor
    {
        // todo: Responsible for validating requests, storing card information and forwarding
        // payment requests and accepting payment responses to and from the acquiring bank.
        // A merchant should be able to process a payment through the payment gateway and receive either
        // a successful or unsuccessful response.
        // The payment gateway will need to provide merchants with a way to process a payment. To do this, the
        // merchant should be able to submit a request to the payment gateway. A payment request should include
        // appropriate fields such as the card number, expiry month/date, amount, currency, and cvv.
    }

    public class BankMock
    {
        //todo: Allows us to do the actual retrieval of money from the shopper’s card and payout
        // to the merchant. It also performs some validation of the card information and then sends the
        // payment details to the appropriate 3rd party organization for processing.
        // Simulating the bank
        // In your solution you should use the CKO bank simulator to simulate the Acquiring Bank (see diagram above).
        // Build a bank simulator to test your payment gateway API.
    }
}