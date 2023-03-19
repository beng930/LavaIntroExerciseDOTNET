using CosmosApi;
using CosmosApi.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json.Linq;
using System.Text;

namespace LavaIntroExercise.ClientDirectAccessLayer.ClientRequests
{
    public class GetSyncingCreator : RequestCreatorBase<NodeSyncingStatus, GetSyncingRequest>
    {
        // Create a specific instance of the generic RequestCreatorBase, using the Osmosis gRPC API GetSyncingAsync endpoint with the parameters defined in our proxy server API.
        // If needed, we can play around with the parameters, endpoint, etc. in complete separation from the server.
        public override async Task<ClientResult<NodeSyncingStatus>> CreateAndExecuteRequest(GetSyncingRequest parameters)
        {
            try
            {
                using var client = CreateClient(base._baseUrl);
                return ClientResult.Ok(await client.TendermintRpc.GetSyncingAsync());
            }
            catch (Exception ex)
            {
                return ClientResult.Fail<NodeSyncingStatus>(ex.Message);
            }
        }
    }
}
