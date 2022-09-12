using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenaltyCalculator.Models
{
    public class Penalty
    {
        //Properties
        public int businessDays { get; set; }

        public string totalPenalty { get; set; }

        //Constructors
        public Penalty()
        {

        }

        public Penalty(int totalBusinessDays, string penaltyAmount)
        {
            businessDays = totalBusinessDays;
            totalPenalty = penaltyAmount;
        }
    }
}