using CountryGwpAPI.Controllers;
using GrossPremiumCalculator;



namespace CountryGwpAPI.Tests
{
    public class CountryGwpControllerTests
    {

        private CountryGwpController _countryGwpController = null;

        public void GetAverageGwp_ValidRequest()
        {
            var request = new CountryGwpModel
            {
                Country = "ae",
                LinesOfBusiness = new List<string> { "transport", "liability" }
            };

            var result =  _countryGwpController.GetAverageGwp(request);
            Console.WriteLine(result);
        }
    }
}
