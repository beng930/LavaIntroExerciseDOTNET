using LavaIntroExercise.ClientDirectAccessLayer.ClientRequests;

namespace LavaIntroExercise.ClientDirectAccessLayer
{
    public interface IClientDal
    {
        public Task<ClientResult<S>> ForwardRequests<S, T>(RequestCreatorBase<S, T> request, T parameters);
    }
}
