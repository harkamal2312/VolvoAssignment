using System;

namespace Congestion.Calculator.Interfaces
{
    public interface ICongestionTaxCalculator
    {
        int GetTax(string vehicle, DateTime[] dates);
    }

}
