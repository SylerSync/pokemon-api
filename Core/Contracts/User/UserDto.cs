namespace Core.Contracts.User
{
    public class UserDto
    {
        public string? Email { get; set; }
        public List<string>? WishList { get; set; } = new List<string>();
    }
}
