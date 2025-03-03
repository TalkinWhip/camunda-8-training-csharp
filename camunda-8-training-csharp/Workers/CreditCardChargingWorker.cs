using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;
using Newtonsoft.Json;
using Camunda8Training.Services;


namespace Camunda8Training.Workers;

public class CreditCardChargingWorker : Worker {

    public CreditCardChargingWorker(string jobType, IZeebeClient client) : base(jobType, client)
    {
    }

    public override void Handler(IJobClient jobClient, IJob activatedjob)
    {
        Console.Out.WriteLine("Worker invoked: " + activatedjob.Type);
        client.NewCompleteJobCommand(activatedjob.Key).Send();   

    }
}