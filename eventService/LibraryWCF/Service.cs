using System;
using System.Text;
using System.ServiceModel;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Threading;

namespace EventsLib
{
    public class Service : IService
    {

        static Action<string, string, int> avaibleEvent = delegate { };

        public void SubscribeAvailableBike(string town, string station, int time)
        {
            IServiceEvent subscriber =
            OperationContext.Current.GetCallbackChannel<IServiceEvent>();
            avaibleEvent += subscriber.avaibleBike;

            string[] param = new string[] { town, station, time.ToString()};

            Thread updater = new Thread(new ParameterizedThreadStart(Update));
            updater.Start(param);
        }

        public void Update(object param)
        {
            string[] mParam = (string[])param;
            string town = mParam[0];
            string station = mParam[1];
            int time = Int32.Parse(mParam[2]);

            while (true)
            {
                WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + town + "&apiKey=fd72347f5b5342b4139b5bc40ac8b0fa058e9552");
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                JArray j = JArray.Parse(responseFromServer);
                foreach (JObject item in j)
                {
                    string name = (string)item.SelectToken("name");
                    if (name.Contains(station.ToUpper()))
                    {
                        int availableVelibs = Convert.ToInt32(item.SelectToken("available_bikes"));
                        avaibleEvent(town, station, availableVelibs);
                    }
                }
                Thread.Sleep(time);
            }
        }
    }
}