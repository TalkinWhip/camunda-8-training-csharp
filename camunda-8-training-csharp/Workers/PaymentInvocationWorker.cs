using Camunda8Training.Services;
using Newtonsoft.Json;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace Camunda8Training.Workers {

  public class PaymentInvocationWorker : Worker {
    public PaymentInvocationWorker(IZeebeClient client) : base("payment-invocation", client) {}

    public override void Handler(IJobClient jobClient, IJob activatedjob) {
      Console.Out.WriteLine("Worker invoked: " + activatedjob.Type);
      Random rnd = new Random();
      string orderId = rnd.Next().ToString();

      var variables = JsonConvert.DeserializeObject<Dictionary<string, object>>(activatedjob.Variables);
      variables.Add("orderId", orderId);

      string json = JsonConvert.SerializeObject(variables);      
      
      client.NewPublishMessageCommand().MessageName("paymentRequestedMessage").CorrelationKey(orderId).Variables(json).Send();
      jobClient.NewCompleteJobCommand(activatedjob).Variables(json).Send();
    }
  }
}