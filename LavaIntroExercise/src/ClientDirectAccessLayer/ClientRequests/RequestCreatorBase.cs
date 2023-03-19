using CosmosApi;

namespace LavaIntroExercise.ClientDirectAccessLayer.ClientRequests
{
    public abstract class RequestCreatorBase<S, T>
    {
        // A generic class for creating a request instance with complete separation of concerns from the server and direct access layer.
        // This class instances will be endpoint and parameter specific and can be changed on demand.

        //Can be overidden in any of the child classes, to use other endpoints
        protected readonly string _baseUrl = "http://rpc.osmosis.zone:9090/cosmos/base/tendermint/v1beta1";

        // Creates and executes a request given specific endpoint and parameters received.
        // Returns a ClientResult instance that includes an error message (if applicable), result value and status of the request.
        public abstract Task<ClientResult<S>> CreateAndExecuteRequest(T parameters);

        protected ICosmosApiClient CreateClient(string baseUrl)
        {
            return new CosmosApiBuilder()
                .UseBaseUrl(baseUrl)
                .RegisterCosmosSdkTypeConverters()
                .CreateClient();
        }
    }
}
