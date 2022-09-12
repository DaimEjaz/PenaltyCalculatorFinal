using NUnit.Framework;
using PenaltyCalculator.BusinessLayer;
using PenaltyCalculator.Models;
using System;
using System.Collections.Generic;

namespace PenaltyUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void BusinessDays_Are_Greater_Than_10_With_Holidays_And_Weekends()
        {
            //Arrange, create all inputs

            Country country = new Country(1, "Pakistan", "Pakistani rupees", "PKR", "0000011", 0, 50);
            List<Country> countries = new List<Country>() { country, new Country(2, "UAE", "UAE Dirham", "AED", "0000110", 8, 5) };
            List<string> weekendDays = new List<string>() { "Saturday", "Sunday" };
            DateTime holidayTime = new DateTime(2022, 08, 14);
            DateTime secondHoliday = new DateTime(2022, 03, 23);
            List<Holiday> holidays = new List<Holiday>() { new Holiday(1, "Independence Day", holidayTime, 1), new Holiday(2, "Pakistan Day", secondHoliday, 1) };
            DateTime checkoutDate = new DateTime(2022, 08, 10);
            DateTime returnedDate = new DateTime(2022, 08, 30);
            int countryId = country.countryId;

            //Act, call the function

            CalculatePenalty calculatePenalty = new CalculatePenalty();
            Penalty penalty = calculatePenalty.CalculatePenaltyAmount(checkoutDate, returnedDate, countryId, countries, weekendDays, holidays);


            //Approve, test the output

            Assert.Less(10, penalty.businessDays);
            Assert.AreNotSame("PKR 250", penalty.totalPenalty);
        }

        [Test]
        public void BusinessDays_Are_Less_Than_10_With_Holidays_And_Weekends()
        {
            //Arrange

            Country country = new Country(1, "Pakistan", "Pakistani rupees", "PKR", "0000011", 0, 50);
            List<Country> countries = new List<Country>() { country, new Country(2, "UAE", "UAE Dirham", "AED", "0000110", 8, 5) };
            List<string> weekendDays = new List<string>() { "Saturday", "Sunday" };
            DateTime holidayTime = new DateTime(2022, 08, 14);
            DateTime secondHoliday = new DateTime(2022, 03, 23);
            List<Holiday> holidays = new List<Holiday>() { new Holiday(1, "Independence Day", holidayTime, 1), new Holiday(2, "Pakistan Day", secondHoliday, 1) };
            DateTime checkoutDate = new DateTime(2022, 08, 10);
            DateTime returnedDate = new DateTime(2022, 08, 20);
            int countryId = country.countryId;

            //Act

            CalculatePenalty calculatePenalty = new CalculatePenalty();
            Penalty penalty = calculatePenalty.CalculatePenaltyAmount(checkoutDate, returnedDate, countryId, countries, weekendDays, holidays);


            //Approve
            Assert.Greater(10, penalty.businessDays);
            Assert.AreSame("0", penalty.totalPenalty);
        }

        [Test]
        public void BusinessDays_Are_Greater_Than_10_Without_Holidays_With_Weekends()
        {
            //Arrange

            Country country = new Country(1, "Pakistan", "Pakistani rupees", "PKR", "0000011", 0, 50);
            List<Country> countries = new List<Country>() { country, new Country(2, "UAE", "UAE Dirham", "AED", "0000110", 8, 5) };
            List<string> weekendDays = new List<string>() { "Saturday", "Sunday" };
            DateTime holidayTime = new DateTime(2022, 08, 14);
            DateTime secondHoliday = new DateTime(2022, 03, 23);
            List<Holiday> holidays = new List<Holiday>() { new Holiday(1, "Independence Day", holidayTime, 1), new Holiday(2, "Pakistan Day", secondHoliday, 1) };
            DateTime checkoutDate = new DateTime(2022, 08, 15);
            DateTime returnedDate = new DateTime(2022, 08, 30);
            int countryId = country.countryId;

            //Act

            CalculatePenalty calculatePenalty = new CalculatePenalty();
            Penalty penalty = calculatePenalty.CalculatePenaltyAmount(checkoutDate, returnedDate, countryId, countries, weekendDays, holidays);


            //Approve
            Assert.Less(10, penalty.businessDays);
            Assert.AreEqual("PKR 100", penalty.totalPenalty);
        }

        [Test]
        public void BusinessDays_Are_Lesser_Than_10_Without_Holidays_With_Weekends()
        {
            //Arrange
           
            Country country = new Country(1, "Pakistan", "Pakistani rupees", "PKR", "0000011", 0, 50);
            List<Country> countries = new List<Country>() { country, new Country(2, "UAE", "UAE Dirham", "AED", "0000110", 8, 5) };
            List<string> weekendDays = new List<string>() { "Saturday", "Sunday" };
            DateTime holidayTime = new DateTime(2022, 08, 14);
            DateTime secondHoliday = new DateTime(2022, 03, 23);
            List<Holiday> holidays = new List<Holiday>() { new Holiday(1, "Independence Day", holidayTime, 1), new Holiday(2, "Pakistan Day", secondHoliday, 1) };
            DateTime checkoutDate = new DateTime(2022, 08, 15);
            DateTime returnedDate = new DateTime(2022, 08, 22);
            int countryId = country.countryId;

            //Act

            CalculatePenalty calculatePenalty = new CalculatePenalty();
            Penalty penalty = calculatePenalty.CalculatePenaltyAmount(checkoutDate, returnedDate, countryId, countries, weekendDays, holidays);


            //Approve
            Assert.GreaterOrEqual(10, penalty.businessDays);
            Assert.AreEqual("0", penalty.totalPenalty);
        }
    }
}