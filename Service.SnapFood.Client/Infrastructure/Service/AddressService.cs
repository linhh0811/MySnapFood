using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Service.SnapFood.Client.Infrastructure.Service.AddressService;

namespace Service.SnapFood.Client.Infrastructure.Service
{
    public interface IAddressService
    {
        Task<double?> CalculateDistanceKmAsync(DistanceRequest request);
    }
    public class AddressService : IAddressService
    {

        private readonly string _apiKey = "GTzwweyhgu0GBvSH0XJjPkPDwYeFkVV_ok80Oyas_qA";
        private readonly HttpClient _httpClient;
        public AddressService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }
        public async Task<double?> CalculateDistanceKmAsync(DistanceRequest request)
        {
            string url = $"https://router.hereapi.com/v8/routes" +
                         $"?origin={request.OriginLatitude},{request.OriginLongitude}" +
                         $"&destination={request.DestinationLatitude},{request.DestinationLongitude}" +
                         $"&transportMode=car&return=summary&apikey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            var json = JObject.Parse(content);
            try
            {
                var lengthMeters = (double)json["routes"][0]["sections"][0]["summary"]["length"];
                return lengthMeters / 1000.0;
            }
            catch
            {
                return null;
            }
        }
        public class DistanceRequest
        {
            public double OriginLatitude { get; set; }       // tọa độ quán
            public double OriginLongitude { get; set; }

            public double DestinationLatitude { get; set; }  // tọa độ người nhận
            public double DestinationLongitude { get; set; }
        }
    }

}

