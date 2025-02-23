using Zeebe.Client;
using Zeebe.Client.Api.Responses;
using Zeebe.Client.Api.Worker;

namespace Camunda8Training.Workers {
  public abstract class Worker {
    protected IZeebeClient client;
    protected string jobType;

    protected Worker(string jobType, IZeebeClient client) {
      this.jobType = jobType;
      this.client = client;
      
      Start();
    }

    protected void Start() {
      client.NewWorker().JobType(jobType)
        .Handler(Handler)
        .MaxJobsActive(1)
        .Timeout(TimeSpan.FromSeconds(10))
        .Open();  
    }
    
    protected void PrintProcessVariables(Dictionary<string, object> variables) {
      foreach (var variable in variables) {
        Console.Out.WriteLine("Process variable: " + variable);
      }
    }
    
    public abstract void Handler(IJobClient jobClient, IJob activatedjob);
  }
}