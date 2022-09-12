using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PenaltyCalculator.Models;

namespace PenaltyCalculator.BusinessLayer
{
    public interface ICalculatePenalty
    {
        //Interface with method signatures
        Penalty CalculateAmount(DateTime checkoutDate, DateTime returnedDate, int countryId);

        List<Country> GetCountries();
    }
}