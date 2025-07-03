using Newtonsoft.Json;

namespace Service.SnapFood.Client.Infrastructure.Service
{
    public interface IAddressService
    {
        Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string address);
    }
        public class AddressService : IAddressService
        {

            private readonly string _apiKey = "GTzwweyhgu0GBvSH0XJjPkPDwYeFkVV_ok80Oyas_qA";
            private readonly HttpClient _httpClient;
            public AddressService(HttpClient httpClient)
            {

                _httpClient = httpClient;

            }

        public async Task<(double Latitude, double Longitude)> GetCoordinatesAsync(string address)
        {
            string url = $"https://geocode.search.hereapi.com/v1/geocode?q={Uri.EscapeDataString(address)}&apiKey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return (0.0, 0.0);

            var result = await response.Content.ReadAsStringAsync();

            var coordinates = JsonConvert.DeserializeObject<dynamic>(result);
            if (coordinates.items != null && coordinates.items.Count > 0)
            {
                double latitude = coordinates.items[0].position.lat;
                double longitude = coordinates.items[0].position.lng;
                return (latitude, longitude);
            }

            return (0.0, 0.0);
        }
    }

}

