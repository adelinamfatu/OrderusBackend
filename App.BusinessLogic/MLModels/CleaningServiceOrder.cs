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
        public float CompanyID;

        [ColumnName("EmployeeEmail")]
        public string EmployeeEmail;

        [ColumnName("ClientEmail")]
        public string ClientEmail;

        [ColumnName("DateTime")]
        public DateTime DateTime;

        [ColumnName("Rating")]
        public float Rating;

        [ColumnName("NbRooms")]
        public float NbRooms;

        [ColumnName("Surface")]
        public float Surface;

        [ColumnName("Duration")]
        public float Duration;
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
