using JamaicaTaxCalculator.Interfaces;
using JamaicaTaxCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Services
{
    public class JamaicaTaxService : ITaxService
    {

        public SalaryDetails GetSalaryDetails(SalaryInformationRequest request)
        {
            var salaryDetails = new SalaryDetails();

            return salaryDetails;
        }
    }
}
