using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace App.API
{
    public class Starter
    {
        static IDisposable owinService;

        public static void StartOwin()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            owinService = WebApp.Start<WebConfig>(url: baseAddress);
        }
    }
}