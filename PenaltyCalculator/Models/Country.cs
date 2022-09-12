using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models
{
    public class Country
    {
        //Properties

        public int countryId { get; set; }

        public string name { get; set; }

        public string currencyName { get; set; }

        public string currencySymbol { get; set; }

        public string weekend { get; set; }

        public double tax { get; set; }

        public double penaltyPerDay { get; set; }

        //Constructors

        public Country(int Id, string countryName, string curName, string curSymbol, string week, double taxAmount, double penalty)
        {
            countryId = Id;
            name = countryName;
            currencyName = curName;
            currencySymbol = curSymbol;
            weekend = week;
            tax = taxAmount;
            penaltyPerDay = penalty;
        }

        public Country()
        {
            //Default constructor
        }




    }
}
