using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Models
{
    public class Features
    {
        public bool UseTaxThreshold { get; set; }
        public bool UseNISRate { get; set; }
        public bool UseNISCap { get; set; }
        public bool UseEducationTaxRate { get; set; }
        public bool UseNHTRate { get; set; }
        public bool UsePension { get; set; }
        public bool UseIncomeTax { get; set; }
    }
}
