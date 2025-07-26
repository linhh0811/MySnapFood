using System.Text.Json.Serialization;
namespace Service.SnapFood.Client.Dto.Momo
{
    public class MomoResponse
    {
        [JsonPropertyName("qrCodeUrl")]
        public string QrCodeUrl { get; set; } = string.Empty;

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        public string PayUrl { get; set; } = string.Empty;
    }
}
