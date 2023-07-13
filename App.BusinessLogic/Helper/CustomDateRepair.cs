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
    public class CustomDateRepair : CustomMappingFactory<ReparationServiceOrder, CustomMappingOutputRepair>
    {
        public static void CustomAction(ReparationServiceOrder input, CustomMappingOutputRepair output)
        {
            var customDate = Convert.ToDateTime(input.DateTime);
            output.CustomDateHour = ((customDate.Year * 10 + customDate.Month) * 10 + customDate.Day) * 10;
        }

        public override Action<ReparationServiceOrder, CustomMappingOutputRepair> GetMapping() => CustomAction;
    }
}
