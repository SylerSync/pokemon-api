namespace Core.Domain.DataObjects
{
    public class User
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public List<string>? WishList { get; set; }
    }
}
