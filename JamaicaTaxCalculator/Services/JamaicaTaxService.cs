using JamaicaTaxCalculator.Interfaces;
using JamaicaTaxCalculator.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamaicaTaxCalculator.Services
{
    public class JamaicaTaxService : ITaxService
    {
        private readonly TaxSettings _taxSettings;
        private readonly Features _features;

        public JamaicaTaxService(IOptions<TaxSettings> taxSettingsAccessor, IOptions<Features> featuresAccessor)
        {
            _taxSettings = taxSettingsAccessor.Value;
            _features = featuresAccessor.Value;
        }

        public SalaryDetails GetSalaryDetails(SalaryInformationRequest request)
        {
            var salaryDetails = new SalaryDetails();
            var grossIncome = request.details.GrossIncome;

            if (grossIncome > 0)
            {
                salaryDetails.GrossIncome = grossIncome;
                salaryDetails.IncomeTaxThreshold = _taxSettings.TaxThreshold;
                
                // TODO: Refactor into smaller more maintainable parts
                if (_features.UsePension)
                {
                    salaryDetails.Pension = _taxSettings.DefaultPensionRate * grossIncome;
                    salaryDetails.TotalStatutoryDeductions += salaryDetails.Pension;
                }

                if(_features.UseNISRate)
                {
                    salaryDetails.NIS = _taxSettings.NISRate * grossIncome;

                    if (_features.UseNISCap && salaryDetails.NIS > _taxSettings.NISCap)
                    {
                        salaryDetails.NIS = _taxSettings.NISCap;
                    }

                    salaryDetails.TotalStatutoryDeductions += salaryDetails.NIS;
                }                

                if (_features.UseEducationTaxRate)
                {
                    salaryDetails.EducationTax = _taxSettings.EducationTaxRate * (grossIncome - salaryDetails.TotalStatutoryDeductions);
                    salaryDetails.TotalStatutoryDeductions += salaryDetails.EducationTax;
                }

                if (_features.UseNHTRate)
                {
                    salaryDetails.NHT = _taxSettings.NHTRate * grossIncome;
                    salaryDetails.TotalStatutoryDeductions += salaryDetails.NHT;
                }

                var statutoryIncome = grossIncome - salaryDetails.TotalStatutoryDeductions;
                var taxableIncome = (grossIncome - (salaryDetails.Pension + salaryDetails.NIS)) - _taxSettings.TaxThreshold;
                salaryDetails.TaxableIncome = taxableIncome;

                if(_features.UseIncomeTax)
                {
                    salaryDetails.IncomeTax = (grossIncome > _taxSettings.StandardIncomeTaxMark) 
                        ? taxableIncome * _taxSettings.IncomeTaxRateForIncomeAboveStandardMark
                        : taxableIncome * _taxSettings.IncomeTaxRateForIncomeBelowStandardMark;                    
                }

                salaryDetails.NetIncome = statutoryIncome - salaryDetails.IncomeTax;

            }

            return salaryDetails;
        }
    }
}
