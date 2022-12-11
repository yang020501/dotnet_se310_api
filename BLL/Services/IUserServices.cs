using BLL.DTOs.Users;
using DAL.Aggregates;
using System.Security.Claims;

namespace BLL.Services;

public interface IUserServices
{
    Guid Register(RegisterRequest request);

    IEnumerable<User>? GetAllByRole(string? role);

    AuthenticationResponse Authenticate(LoginRequest loginRequest);

    User? GetUserByUsername(string? username);

    User? GetUserById(string? id);

    UserDTO? UpdateUserInfo(UpdateInfoRequest request);

    User? ChangePassword(ChangePasswordRequest request);

    Guid DeleteUserById(Guid? id);
}