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

        public IActionResult Calculate(SalaryInformationRequest request)
        {
            SalaryInformationResponse response = new SalaryInformationResponse();
            try
            {
                response.Details = _taxService.GetSalaryDetails(request);
                response.Success = true;
            }
            catch (Exception ex)
            {
                var errorMessage = $"An exception occured while trying to process your request.";
                Console.WriteLine($"{errorMessage} {ex.Message} - {ex.StackTrace}");
                response = new SalaryInformationResponse();
                response.Errors.Add(errorMessage);
            }

            return new JsonResult(response);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
