using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleClient
{
    class Program
    {
        private static string processCommand(string command, Service1Client client)
        {
            string res = "\n";
            string[] tokens = command.Split(' ');
            string[] response;
            switch (tokens[0])
            {
                case "help":
                    res += "Commandes disponibles:\n";
                    res += "contracts\n";
                    res += "stations town\n";
                    res += "bikes town station\n";
                    break;
                case "contracts":
                    response = client.GetAllContract();
                    for(int i= 0; i< response.Length;i++)
                    {
                        res += response[i] + "\n";
                    }
                    break;
                case "stations":
                    response = client.GetStations(tokens[1]);
                    for (int i = 0; i < response.Length; i++)
                    {
                        res += response[i] + "\n";
                    }
                    break;
                case "bikes":
                    res = client.GetAvaibleBike(tokens[1], mergeStrings(tokens)) + "\n"; 
                    break;
                default:
                    res = "Erreur: Command invalide";
                    break;
            }
            return res + "\n";
        }

        private static string mergeStrings(string[] strings)
        {
            string res = "";
            for(int i = 2; i < strings.Length; i++)
            {
                res += strings[i] + " ";
            }
            return res.TrimEnd();
        }

        static void Main(string[] args)
        {
            Service1Client client = new Service1Client();
            Console.Write("Début du client Velib\n");
            Console.Write("Tapez help pour avoir la liste des commandes\n");
            Console.Write("Tapez exit pour fermer le client\n\n");
            String entry = "";
            while (entry != "exit")
            {
                entry = Console.ReadLine();
                Console.Write(processCommand(entry, client));
            }
            client.Close();
        }
    }
}
