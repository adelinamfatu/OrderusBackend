using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BusinessLogic.MLModels
{
    public class ReparationServiceOrder
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

        [ColumnName("NbRepairs")]
        public float NbRepairs { get; set; }

        [ColumnName("Complexity")]
        public float Complexity { get; set; }

        [ColumnName("Duration")]
        public float Duration { get; set; }
    }

    public class ReparationDurationPrediction
    {
        [ColumnName("Score")]
        public float Duration;
    }

    public class CustomMappingOutputRepair
    {
        [ColumnName("CustomMappingOutputRepair")]
        public float CustomDateHour { get; set; }
    }
}
