using BLL.DTOs.Users;
using DAL.Aggregates;
using System.Security.Claims;

namespace BLL.Services;

public interface IUserServices
{
    public Guid Register(RegisterRequest request);

    public IEnumerable<User>? GetAllByRole(string? role);

    public AuthenticationResponse Authenticate(LoginRequest loginRequest);

    public User? GetUserById(string? id);

    public User? UpdateUserInfo(UpdateInfoRequest request);

    public User? ChangePassword(ChangePasswordRequest request);

    public Guid DeleteUserById(Guid? id);

    public IEnumerable<User>? GetAllUser();
}