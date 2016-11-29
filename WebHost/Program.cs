using System;
using Microsoft.Owin.Hosting;
using CyApi;

namespace WebHost
{
    internal class Program
    {
        public static void Main()
        {
            Start();
        }

        private static void Start()
        {
            //Assembly.LoadFile(System.AppDomain.CurrentDomain.BaseDirectory + "BaseApi.dll");
            // Specify the URI to use for the local host:
            var baseUri = "http://localhost:8080";
            var options = new StartOptions();
            options.Urls.Add(baseUri);
            //options.Urls.Add("http://localhost:8081");
            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(options);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", baseUri);
            Console.ReadLine();
        }
    }
}