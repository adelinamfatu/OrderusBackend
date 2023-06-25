using App.API;
using App.Service;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using App.SMS;

namespace App.Service
{
    public class Program
    {
        static void Main()
        {
            Starter.StartOwin();
            TwilioSMSService.Start();
            Console.ReadLine();
        }
    }
}