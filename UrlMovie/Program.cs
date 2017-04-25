using System;
using System.Net;
using System.IO;

using Newtonsoft.Json.Linq;

namespace UrlMovie
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Pelicula());
            Console.ReadKey();
        }

        static string Pelicula()
        {
            Console.Write("Ingresar un nombre de una pelicula: ");
            string pelicula = Console.ReadLine();
            pelicula = pelicula.ToLower();
            try
            {
                WebRequest req = WebRequest.Create("http://www.omdbapi.com/?t=" + pelicula);

                WebResponse respuesta = req.GetResponse();

                Stream stream = respuesta.GetResponseStream();

                StreamReader sr = new StreamReader(stream);

                JObject data = JObject.Parse(sr.ReadToEnd());

                string titulo = (string)data["Year"];

                string dato = (string)data["Title"];

                if (dato.ToLower() == pelicula)
                    return ("Año de estreno: " + titulo);
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