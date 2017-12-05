using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Models
{
    public class SalaryDetails
    {
        public Decimal GrossIncome { get; set; }
        public Decimal Pension { get; set; }
        public Decimal NIS { get; set; }
        public Decimal EducationTax { get; set; }
        public Decimal NHT { get; set; }
        public Decimal TotalStatutoryDeductions { get; set; }
        public Decimal IncomeTaxThreshold { get; set; }
        public Decimal TaxableIncome { get; set; }
        public Decimal IncomeTax { get; set; }
        public Decimal NetIncome { get; set; }
    }
}
