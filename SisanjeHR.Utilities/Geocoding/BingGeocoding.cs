using Geocoding;
using Geocoding.Microsoft;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Utilities.Geocoding
{
    public class BingGeocoding
    {
        private static string API_KEY = ConfigurationManager.AppSettings["API_KEY"];

        private readonly static IGeocoder geocoder = new BingMapsGeocoder(API_KEY);

        public static async Task<Address> GetGeocodedAddress(string inputAddress)
        {
            IEnumerable<Address> addresses = await geocoder.GeocodeAsync(inputAddress);
            return addresses.First();
        }
    }
}
