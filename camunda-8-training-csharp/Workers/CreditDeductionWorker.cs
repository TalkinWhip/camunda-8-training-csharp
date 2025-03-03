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

        CreditService customerService = new CreditService();
        Dictionary<string, object> variables = JsonConvert.DeserializeObject<Dictionary<string, object>>(activatedjob.Variables);

        double orderTotal = Convert.ToDouble(variables["orderTotal"]); 
        string custId = variables["customerId"].ToString();
        double customerCredit = customerService.GetCustomerCredit(custId);

        double openAmount = customerService.DeductCredit(customerCredit, orderTotal);
        variables.Add("openAmount", openAmount);
        
        string json = JsonConvert.SerializeObject(variables);
        client.NewCompleteJobCommand(activatedjob.Key).Variables(json).Send();   
    }
}
