using Congestion.Calculator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator.Controllers
{
    [ApiController]
    [Route("tax")]
    public class TaxCalculator : ControllerBase
    {
        private readonly ICongestionTaxCalculator _congestionTaxCalculator;
        public TaxCalculator(ICongestionTaxCalculator congestionTaxCalculator)
        {
            this._congestionTaxCalculator = congestionTaxCalculator;
        }

        [HttpPost]
        [Route("calculate-congestion")]
        public IActionResult CalculateCongestionTax(TaxInputModel taxableInformation)
        {
            if (ModelState.IsValid)
            {
                return Ok(_congestionTaxCalculator.GetTax(taxableInformation.Vehicle, 
                    taxableInformation.Dates));
            }
            return BadRequest("The Input is not valid");
        }
    }
}