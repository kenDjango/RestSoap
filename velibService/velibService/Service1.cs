using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace velibService
{
    // REMARQUE : vous pouvez utiliser la commade Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        //structure de donnée pour stocker le resultat des requetes (déja effectué) i.e le cache
        private static List<string> contractCache = new List<string>();
        private static Dictionary<string, List<string>> stationsCache = new Dictionary<string, List<string>>();

        public async Task<List<string>> GetAllContract()
        {
            if (contractCache.Count >  0)
            {
                contractCache.Add("test");
                return contractCache;
            }
            else
            {
                List<string> result = new List<string>();
                //do the request to get all access point
                WebRequest request = await Task.Run(() => request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts/?apiKey=fd72347f5b5342b4139b5bc40ac8b0fa058e9552"));
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
                contractCache.AddRange(result);
                return contractCache;
            }
        }

        public async Task<List<string>> GetStations(string town)
        {
            if (stationsCache.ContainsKey(town))
            {
                return stationsCache[town];
            }
            else
            {
                List<string> result = new List<string>();
                //do the request to get all access point
                try
                {
                    WebRequest request = await Task.Run(() => request = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + town + "&apiKey=fd72347f5b5342b4139b5bc40ac8b0fa058e9552"));
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
                    stationsCache.Add(town, result);
                    return result;
                }
                catch (WebException e)
                {
                    result.Add("Ville inconnue");
                    return result;
                }
            }
        }

        public async Task<string> GetAvaibleBike(string town, string station)
        {
            string result = "station inexistante";
            try
            {
                WebRequest request = await Task.Run(() => request =  WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + town + "&apiKey=fd72347f5b5342b4139b5bc40ac8b0fa058e9552"));
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
            catch (WebException e)
            {
                result = "ville invalide";
                return result;
            }
        }
    }
}
