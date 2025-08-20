using Service.SnapFood.Share.Interface.Extentions;
using Service.SnapFood.Share.Model.ServiceCustomHttpClient;
using System.Text.Json;

namespace Service.SnapFood.Client.Infrastructure.Service
{
    public class CallServiceRegistry : ICallServiceRegistry
    {
        private readonly HttpClient _httpClient;

        public CallServiceRegistry(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;


        }

        // DELETE method
        public async Task<ResultAPI> Delete(ApiRequestModel apiRequestModel)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(apiRequestModel.Endpoint);

            return await HandleResponse(response);
        }



        // PUT method
        public async Task<ResultAPI> Put(ApiRequestModel apiRequestModel, object? data = null)
        {


            HttpResponseMessage response = data == null
                ? await _httpClient.PutAsync(apiRequestModel.Endpoint, null)
                : await _httpClient.PutAsJsonAsync(apiRequestModel.Endpoint, data);

            return await HandleResponse(response);
        }

        // GET method
        public async Task<ResultAPI> Get<T>(ApiRequestModel apiRequestModel)
        {

            HttpResponseMessage response = await _httpClient.GetAsync(apiRequestModel.Endpoint);

            return await HandleResponse<T>(response);
        }

        // POST method
        public async Task<ResultAPI> Post<T>(ApiRequestModel apiRequestModel, object data)
        {


            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiRequestModel.Endpoint, data);

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
                else
                {
                    ResponseErrorAPI? error = JsonSerializer.Deserialize<ResponseErrorAPI>(content, options);
                    string? detailError = error?.Errors?.Id != null ? string.Join(" ", error.Errors.Id) : "Lỗi không xác định.";

                    resultAPI.Message = $"{detailError}";
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

        public Task SetAuthorizeHeader()
        {
            throw new NotImplementedException();
        }
    }
}
