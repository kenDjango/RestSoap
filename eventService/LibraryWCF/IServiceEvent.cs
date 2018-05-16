using System.ServiceModel;

namespace EventsLib
{
    interface IServiceEvent
    {
        [OperationContract(IsOneWay = true)]
        void avaibleBike(string town, string station, int number);
    }
}
