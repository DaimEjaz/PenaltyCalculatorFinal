using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PenaltyCalculator.BusinessLayer;
using PenaltyCalculator.Models;

namespace PenaltyCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenaltyController : ControllerBase
    {
        ICalculatePenalty _penaltyCalculator;

        //Dependency injection
        public PenaltyController(ICalculatePenalty penaltyCalculator)
        {
            _penaltyCalculator = penaltyCalculator;
        }


        [Route("GetAllCountries")]
        [HttpGet]

        //API for getting the list of all countries
        public List<Country> GetCountries()
        {
            List<Country> countries = _penaltyCalculator.GetCountries();
            return countries;
        }

        [Route("CalculateAmount")]
        [HttpPost]

        //API for returning the penalty amount & business days
        public Penalty CalculateAmount([FromBody] Query obj)
        {
            DateTime first = obj.checkoutDate;
            DateTime second = obj.returnedDate;
            int id = obj.countryId;
            
            return _penaltyCalculator.CalculateAmount(first, second, id);
        }
    }
}