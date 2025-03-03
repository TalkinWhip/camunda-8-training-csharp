using Camunda8Training.Workers;
using System;
using Zeebe.Client;
using Zeebe.Client.Impl.Builder;

namespace Camunda8Training {
  public class Program {
    private static IZeebeClient _client = CamundaCloudClientBuilder
    .Builder()
      .UseClientId("9JkXtImZ3kDXy~J-7WsC3ovgy~Yf2P_M")
      .UseClientSecret("BiHKk_rhBnZQO.OqPc2HjFXxZGhBlxginaf.Lj4rhLtCKNPCNywh22_44v-JzBwp")
      .UseContactPoint("d399dce9-6cf6-43e0-9fbd-3c5d68995beb.bru-2.zeebe.camunda.io:443")
    .Build();
    
    public static void Main(string[] args) {
      var program = new Program();
      program.Run();
    }
    
    private void Run() {
      using var signal = new EventWaitHandle(false, EventResetMode.AutoReset);
      var creditDeductionWorker = new CreditDeductionWorker("credit-deduction", _client);
      var creditCardChargingWorker = new CreditCardChargingWorker("credit-card-charging", _client);
    //   var paymentInvocationWorker = new PaymentInvocationWorker(_client); // BPMN message: paymentInvocationMessage
    //   var paymentCompletionWorker = new PaymentCompletionWorker(_client); // BPMN message: paymentCompletionMessage
        
      signal.WaitOne();
    }
  }
}
