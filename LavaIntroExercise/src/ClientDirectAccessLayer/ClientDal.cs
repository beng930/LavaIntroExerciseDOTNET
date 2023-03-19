namespace LavaIntroExercise.ClientDirectAccessLayer;

using LavaIntroExercise.ClientDirectAccessLayer.ClientRequests;
public class ClientDal : IClientDal
{
    private ILogger _logger;

    public ClientDal(ILogger logger) 
    {
        _logger = logger;
    }

    public async Task<ClientResult<S>> ForwardRequests<S, T>(RequestCreatorBase<S, T> request, T parameters)
    {
        return await ExecuteRequest(request, parameters);
    }

    private async Task<ClientResult<S>> ExecuteRequest<S, T>(RequestCreatorBase<S, T> request, T parameters)
    {
        try
        {
            // Create and execute the request by the RequestCreatorBase generic class, receiving a ClientResult instance
            var response = await request.CreateAndExecuteRequest(parameters);
            if (response.Value == null || response.IsFailure)
            {
                _logger.LogError(response.ErrorMessage);
                return ClientResult.Fail<S>(response.ErrorMessage);
            }

            return ClientResult.Ok(response.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return ClientResult.Fail<S>(ex.Message);
        }
    }
}
