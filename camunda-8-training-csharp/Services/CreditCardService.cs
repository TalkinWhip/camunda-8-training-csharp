using Camunda8Training.Exceptions;

namespace Camunda8Training.Services; 

public class CreditCardService {
  public void ChargeAmount(string? cardNumber, string? cvc, string? expiryDate, double amount) {
    if (expiryDate.Length == 5) {
      Console.Out.WriteLine("Credit card number: " + cardNumber + " CVC: " + cvc + " Expiry date: " + expiryDate +
                            " Amount to charge: " + amount);
    } else {
      throw new InvalidCreditCardException("Invalid credit card expiry date: " + expiryDate);
    }
  }
}

