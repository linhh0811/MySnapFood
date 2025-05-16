using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;

namespace Service.SnapFood.Manage.Infrastructure.Services
{
    public class CallServiceRegistry : ICallServiceRegistry
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CallServiceRegistry(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;


        }

        // DELETE method
        public async Task<ResultAPI> Delete(ApiRequestModel apiRequestModel)
        {

            //await HandleTokenFromSession(apiRequestModel);

            string endpoint = GetFullEndPoint(apiRequestModel);
            HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint);

            return await HandleResponse(response);
        }

        private string GetFullEndPoint(ApiRequestModel apiRequestModel)
        {
            /// <summary>
            /// Lấy endpoint đầy đủ bao gồm base URL + endpoint + query params (nếu có).
            /// </summary>
            /// <returns>URL đầy đủ</returns>
            /// <exception cref="KeyNotFoundException">Nếu service không tồn tại trong cấu hình</exception>
            if (_configuration == null)
                throw new InvalidOperationException("Configuration is not provided.");

            // Lấy base URL của service từ cấu hình
            string serviceBaseUrl = _configuration[$"ServicesRegistry:{apiRequestModel.ApiService}"]
                ?? throw new KeyNotFoundException($"ServicesRegistry {apiRequestModel.ApiService} not found in configuration.");

            // Ghép URL
            string fullUrl = $"{serviceBaseUrl}/api/v{apiRequestModel.Version}{apiRequestModel.Endpoint}";

            // Nếu có query params, thêm vào URL
            if (apiRequestModel.QueryParams != null && apiRequestModel.QueryParams.Any())
            {
                string queryString = string.Join("&", apiRequestModel.QueryParams.Select(q => $"{q.Key}={Uri.EscapeDataString(q.Value)}"));
                fullUrl += "?" + queryString;
            }

            return fullUrl;
        }


        // PUT method
        public async Task<ResultAPI> Put(ApiRequestModel apiRequestModel, object? data = null)
        {
            //await HandleTokenFromSession(apiRequestModel);

            string endpoint = GetFullEndPoint(apiRequestModel);
            HttpResponseMessage response = data == null
                ? await _httpClient.PutAsync(endpoint, null)
                : await _httpClient.PutAsJsonAsync(endpoint, data);

            return await HandleResponse(response);
        }

        // GET method
        public async Task<ResultAPI> Get<T>(ApiRequestModel apiRequestModel)
        {
            //await HandleTokenFromSession(apiRequestModel);

            string endpoint = GetFullEndPoint(apiRequestModel);
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            return await HandleResponse<T>(response);
        }

        // POST method
        public async Task<ResultAPI> Post<T>(ApiRequestModel apiRequestModel, object data)
        {

            //await HandleTokenFromSession(apiRequestModel);

            string endpoint = GetFullEndPoint(apiRequestModel);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, data);

            return await HandleResponse<T>(response);
        }



        /// <summary>
        /// Xử lý phản hồi HTTP chung
        /// </summary>
        private async Task<ResultAPI> HandleResponse(HttpResponseMessage response)
        {
            return await HandleResponse<object>(response);
        }

        /// <summary>
        /// Xử lý phản hồi HTTP với dữ liệu đầu ra kiểu T
        /// </summary>
        private async Task<ResultAPI> HandleResponse<T>(HttpResponseMessage response)
        {
            ResultAPI resultAPI = new ResultAPI(StatusCode.Forbidden);
            string content = await response.Content.ReadAsStringAsync();

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (response.IsSuccessStatusCode)
                {
                    resultAPI.Message = "Thao tác thành công.";
                    resultAPI.Status = StatusCode.OK;
                    resultAPI.Data = string.IsNullOrWhiteSpace(content) ? default : JsonSerializer.Deserialize<T>(content, options);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    resultAPI.Message = "Hết phiên đăng nhập.";
                    resultAPI.Status = StatusCode.Forbidden;
                }
                else
                {
                    ResponseErrorAPI? error = JsonSerializer.Deserialize<ResponseErrorAPI>(content, options);
                    string? detailError = error?.Errors?.Id != null ? string.Join(", ", error.Errors.Id) : "Lỗi không xác định.";

                    resultAPI.Message = $"Lỗi: {detailError}";
                    resultAPI.Status = StatusCode.InternalServerError;
                    resultAPI.Data = error;
                }
            }
            catch (Exception ex)
            {
                resultAPI.Message = $"Đã có lỗi xảy ra: {ex.Message}";
                resultAPI.Status = StatusCode.InternalServerError;
            }

            return resultAPI;
        }



        /// <summary>
        /// Xử lý token từ session và thiết lập vào HttpClient
        /// </summary>
        //private async Task HandleTokenFromSession(ApiRequestModel apiRequestModel)
        //{
        //    if (apiRequestModel == null)
        //        throw new ArgumentNullException(nameof(apiRequestModel));

        //    if (string.IsNullOrEmpty(apiRequestModel.Token))
        //    {
        //        var currtentUser = await _userService.GetCurrentUserAsync();

        //        if (currtentUser == null || string.IsNullOrWhiteSpace(currtentUser.Token))
        //            throw new UnauthorizedAccessException("Không lấy được token từ đầu vào.");

        //        apiRequestModel.Token = currtentUser.Token;
        //    }

        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequestModel.Token);
        //}
    }
}
