using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace velibService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        public List<string> GetAllContract()
        {
            List<string> result = new List<string>();
            //do the request to get all access point
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts/?apiKey=fd72347f5b5342b4139b5bc40ac8b0fa058e9552");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            JArray j = JArray.Parse(responseFromServer);
            foreach (JObject item in j)
            {
                result.Add((string)item.SelectToken("name"));
            }
            reader.Close();
            response.Close();
            return result;
        }

        public string GetAvaibleBike(string town, string station)
        {
            string result = "";
            //do the request to get all access point
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
                    result = (string)item.SelectToken("available_bikes");
                    break;
                }
            }
            reader.Close();
            response.Close();
            return result;
        }

        public List<string> GetStations(string town)
        {
            List<string> result = new List<string>();
            //do the request to get all access point
            WebRequest request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract="+town+"&apiKey=fd72347f5b5342b4139b5bc40ac8b0fa058e9552");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            JArray j = JArray.Parse(responseFromServer);
            foreach (JObject item in j)
            {
                result.Add((string)item.SelectToken("name"));
            }

            reader.Close();
            response.Close();
            return result;
        }
    }
}
