using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Models
{
    public class SalaryInformationRequest
    {
        public SalaryDetails details { get; set; }

        public SalaryInformationRequest()
        {
            details = new SalaryDetails();
        }
    }
}
