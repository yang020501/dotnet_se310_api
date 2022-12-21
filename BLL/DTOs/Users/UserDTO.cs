namespace BLL.DTOs.Users;

public class UserDTO
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Role { get; set; }
    public string? Avatar { get; set; }

}