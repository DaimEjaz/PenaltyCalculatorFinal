using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PenaltyCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.DataLayer
{
    public class SqlDataHelper : ISqlDataHelper
    {
        //Initializng the variables

        string conString = "";

        List<Country> countryList = new List<Country>();

        List<Holiday> holidayList = new List<Holiday>();


        //Dependency injection
        public SqlDataHelper(IConfiguration configuration)
        {
            conString = configuration.GetConnectionString("DefaultConnection");
        }

        //Fetches & returns the countries list from DB
        public List<Country> GetAllCountries()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM CountryTable;", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Creates country objects from DB data & add into list of countries

                            Country country = new Country(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), Convert.ToDouble(reader.GetValue(5)), Convert.ToDouble(reader.GetValue(6)));
                            countryList.Add(country);
                        }
                    }

                }
                con.Close();
                return countryList;

            }
        }

        //Fetches & returns the holidays list for a particular country from DB

        public List<Holiday> GetHolidays(int countryId)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM HolidaysTbl WHERE countryId=@countryId;", con))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@countryId";
                    param.Value = countryId;

                    command.Parameters.Add(param);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Creates holiday objects from DB data & add into list of holidays

                            Holiday holiday = new Holiday(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), (DateTime)reader.GetValue(2), Convert.ToInt32(reader.GetValue(3)));
                            holidayList.Add(holiday);
                        }
                    }

                }
                con.Close();
                return holidayList;

            }
        }

        //Fetches & returns the weekends list for a particular country from DB

        public List<string> GetWeekendDays(List<string> WeekDays, string weekendString)
        {
            int length = WeekDays.Count;
            List<string> tempCopy = new List<string>(WeekDays);

            for (int count = 0; count < length; count++)
            {
                //Weekend string is of the form "0000011" , where 0 signifies business day while 1 signifies weekend
                //Checking if current day(from the string[]) is not 1, if yes then remove it
                if (!(weekendString[count] is '1'))
                {
                    tempCopy.Remove(WeekDays[count]);
                }
            }
            WeekDays = tempCopy;

            return WeekDays;
        }


        //Returns a country Object for a particular id
        public Country GetCountry(int countryId)
        {
            Country country = new Country();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM CountryTable WHERE countryId=@countryId;", con))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@countryId";
                    param.Value = countryId;

                    // 3. add new parameter to command object
                    command.Parameters.Add(param);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            country = new Country(Convert.ToInt32(reader.GetValue(0)), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), Convert.ToDouble(reader.GetValue(5)), Convert.ToDouble(reader.GetValue(6)));
                        }
                    }

                }
                con.Close();

                return country;

            }
        }

    }
}
