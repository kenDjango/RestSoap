using EventsLib;
using System;
using System.ServiceModel;


namespace ClientToLaunchHost
{
    class Program{
        static void Main(string[] args){

            //Create ServiceHost
            ServiceHost host  = new ServiceHost(typeof(Service));
            //Start the Service
            host.Open();
            Console.WriteLine("Hey I am Alive");
            Console.ReadLine();
            host.Close();
        }
    }
}
