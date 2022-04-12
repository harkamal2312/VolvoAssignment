using System.ComponentModel.DataAnnotations;

namespace TaxCalculator
{
    public class TaxInputModel
    {
        [Required]
        public DateTime[] Dates { get; set; }
        [Required]
        public string Vehicle { get; set; }
    }
}
