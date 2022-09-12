using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models
{
    public class Query
    {
        //Properties
        public DateTime checkoutDate { get; set; }
        public DateTime returnedDate { get; set; }
        public int countryId { get; set; }

        //Constructors

        public Query(DateTime first, DateTime end, int id)
        {
            checkoutDate = first;
            returnedDate = end;
            countryId = id;
        }

        public Query()
        {

        }
    }
}
