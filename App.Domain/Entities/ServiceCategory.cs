﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    [Table("ServiceCategories")]
    public class ServiceCategory
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
    }
}
