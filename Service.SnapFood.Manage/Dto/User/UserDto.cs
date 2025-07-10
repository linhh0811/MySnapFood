namespace Service.SnapFood.Manage.Dto.User
{
    public class UserDto
    {
        public int Index { get; set; }
        public Guid Id { get; set; }
        public bool IsInRole { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Numberphone { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string LastModifiedByName { get; set; } = string.Empty;
    }
}