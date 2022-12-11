namespace BLL.DTOs.Users;

public class UserDTO
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Fullname { get; set; }
    public string? Avatar { get; set; }

    public UserDTO(Guid id, string? username, string? email, string? fullname, string? avatar)
    {
        Id = id;
        Username = username;
        Email = email;
        Fullname = fullname;
        Avatar = avatar;
    }
}