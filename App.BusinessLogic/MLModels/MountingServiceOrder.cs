using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.MLModels
{
    public class MountingServiceOrder
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

        [ColumnName("NbObjects")]
        public float NbObjects { get; set; }

        [ColumnName("Size")]
        public float Size { get; set; }

        [ColumnName("Duration")]
        public float Duration { get; set; }
    }

    public class MountingDurationPrediction
    {
        [ColumnName("Score")]
        public float Duration;
    }

    public class CustomMappingOutputMounting
    {
        [ColumnName("CustomMappingOutputMounting")]
        public float CustomDateHour { get; set; }
    }
}
