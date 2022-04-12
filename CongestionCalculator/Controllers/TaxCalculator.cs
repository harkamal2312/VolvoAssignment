using Congestion.Calculator.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TaxCalculator.Controllers
{
    [ApiController]
    [Route("Tax")]
    public class TaxCalculator : ControllerBase
    {
        private readonly ICongestionTaxCalculator _congestionTaxCalculator;
        public TaxCalculator(ICongestionTaxCalculator congestionTaxCalculator)
        {
            this._congestionTaxCalculator = congestionTaxCalculator;
        }

        [HttpPost]
        [Route("CalculateCongestionTax")]
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