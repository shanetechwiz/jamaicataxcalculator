using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Models
{
    public class TaxSettings
    {
        public double TaxThreshold { get; set; }
        public double NISRate { get; set; }
        public double EducationTaxRate { get; set; }
        public double NHTRate { get; set; }
    }
}
