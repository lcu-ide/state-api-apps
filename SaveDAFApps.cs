using Fathym;
using LCU.Graphs;
using LCU.Graphs.Registry.Enterprises;
using LCU.Graphs.Registry.Enterprises.Apps;
using LCU.State.API.ForgePublic.Harness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LCU.State.API.Apps
{
	[Serializable]
	[DataContract]
	public class SaveDAFAppsRequest
	{
		[DataMember]
		public virtual List<DAFApplicationConfiguration> DAFApps { get; set; }
	}

	public static class SaveDAFApps
	{
		[FunctionName("SaveDAFApps")]
		public static async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
			ILogger log)
		{
			return await req.Manage<SaveDAFAppsRequest, LCUAppsState, ForgeAPIAppsStateHarness>(log, async (mgr, reqData) =>
            {
				log.LogInformation($"Saving DAF Apps: {reqData.DAFApps.ToJSON()}");

                return await mgr.SaveDAFApps(reqData.DAFApps);
            });
		}
	}
}
