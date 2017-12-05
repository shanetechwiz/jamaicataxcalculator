using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JamaicaTaxCalculator.Models;
using Microsoft.Extensions.Options;
using JamaicaTaxCalculator.Interfaces;

namespace JamaicaTaxCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaxSettings _taxSettings;
        private readonly Features _features;
        private readonly ITaxService _taxService;

        public HomeController(IOptions<TaxSettings> taxSettingsAccessor, IOptions<Features> featuresAccessor,
            ITaxService taxServiceAccessor)
        {
            _taxSettings = taxSettingsAccessor.Value;
            _features = featuresAccessor.Value;
            _taxService = taxServiceAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("api/Calculate")]
        public IActionResult Calculate(Decimal grossIncome, bool hasPension = false, Decimal pensionRate = 0)
        {
            SalaryInformationRequest request = new SalaryInformationRequest();
            SalaryInformationResponse response = new SalaryInformationResponse();
            try
            {
                request.details.GrossIncome = grossIncome;

                response.Details = _taxService.GetSalaryDetails(request);
                response.Success = true;
            }
            catch (Exception ex)
            {
                var errorMessage = "An exception occured while trying to process your request.";
                Console.WriteLine($"{errorMessage} {ex.Message} - {ex.StackTrace}");
                response = new SalaryInformationResponse();
                response.Errors.Add(errorMessage);

                return BadRequest(response);
            }

            return Ok(response);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
