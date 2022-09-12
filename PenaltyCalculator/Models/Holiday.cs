using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models
{
    public class Holiday
    {
        //Properties
        public int holidayId { get; set; }

        public string name { get; set; }

        public DateTime date { get; set; }

        public int countryId { get; set; }

        //Constructors

        public Holiday()
        {

        }

        public Holiday(int id, string holidayName, DateTime holidayDate, int country)
        {
            holidayId = id;
            name = holidayName;
            date = holidayDate;
            countryId = country;

        }
    }
}
