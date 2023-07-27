using System;
using Microsoft.AspNetCore.Mvc;
using GrossPremiumCalculator;
using System.Collections;

namespace CountryGwpAPI.Controllers
{
    [ApiController]
    [Route("server/api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        

        private readonly ICountryGwpManager _CountryGwpManager;
        public CountryGwpController(ICountryGwpManager countryGwpManager)
        {
            _CountryGwpManager = countryGwpManager;
        }

        [HttpPost("avg")]
        public IActionResult GetAverageGwp([FromBody] CountryGwpModel data)
        {
            if (data == null || string.IsNullOrEmpty(data.Country) || data.LinesOfBusiness.Count() == 0)
            {
                return BadRequest("Invalid Data Passed....");
            }

            Hashtable result =  _CountryGwpManager.GetAverageGwp(data);
            return Ok(result);          
        }
    }
}
