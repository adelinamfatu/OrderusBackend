using App.BusinessLogic.ServicesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Server
{
    public static class OrderVerificationService
    {
        public async static void Start()
        {
            var orderDisplay = new OrdersDisplay();

            while (true)
            {
                await Task.Run(() =>
                {
                    orderDisplay.VerifyOrderCancellation();
                });

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
