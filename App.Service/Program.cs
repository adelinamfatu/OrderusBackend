using App.API;
using App.Service;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using App.Server;

namespace App.Service
{
    public class Program
    {
        static void Main()
        {
            Starter.StartOwin();
            OrderVerificationService.Start();
            TwilioSMSService.Start();
            Console.ReadLine();
        }
    }
}