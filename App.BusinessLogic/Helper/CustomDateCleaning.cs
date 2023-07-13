using App.BusinessLogic.MLModels;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.Helper
{
    [CustomMappingFactoryAttribute("CustomDateMapping")]
    public class CustomDateCleaning : CustomMappingFactory<CleaningServiceOrder, CustomMappingOutputCleaning>
    {
        public static void CustomAction(CleaningServiceOrder input, CustomMappingOutputCleaning output)
        {
            var customDate = Convert.ToDateTime(input.DateTime);
            output.CustomDateHour = ((customDate.Year * 10 + customDate.Month) * 10 + customDate.Day) * 10;
        }

        public override Action<CleaningServiceOrder, CustomMappingOutputCleaning> GetMapping() => CustomAction;
    }
}
