using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace App.BusinessLogic.MLModels
{
    public class CleaningServiceOrder
    {
        [ColumnName("CompanyID")]
        public float CompanyID { get; set; } 

        [ColumnName("EmployeeID")]
        public float EmployeeID { get; set; }

        [ColumnName("ClientID")]
        public float ClientID { get; set; }

        [ColumnName("DateTime")]
        public DateTime DateTime { get; set; }

        [ColumnName("Hour")]
        public float Hour { get; set; }

        [ColumnName("NbRooms")]
        public float NbRooms { get; set; }

        [ColumnName("Surface")]
        public float Surface { get; set; }

        [ColumnName("Duration")]
        public float Duration { get; set; }
    }

    public class CleaningDurationPrediction
    {
        [ColumnName("Score")]
        public float Duration;
    }

    public class CustomMappingOutput
    {
        [ColumnName("CustomMappingOutput")]
        public float CustomDateHour { get; set; }
    }
}
