using JamaicaTaxCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Interfaces
{
    public interface ITaxService
    {
        SalaryDetails GetSalaryDetails(SalaryInformationRequest request);
    }
}
