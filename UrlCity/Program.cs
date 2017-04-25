using System;
using System.IO;
using System.Net;

using Newtonsoft.Json.Linq;

namespace UrlCity
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Ciudad());
            Console.ReadKey();
        }

        static string Ciudad()
        {
            Console.Write("Ingresar un nombre de una ciudad: ");
            string ciudad = Console.ReadLine();
            ciudad = ciudad.ToLower();
            try
            {
                WebRequest req = WebRequest.Create("https://maps.googleapis.com/maps/api/geocode/json?address=" + ciudad);

                WebResponse respuesta = req.GetResponse();

                Stream stream = respuesta.GetResponseStream();

                StreamReader sr = new StreamReader(stream);

                JObject data = JObject.Parse(sr.ReadToEnd());

                string dato = "";
                int i = -1;
                do
                {
                    i++;
                    try
                    {
                        dato = (string)data["results"][0]["address_components"][i]["types"][0];
                    }
                    catch
                    {
                        dato = "error";
                    }
                } while (dato != "country" && dato != "error");

                string titulo = (string)data["results"][0]["address_components"][i]["long_name"];
                dato = (string)data["results"][0]["address_components"][0]["long_name"];
                if (dato.ToLower() == ciudad)
                    return ("Pertenece a " + titulo);
                else
                    return "Nombre Invalido";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
