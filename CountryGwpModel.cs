using System.Collections;

namespace GrossPremiumCalculator
{
    public class CountryGwpModel
    {
        public string Country { get; set; }
        public List<string> LinesOfBusiness { get; set; }
    }

    public class SampleData
    {
        public string Country { get; set; }
        public string NameOfLOB { get; set; }

        public Dictionary< int, long> YearPremiumObj { get; set; }
    }

}