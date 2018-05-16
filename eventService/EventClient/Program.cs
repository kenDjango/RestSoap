using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EventClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select your town :");
            string town = Console.ReadLine();

            Console.WriteLine("Select your station :");
            string station = Console.ReadLine();

            Console.WriteLine("Enter an update time (in ms) :");
            int time = Int32.Parse(Console.ReadLine());

            Console.WriteLine("---Start of the event Service---");
            ServiceCallBackSink obj = new ServiceCallBackSink();
            InstanceContext context = new InstanceContext(obj);
            ServiceReference.ServiceClient objClient = new ServiceReference.ServiceClient(context);
            objClient.SubscribeAvailableBike(town, station, time);

            Console.ReadLine();
        }
    }
}
