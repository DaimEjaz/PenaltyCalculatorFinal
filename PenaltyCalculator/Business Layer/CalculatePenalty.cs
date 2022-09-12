using PenaltyCalculator.DataLayer;
using PenaltyCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.BusinessLayer
{
    public class CalculatePenalty : ICalculatePenalty
    {
        //Initializing the required varaibles 

        List<string> weekDays = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        ISqlDataHelper _sqlDataHelper;

        //Dependency injection
        public CalculatePenalty(ISqlDataHelper sqlDataHelper)
        {
            _sqlDataHelper = sqlDataHelper;
        }

        //Constructor
        public CalculatePenalty()
        {

        }

        //Method for penalty amount for API, DB dependent. 
        //*****************Independent one is after it*********************
        public Penalty CalculateAmount(DateTime checkoutDate, DateTime returnedDate, int countryId)
        {
            //Getting Data from DB

            Country selectedCountry = _sqlDataHelper.GetCountry(countryId);

            List<string> weekendDays = _sqlDataHelper.GetWeekendDays(weekDays, selectedCountry.weekend);

            List<Holiday> allHolidays = _sqlDataHelper.GetHolidays(countryId);

            //Initialzing variables
            double totalPenalty = 0;

            double taxAdditon = 0;

            string penaltyStr = "";

            List<Holiday> includedHolidays = new List<Holiday>();


            //If last day is before first day, raise EXCEPTION

            DateTime firstDay = checkoutDate.Date;
            DateTime lastDay = returnedDate.Date;
            

            TimeSpan totalTime = lastDay - firstDay;

            int businessDays = totalTime.Days + 1;

            DateTime day = firstDay;

            while (day <= lastDay)
            {
                //Subtracting every weekend day

                for (int i = 0; i < weekendDays.Count; i++)
                {
                    if ((day.DayOfWeek).ToString() == weekendDays[i])
                    {
                        businessDays--;
                    }
                }
                day = day.AddDays(1);
            }

            for (int i = 0; i < allHolidays.Count; i++)
            {
                //Subtracting holidays

                if (firstDay.CompareTo(allHolidays[i].date) <= 0 && lastDay.CompareTo(allHolidays[i].date) >= 0)
                {
                    includedHolidays.Add(allHolidays[i]);
                    businessDays--;
                }
            }

            for (int i = 0; i < includedHolidays.Count; i++)
            {
                string dayOfHoliday = includedHolidays[i].date.DayOfWeek.ToString();

                //Checking for holidays that fell on weekends
                for (int count = 0; count < weekendDays.Count; count++)
                {
                    if (dayOfHoliday == weekendDays[count])
                    {
                        businessDays++;
                    }
                }
                
            }


            //Method for Penalty calculation

            if (businessDays > 10)
            {
                totalPenalty = (businessDays - 10) * selectedCountry.penaltyPerDay;

                taxAdditon = totalPenalty * (selectedCountry.tax / 100);

                totalPenalty = totalPenalty + taxAdditon;

                penaltyStr = selectedCountry.currencySymbol + " " + totalPenalty.ToString();

                Penalty penaltyObj = new Penalty(businessDays, penaltyStr);

                return penaltyObj;
            }
            else
            {
                penaltyStr = "0";
                Penalty penaltyObj = new Penalty(businessDays, penaltyStr);
                return penaltyObj;

            }
        }

        public List<Country> GetCountries()
        {
            List<Country> countries = _sqlDataHelper.GetAllCountries();
            return countries;
        }

        //Method for penalty calculation, Decoupled from DB
        //Made for unit testing
        //All the steps are the same as for the above method

        public Penalty CalculatePenaltyAmount(DateTime checkoutDate, DateTime returnedDate, int countryId, List<Country> countries, List<string> weekendDays, List<Holiday> allHolidays)
        {
            List<Holiday> includedHolidays = new List<Holiday>();
            double totalPenalty = 0;

            double taxAdditon = 0;

            string penaltyStr = "";

            Country selectedCountry = new Country() ;
            for(int i = 0; i < countries.Count; i++)
            {
                if(countries[i].countryId == countryId)
                {
                    selectedCountry = countries[i];
                }
            }

            DateTime firstDay = checkoutDate.Date;
            DateTime lastDay = returnedDate.Date;
            

            TimeSpan totalTime = lastDay - firstDay;

            int businessDays = totalTime.Days + 1;

            DateTime day = firstDay;

            while (day <= lastDay)
            {
                //Subtracting every weekend day

                for (int i = 0; i < weekendDays.Count; i++)
                {
                    if ((day.DayOfWeek).ToString() == weekendDays[i])
                    {
                        businessDays--;
                    }
                }
                day = day.AddDays(1);
            }

            for (int i = 0; i < allHolidays.Count; i++)
            {
                //Subtracting holidays

                if (firstDay.CompareTo(allHolidays[i].date) <= 0 && lastDay.CompareTo(allHolidays[i].date) >= 0)
                {
                    includedHolidays.Add(allHolidays[i]);
                    businessDays--;
                }
            }

            for (int i = 0; i < includedHolidays.Count; i++)
            {
                string dayOfHoliday = includedHolidays[i].date.DayOfWeek.ToString();

                //Checking for holidays that fell on weekends
                for (int count = 0; count < weekendDays.Count; count++)
                {
                    if (dayOfHoliday == weekendDays[count])
                    {
                        businessDays++;
                    }
                }

            }


            //Method for Penalty calculation

            if (businessDays > 10)
            {
                totalPenalty = (businessDays - 10) * selectedCountry.penaltyPerDay;

                taxAdditon = totalPenalty * (selectedCountry.tax / 100);

                totalPenalty = totalPenalty + taxAdditon;

                penaltyStr = selectedCountry.currencySymbol + " " + totalPenalty.ToString();

                Penalty penaltyObj = new Penalty(businessDays, penaltyStr);

                return penaltyObj;
            }
            else
            {
                penaltyStr = "0";
                Penalty penaltyObj = new Penalty(businessDays, penaltyStr);
                return penaltyObj;

            }
        }

    }
}
