using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using LCU.Graphs.Registry.Enterprises.Apps;
using LCU.Graphs.Registry.Enterprises;
using System.Linq;
using System.Collections.Generic;

namespace LCU.State.API.Apps
{
	[Serializable]
	[DataContract]
	public class SetActiveAppTypeRequest
	{
		[DataMember]
		public virtual string Type{ get; set; }
	}

	public static class SetActiveAppType
    {
        [FunctionName("SetActiveAppType")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
		{
			return await req.WithState<SetActiveAppTypeRequest, LCUAppsState>(log, async (details, reqData, state, stateMgr) =>
			{
				state.ActiveAppType = reqData.Type;

				return state;
			});
		}
    }
}