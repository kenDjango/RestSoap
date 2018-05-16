using System.ServiceModel;

namespace EventsLib
{
    [ServiceContract(CallbackContract = typeof(IServiceEvent))]
    public interface IService {
        [OperationContract]
        void SubscribeAvailableBike(string city, string station, int time);
    }
}