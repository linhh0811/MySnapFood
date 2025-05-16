using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Model.ServiceCustomHttpClient
{
    public class ApiRequestModel
    {

        public ServicesRegistryEnum ApiService { get; set; }
        public float Version { get; set; }
        public string Endpoint { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public Dictionary<string, string>? QueryParams { get; set; } = null;

        public ApiRequestModel()
        {
            Version = 1.0f;
        }
        public ApiRequestModel(string endpoint)
        {
            Endpoint = endpoint;
            Version = 1.0f;
        }
    }
}
