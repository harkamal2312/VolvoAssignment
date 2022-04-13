using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CongestionTaxCalculator.Tests
{
    public class TaxCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateCongestionTaxTestForCar()
        {
            var datesArray = new DateTime[5];
            datesArray[0] = Convert.ToDateTime("2013-01-14 21:00:00");
            datesArray[1] = Convert.ToDateTime("2013-01-15 21:00:00");
            datesArray[2] = Convert.ToDateTime("2013-02-07 06:23:27");
            datesArray[3] = Convert.ToDateTime("2013-02-07 15:27:00");
            datesArray[4] = Convert.ToDateTime("2013-02-08 06:27:00");

            var taxCalculator = new Congestion.Calculator.CongestionTaxCalculator();
            var taxAmount=  taxCalculator.GetTax("car", datesArray);

            Assert.AreEqual(29,taxAmount);
        }
        [Test]
        public void CalculateCongestionTaxTestForChargableWithTwoTollInSameHour()
        {
            var datesArray = new List<DateTime>
            {
                Convert.ToDateTime("2013-02-08 06:20:27"),
                Convert.ToDateTime("2013-02-08 14:35:00"),
                Convert.ToDateTime("2013-02-08 15:29:00"),
                Convert.ToDateTime("2013-02-08 15:47:00"),
                Convert.ToDateTime("2013-02-08 16:01:00")
            };

            var taxCalculator = new Congestion.Calculator.CongestionTaxCalculator();
            var taxAmount = taxCalculator.GetTax("car", datesArray.ToArray());

            Assert.AreEqual(26, taxAmount);
            var truckAmount = taxCalculator.GetTax("Truck", datesArray.ToArray());

            Assert.AreEqual(26, truckAmount);
        }

        [Test]
        public void CalculateCongestionTaxTestForWeekend()
        {
            var datesArray = new List<DateTime>
            {
                Convert.ToDateTime("2013-02-09 06:20:27"),
                Convert.ToDateTime("2013-02-09 14:35:00"),
                Convert.ToDateTime("2013-02-09 15:29:00"),
                Convert.ToDateTime("2013-02-10 15:47:00"),
                Convert.ToDateTime("2013-02-10 16:01:00")
            };

            var taxCalculator = new Congestion.Calculator.CongestionTaxCalculator();
            var taxAmount = taxCalculator.GetTax("car", datesArray.ToArray());

            Assert.AreEqual(0, taxAmount);
        }

        [Test]
        public void CalculateCongestionTaxTestJuly()
        {
            var datesArray = new List<DateTime>
            {
                Convert.ToDateTime("2013-07-09 06:20:27"),
                Convert.ToDateTime("2013-07-09 14:35:00"),
                Convert.ToDateTime("2013-07-09 15:29:00"),
                Convert.ToDateTime("2013-07-10 15:47:00"),
                Convert.ToDateTime("2013-07-10 16:01:00")
            };

            var taxCalculator = new Congestion.Calculator.CongestionTaxCalculator();
            var taxAmount = taxCalculator.GetTax("car", datesArray.ToArray());

            Assert.AreEqual(0, taxAmount);
        }
        [Test]
        public void CalculateCongestionTaxTestForAllTimeStamps()
        {
            var datesArray = new List<DateTime>
            {
                Convert.ToDateTime("2013-01-14 21:00:00"),
                Convert.ToDateTime("2013-01-15 21:00:00"),
                Convert.ToDateTime("2013-02-07 06:23:27"),
                Convert.ToDateTime("2013-02-07 15:27:00"),
                Convert.ToDateTime("2013-02-08 06:27:00"),
                Convert.ToDateTime("2013-02-08 06:20:27"),
                Convert.ToDateTime("2013-02-08 14:35:00"),
                Convert.ToDateTime("2013-02-08 15:29:00"),
                Convert.ToDateTime("2013-02-08 15:47:00"),
                Convert.ToDateTime("2013-02-08 16:01:00"),
                Convert.ToDateTime("2013-02-08 16:48:00"),
                Convert.ToDateTime("2013-02-08 17:49:00"),
                Convert.ToDateTime("2013-02-08 18:29:00"),
                Convert.ToDateTime("2013-02-08 18:35:00"),
                Convert.ToDateTime("2013-03-26 14:25:00"),
                Convert.ToDateTime("2013-03-28 14:07:27")
            };

            var taxCalculator = new Congestion.Calculator.CongestionTaxCalculator();
            var taxAmount = taxCalculator.GetTax("car", datesArray.ToArray());

            Assert.AreEqual(60, taxAmount);
            var truckAmount = taxCalculator.GetTax("Truck", datesArray.ToArray());

            Assert.AreEqual(60, truckAmount);
        }
        [Test]
        public void CalculateCongestionTaxTestForAllTimeStampsForExludedVehicle()
        {
            var datesArray = new List<DateTime>
            {
                Convert.ToDateTime("2013-01-14 21:00:00"),
                Convert.ToDateTime("2013-01-15 21:00:00"),
                Convert.ToDateTime("2013-02-07 06:23:27"),
                Convert.ToDateTime("2013-02-07 15:27:00"),
                Convert.ToDateTime("2013-02-08 06:27:00"),
                Convert.ToDateTime("2013-02-08 06:20:27"),
                Convert.ToDateTime("2013-02-08 14:35:00"),
                Convert.ToDateTime("2013-02-08 15:29:00"),
                Convert.ToDateTime("2013-02-08 15:47:00"),
                Convert.ToDateTime("2013-02-08 16:01:00"),
                Convert.ToDateTime("2013-02-08 16:48:00"),
                Convert.ToDateTime("2013-02-08 17:49:00"),
                Convert.ToDateTime("2013-02-08 18:29:00"),
                Convert.ToDateTime("2013-02-08 18:35:00"),
                Convert.ToDateTime("2013-03-26 14:25:00"),
                Convert.ToDateTime("2013-03-28 14:07:27")
            };

            var taxCalculator = new Congestion.Calculator.CongestionTaxCalculator();
            var taxAmount = taxCalculator.GetTax("Motorcycle", datesArray.ToArray());

            Assert.AreEqual(0, taxAmount);
           
        }


    }
}