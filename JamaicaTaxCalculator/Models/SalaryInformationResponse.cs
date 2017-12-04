using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Models
{
    public class SalaryInformationResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public SalaryDetails Details { get; set; }

        public SalaryInformationResponse()
        {
            Success = false;
            Errors = new List<string>();
            Details = new SalaryDetails();
        }
    }
}
