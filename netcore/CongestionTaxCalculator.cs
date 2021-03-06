using Congestion.Calculator.Interfaces;
using System;
using System.Collections.Generic;

namespace Congestion.Calculator
{
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        private static List<TaxSlabsModel> _parameters = null;
        public CongestionTaxCalculator()
        {
            var parameter = new TaxParameter();
            _parameters = parameter.TaxParameters;
        }
        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

        public int GetTax(string vehicle, DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            if (IsTollFreeVehicle(vehicle))
                return 0;
            foreach (DateTime date in dates)
            {
                int nextFee = GetTollFee(date);
                int tempFee = GetTollFee(intervalStart);

                var timeSpan = date.Subtract(intervalStart);
                if (timeSpan.Days == 0 && timeSpan.Hours == 0 && timeSpan.Minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }

                intervalStart = date;
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private static bool IsTollFreeVehicle(string vehicle)
        {
            if (vehicle == null)
                return false;
            return string.Equals(vehicle, TollFreeVehicles.Motorcycle.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
                   string.Equals(vehicle, TollFreeVehicles.Emergency.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
                   string.Equals(vehicle, TollFreeVehicles.Diplomat.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
                   string.Equals(vehicle, TollFreeVehicles.Foreign.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
                   string.Equals(vehicle, TollFreeVehicles.Busses.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
                   string.Equals(vehicle, TollFreeVehicles.Military.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }

        private int GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date))
                return 0;

            return CalculateTollFeeFromJson(date, out var tollFee)
                ? tollFee
                : 0;
        }

        private static bool CalculateTollFeeFromJson(DateTime date, out int tollFee)
        {

            var vehicleTimeSpan = new TimeSpan(date.Hour, date.Minute, 0);
            var calculatedFee = 0;
            foreach (var slabs in _parameters)
            {
                if (slabs.StartTime < slabs.EndTime)
                {
                    if (vehicleTimeSpan >= slabs.StartTime && vehicleTimeSpan <= slabs.EndTime)
                    {
                        calculatedFee = slabs.Toll;
                    }
                }
                else
                {
                    //for clock after 12 midnight
                    if (!(slabs.EndTime < vehicleTimeSpan && vehicleTimeSpan < slabs.StartTime))
                    {
                        calculatedFee = slabs.Toll;
                    }
                }

            }
            tollFee = calculatedFee;
            return true;
        }

        /// <summary>
        /// /This method was used earlier basis the static parameters for tax calculations
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="tollFee"></param>
        /// <returns></returns>
        private static bool CalculateTollFee(int hour, int minute, out int tollFee)
        {

            if (hour == 6 && minute >= 0 && minute <= 29)
            {
                tollFee = 8;
                return true;
            }

            if (hour == 6 && minute >= 30 && minute <= 59)
            {
                tollFee = 13;
                return true;
            }

            if (hour == 7 && minute >= 0 && minute <= 59)
            {
                tollFee = 18;
                return true;
            }

            if (hour == 8 && minute >= 0 && minute <= 29)
            {
                tollFee = 13;
                return true;
            }

            if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59)
            {
                tollFee = 8;
                return true;
            }

            if (hour == 15 && minute >= 0 && minute <= 29)
            {
                tollFee = 13;
                return true;
            }

            if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59)
            {
                tollFee = 18;
                return true;
            }

            if (hour == 17 && minute >= 0 && minute <= 59)
            {
                tollFee = 13;
                return true;
            }

            if (hour == 18 && minute >= 0 && minute <= 29)
            {
                tollFee = 8;
                return true;
            }

            tollFee = 0;
            return false;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            return IsWeekend(date) || year == 2013 && IsHoliday(month, day);
        }

        private static bool IsHoliday(int month, int day)
        {
            return month == 1 && day == 1 ||
                   month == 3 && (day == 28 || day == 29) ||
                   month == 4 && (day == 1 || day == 30) ||
                   month == 5 && (day == 1 || day == 8 || day == 9) ||
                   month == 6 && (day == 5 || day == 6 || day == 21) ||
                   month == 7 ||
                   month == 11 && day == 1 ||
                   month == 12 && (day == 24 || day == 25 || day == 26 || day == 31);
        }

        private static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}