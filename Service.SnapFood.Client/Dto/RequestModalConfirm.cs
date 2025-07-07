namespace Service.SnapFood.Client.Dto
{
    public class RequestModalConfirm
    {
        public string Title { get; set; } = "Xác nhận xoá";
        public string Content { get; set; } = "Bạn có chắc chắn muốn xoá vĩnh viễn bản ghi này không ? ";
        public string Message { get; set; } = " Hành động này không thể hoàn tác.";
    }
}
