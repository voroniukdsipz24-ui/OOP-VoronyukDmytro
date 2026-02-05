using System;

class Program
{
    static void Main(string[] args)
    {
        // Використання CreditCardGateway
        IPaymentGateway creditCardPayment = new CreditCardGateway();
        PaymentHandler paymentHandler = new PaymentHandler(creditCardPayment);
        paymentHandler.Pay(100.0);

        // Використання PayPalService
        IPaymentGateway payPalPayment = new PayPalService();
        paymentHandler = new PaymentHandler(payPalPayment);
        paymentHandler.Pay(200.0);

        // Використання CryptoExchange
        IPaymentGateway cryptoPayment = new CryptoExchange();
        paymentHandler = new PaymentHandler(cryptoPayment);
        paymentHandler.Pay(300.0);
    }
}
