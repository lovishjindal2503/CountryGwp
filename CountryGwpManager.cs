using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Diagnostics.Metrics;

namespace GrossPremiumCalculator
{
    public class CountryGwpManager : ICountryGwpManager
    {

        private readonly IMemoryCache _cache;

        public CountryGwpManager(IMemoryCache cache)
        {
            _cache = cache;
        }   

        public Hashtable GetAverageGwp(CountryGwpModel data)
        {
            //LogHelper.LogInfo(Log, "GetAverageGwp Method Started for Country =" + data.Country);

            Hashtable response = new Hashtable();

            try
            {
                List<SampleData> lstsampleData = new List<SampleData>();

                // Implented caching

                if (_cache.TryGetValue(data.Country, out Hashtable result))
                {
                    return result;
                }


                // Made the sample data myself, whcih could be the data from CSV that we could store in as lastsampleData objects

                lstsampleData.Add(new SampleData { Country= "ae",   NameOfLOB= "transport", YearPremiumObj = new Dictionary<int, long> { { 2008, 268744928 }, { 2009, 744928 }, { 2010, 268449 }, { 2011, 26928 } } });
                lstsampleData.Add(new SampleData { Country = "ae", NameOfLOB = "freight", YearPremiumObj = new Dictionary<int, long> { { 2008, 2628 }, { 2009, 7448 }, { 2010, 449 }, { 2011, 2698 } } });
                lstsampleData.Add(new SampleData { Country = "ae", NameOfLOB = "property", YearPremiumObj = new Dictionary<int, long> { { 2008, 828 }, { 2009, 744928 }, { 2010, 268449 }, { 2011, 26928 } } });
                lstsampleData.Add(new SampleData { Country = "ae", NameOfLOB = "liability", YearPremiumObj = new Dictionary<int, long> { { 2008, 244928 }, { 2009, 7448 }, { 2010, 8449 }, { 2011, 29255 } } });

                lstsampleData.Add(new SampleData { Country = "ao", NameOfLOB = "transport", YearPremiumObj = new Dictionary<int, long> { { 2008, 268744928 }, { 2009, 744928 }, { 2010, 268449 }, { 2011, 26928 } } });
                lstsampleData.Add(new SampleData { Country = "ao", NameOfLOB = "freight", YearPremiumObj = new Dictionary<int, long> { { 2008, 2628 }, { 2009, 7448 }, { 2010, 449 }, { 2011, 2698 } } });
                lstsampleData.Add(new SampleData { Country = "ao", NameOfLOB = "property", YearPremiumObj = new Dictionary<int, long> { { 2008, 828 }, { 2009, 744928 }, { 2010, 268449 }, { 2011, 26928 } } });
                lstsampleData.Add(new SampleData { Country = "ao", NameOfLOB = "liability", YearPremiumObj = new Dictionary<int, long> { { 2008, 244928 }, { 2009, 7448 }, { 2010, 8449 }, { 2011, 29255 } } });


                string countryReceived = data.Country;

                List<SampleData> lstsampleDatafiltered = lstsampleData.Where(i => i.Country == countryReceived ).ToList<SampleData>();

                // For each LOB received in LIST, checking the LOB in the sampleData & then calculation AVG of the premium of all years for that LOB

                foreach (string lob in data.LinesOfBusiness)
                {
                    decimal ans = 0; 
                    foreach (SampleData item in lstsampleDatafiltered)
                    {
                        if(lob == item.NameOfLOB)
                        {
                            foreach(KeyValuePair<int,long> kvp in item.YearPremiumObj)
                            {
                                ans += kvp.Value;
                            }

                            ans = ans / item.YearPremiumObj.Count;                
                        }
                    }

                    response.Add(lob, ans);         
                }

                // Setting the response in the Cache, whatever response is calculated
                _cache.Set(countryReceived, response);
            }
            catch (Exception ex)
            {
                //LogHelper.Logerror(Log, Some occured in GetAverageGwp Method, ex)   
                throw;
            }
            finally
            {
                //LogHelper.LogInfo(Log, "GetAverageGwp Method Ended for Country =" + data.Country);
            }

            return response;

        }
    }

    public interface ICountryGwpManager
    {
        Hashtable GetAverageGwp(CountryGwpModel data);
    }
}