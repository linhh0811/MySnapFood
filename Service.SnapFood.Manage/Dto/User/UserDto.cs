namespace Service.SnapFood.Manage.Dto.User
{
    public class UserDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}