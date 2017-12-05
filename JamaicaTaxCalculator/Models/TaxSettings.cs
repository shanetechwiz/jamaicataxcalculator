using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Models
{
    public class TaxSettings
    {
        public Decimal TaxThreshold { get; set; }
        public Decimal NISRate { get; set; }
        public Decimal NISCap { get; set; }
        public Decimal EducationTaxRate { get; set; }
        public Decimal NHTRate { get; set; }
        public Decimal DefaultPensionRate { get; set; }
        public Decimal StandardIncomeTaxMark { get; set; }
        public Decimal IncomeTaxRateForIncomeBelowStandardMark { get; set; }
        public Decimal IncomeTaxRateForIncomeAboveStandardMark { get; set; }
    }
}
