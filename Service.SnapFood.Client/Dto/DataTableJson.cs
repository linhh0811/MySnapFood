using System.Text.Json;

namespace Service.SnapFood.Client.Dto
{
    public class DataTableJson
    {
        public int? Draw { get; set; }
        public int? RecordsTotal { get; set; }
        public int? RecordsFiltered { get; set; }
        public JsonElement Data { get; set; }
        public string? ExMessage { get; set; }
        public string? Querytext { get; set; }
    }
}
