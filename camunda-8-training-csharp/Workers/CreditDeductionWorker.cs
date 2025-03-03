using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;
using Newtonsoft.Json;
using Camunda8Training.Services;

namespace Camunda8Training.Workers;

public class CreditDeductionWorker : Worker {
    public CreditDeductionWorker(string jobType, IZeebeClient client) : base(jobType, client)
    {
    }

    public override void Handler(IJobClient jobClient, IJob activatedjob)
    {
        Console.Out.WriteLine("Worker invoked: " + activatedjob.Type);
        client.NewCompleteJobCommand(activatedjob.Key).Variables(json).Send();   
    }
}
