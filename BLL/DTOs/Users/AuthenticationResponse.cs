namespace BLL.DTOs.Users;

public class AuthenticationResponse
{
    public string? Token { get; set; }
    public string? Role { get; set; }
}