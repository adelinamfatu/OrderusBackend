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
        public int CompanyID;

        [ColumnName("EmployeeEmail")]
        public string EmployeeEmail;

        [ColumnName("OrderID")]
        public int OrderID;

        [ColumnName("RequireMaterial")]
        public bool RequireMaterial;

        [ColumnName("MaterialQuantity")]
        public int MaterialQuantity;

        [ColumnName("ClientEmail")]
        public string ClientEmail;

        [ColumnName("DateTime")]
        public DateTime DateTime;

        [ColumnName("Rating")]
        public double Rating;

        [ColumnName("NbRooms")]
        public int NbRooms;

        [ColumnName("Surface")]
        public int Surface;

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
