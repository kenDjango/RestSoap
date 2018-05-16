using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventClient
{
    class ServiceCallBackSink : ServiceReference.IServiceCallback
    {
        public void avaibleBike(string town, string station, int number)
        {
            var time = DateTime.Now;
            Console.WriteLine(town + " - " + station + "  Number of bike avaible at " + time.Hour + ":" + time.Minute + ":" + time.Second + " is " + number);
        }
    }
}
