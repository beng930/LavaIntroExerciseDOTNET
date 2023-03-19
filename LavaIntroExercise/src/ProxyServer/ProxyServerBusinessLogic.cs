using Grpc.Core;
using LavaIntroExercise;
using LavaIntroExercise.ClientDirectAccessLayer;
using LavaIntroExercise.ClientDirectAccessLayer.ClientRequests;
using LavaIntroExercise.ProxyServer;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json.Linq;
using System.Text;

namespace LavaIntroExercise.Services
{
    public class ProxyServerBusinessLogic : ProxyServerService.ProxyServerServiceBase
    {
        private readonly ILogger<ProxyServerBusinessLogic> _logger;
        public ProxyServerBusinessLogic(ILogger<ProxyServerBusinessLogic> logger)
        {
            _logger = logger;
        }

        public async override Task<GetNodeInfoResponse> GetNodeInfo(GetNodeInfoRequest requestObject, ServerCallContext context)
        {
            var clientDal = new ClientDal(_logger);

            // Create an API call creator class, and then use the direct access layer to execute the creator specific defined call
            var requestCreator = new NodeInfoRequestCreator();
            var response = await clientDal.ForwardRequests(requestCreator, requestObject);
            if (response.IsFailure)
            {
                _logger.LogError(response.ErrorMessage);
                throw new RpcException(new Status(StatusCode.Internal, response.ErrorMessage));
            }

            return await Task.FromResult(new GetNodeInfoResponse
            {
                Response = response.Value.ToString()
            });
        }

        public async override Task<GetSyncingResponse> GetSyncing(GetSyncingRequest requestObject, ServerCallContext context)
        {
            var clientDal = new ClientDal(_logger);

            // Create an API call creator class, and then use the direct access layer to execute the creator specific defined call
            var requestCreator = new GetSyncingCreator();
            var response = await clientDal.ForwardRequests(requestCreator, requestObject);
            if (response.IsFailure)
            {
                _logger.LogError(response.ErrorMessage);
                throw new RpcException(new Status(StatusCode.Internal, response.ErrorMessage));
            }

            return await Task.FromResult(new GetSyncingResponse
            {
                Response = response.Value.ToString()
            });
        }

        public async override Task<GetLatestBlockResponse> GetLatestBlock(GetLatestBlockRequest requestObject, ServerCallContext context)
        {
            var clientDal = new ClientDal(_logger);

            // Create an API call creator class, and then use the direct access layer to execute the creator specific defined call
            var requestCreator = new GetLatestBlockCreator();
            var response = await clientDal.ForwardRequests(requestCreator, requestObject);
            if (response.IsFailure)
            {
                _logger.LogError(response.ErrorMessage);
                throw new RpcException(new Status(StatusCode.Internal, response.ErrorMessage));
            }

            return await Task.FromResult(new GetLatestBlockResponse
            {
                Hash = ConvertHashToString(response.Value.BlockMeta.BlockId.Hash),
                Height = response.Value.Block.Header.Height ?? 0,
            });
        }

        private string ConvertHashToString(byte[] hash)
        {
            // Merge all bytes into a string of bytes
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public async override Task<GetBlockByHeightResponse> GetBlockByHeight(GetBlockByHeightRequest requestObject, ServerCallContext context)
        {
            var clientDal = new ClientDal(_logger);

            // Create an API call creator class, and then use the direct access layer to execute the creator specific defined call
            var requestCreator = new GetBlockByHeightCreator();
            var response = await clientDal.ForwardRequests(requestCreator, requestObject);
            if (response.IsFailure)
            {
                _logger.LogError(response.ErrorMessage);
                throw new RpcException(new Status(StatusCode.Internal, response.ErrorMessage));
            }

            return await Task.FromResult(new GetBlockByHeightResponse
            {
                Response = response.Value.ToString()
            });
        }

        public async override Task<GetLatestValidatorSetResponse> GetLatestValidatorSet(GetLatestValidatorSetRequest requestObject, ServerCallContext context)
        {
            var clientDal = new ClientDal(_logger);

            // Create an API call creator class, and then use the direct access layer to execute the creator specific defined call
            var requestCreator = new GetLatestValidatorSetCreator();
            var response = await clientDal.ForwardRequests(requestCreator, requestObject);
            if (response.IsFailure)
            {
                _logger.LogError(response.ErrorMessage);
                throw new RpcException(new Status(StatusCode.Internal, response.ErrorMessage));
            }

            return await Task.FromResult(new GetLatestValidatorSetResponse
            {
                Response = response.Value.ToString()
            });
        }

        public async override Task<GetValidatorSetByHeightResponse> GetValidatorSetByHeight(GetValidatorSetByHeightRequest requestObject, ServerCallContext context)
        {
            var clientDal = new ClientDal(_logger);

            // Create an API call creator class, and then use the direct access layer to execute the creator specific defined call
            var requestCreator = new GetValidatorSetByHeightCreator();
            var response = await clientDal.ForwardRequests(requestCreator, requestObject);
            if (response.IsFailure)
            {
                _logger.LogError(response.ErrorMessage);
                throw new RpcException(new Status(StatusCode.Internal, response.ErrorMessage));
            }

            return await Task.FromResult(new GetValidatorSetByHeightResponse
            {
                Response = response.Value.ToString()
            });
        }
    }
}