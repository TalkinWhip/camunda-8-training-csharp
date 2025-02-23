using Camunda8Training.Workers;
using System;
using Zeebe.Client;
using Zeebe.Client.Impl.Builder;

namespace Camunda8Training {
  public class Program {
    private static IZeebeClient _client = CamundaCloudClientBuilder
    .Builder()
      .UseClientId("ZEEBE_CLIENT_ID")
      .UseClientSecret("ZEEBE_CLIENT_SECRET")
      .UseContactPoint("ZEEBE_ADDRESS")
    .Build();
    
    public static void Main(string[] args) {
      var program = new Program();
      program.Run();
    }
    
    private void Run() {
      using var signal = new EventWaitHandle(false, EventResetMode.AutoReset);
    //   var creditDeductionWorker = new CreditDeductionWorker(_client);
    //   var creditCardChargingWorker = new CreditCardChargingWorker(_client);
    //   var paymentInvocationWorker = new PaymentInvocationWorker(_client); // BPMN message: paymentInvocationMessage
    //   var paymentCompletionWorker = new PaymentCompletionWorker(_client); // BPMN message: paymentCompletionMessage
        
      signal.WaitOne();
    }
  }
}
