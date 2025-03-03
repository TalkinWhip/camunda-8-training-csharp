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
        
        var variables = JsonConvert.DeserializeObject<Dictionary<string, object>>(activatedjob.Variables);
        CreditCardService service = new CreditCardService();

        string cvc = variables["cvc"].ToString();
        string cardNumber = variables["cardNumber"].ToString();
        string expiryDate = variables["expiryDate"].ToString();
        double openAmount = Convert.ToDouble(variables["openAmount"]);

        service.ChargeAmount(cardNumber, cvc, expiryDate, openAmount);

        client.NewCompleteJobCommand(activatedjob.Key).Send();   

    }
}