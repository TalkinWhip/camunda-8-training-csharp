using Camunda8Training.Services;
using Newtonsoft.Json;
using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace Camunda8Training.Workers {

  public class PaymentCompletionWorker : Worker {
    public PaymentCompletionWorker(IZeebeClient client) : base("payment-completion", client) {}

    public override void Handler(IJobClient jobClient, IJob activatedjob) {
      Console.Out.WriteLine("Worker invoked: " + activatedjob.Type);

      var variables = JsonConvert.DeserializeObject<Dictionary<string, object>>(activatedjob.Variables);
      PrintProcessVariables(variables);

      string correlationKey = variables["orderId"].ToString();
      string messageName = variables["messageName"].ToString();
      string json = JsonConvert.SerializeObject(variables);

      client.NewPublishMessageCommand().MessageName(messageName).CorrelationKey(correlationKey)
        .Variables(json).Send();
      jobClient.NewCompleteJobCommand(activatedjob).Send();
    }
  }
}