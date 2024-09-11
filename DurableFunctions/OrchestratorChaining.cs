using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace DurableFunctions
{
    public static class OrchestratorChaining
    {
        [Function(nameof(OrchestratorChaining))]
        public static async Task<decimal> RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(OrchestratorChaining));

            var resultA = await context.CallActivityAsync<decimal>(nameof(OdliczUbezpieczenieEmerytalne), 5000);
            var resultB = await context.CallActivityAsync<decimal>(nameof(OdliczUbezpieczenieChorobowe), resultA);
            var resultC = await context.CallActivityAsync<decimal>(nameof(OdliczUbezpieczenieZdrowotne), resultB);

            logger.LogInformation("Koñcowa kwota:" + resultC);

            return resultC;
        }

        [Function(nameof(OdliczUbezpieczenieEmerytalne))]
        public static decimal OdliczUbezpieczenieEmerytalne([ActivityTrigger] decimal money, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("OdliczUbezpieczenieEmerytalne");
            var final = money * (decimal)0.9;
            logger.LogInformation($"A: Odliczam ubezpieczenie emerytalne z kwoty {money}: {final}");
            return final;
        }

        [Function(nameof(OdliczUbezpieczenieChorobowe))]
        public static decimal OdliczUbezpieczenieChorobowe([ActivityTrigger] decimal money, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("OdliczUbezpieczenieChorobowe");
            var final = money * (decimal)0.97;
            logger.LogInformation($"B: Odliczam ubezpieczenie chorobowe z kwoty {money}: {final}");
            return final;
        }

        [Function(nameof(OdliczUbezpieczenieZdrowotne))]
        public static decimal OdliczUbezpieczenieZdrowotne([ActivityTrigger] decimal money, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("OdliczUbezpieczenieZdrowotne");
            var final = money * (decimal)0.92;
            logger.LogInformation($"C: Odliczam ubezpieczenie zdrowotne z kwoty {money}: {final}");
            return final;
        }

        [Function("OrchestratorChaining_HttpStart")]
        public static async Task<HttpResponseData> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(OrchestratorChaining));

            return client.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
