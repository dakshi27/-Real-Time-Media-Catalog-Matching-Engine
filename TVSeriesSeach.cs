
using System;

using System.IO;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Azure.WebJobs;

//using Microsoft.Azure.WebJobs.Extensions.Http;

using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Xperi.DataLayer;
using Microsoft.Azure.Functions.Worker;



namespace Xperi.SmartSearch

{

    public static class TVSeriesSearch

    {

        [FunctionName("TVSeriesSearch")]

        public static async Task<IActionResult> Run(

            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/TVSeries/")] HttpRequest req,

            ILogger log)

        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            string subcategory = req.Query["subcategory"];

            if (string.IsNullOrEmpty(subcategory))

            {

                return new BadRequestObjectResult("Input value of 'subcategory' cannot be empty");

            }

            string searchKey = req.Query["searchkey"];

            if (string.IsNullOrEmpty(searchKey))

            {

                return new BadRequestObjectResult("Input value of 'searchKey' cannot be empty");

            }



            var dbManager = new DBManager();

            var result = await dbManager.FetchTVSeriesResults(subcategory, searchKey);





            return new OkObjectResult(result);

        }

    }

}
