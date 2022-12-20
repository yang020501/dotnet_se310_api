using BLL.DTOs.Users;
using DAL.Aggregates;
using System.Security.Claims;

namespace BLL.Services;

public interface IUserServices
{
    public Guid Register(RegisterRequest request);

    public IEnumerable<UserDTO>? GetAllByRole(string? role);

    public AuthenticationResponse Authenticate(LoginRequest loginRequest);

    public UserDTO? GetUserById(string? id);

    public UserDTO? UpdateUserInfo(UpdateInfoRequest request);

    public UserDTO? ChangePassword(ChangePasswordRequest request);

    public Guid DeleteUserById(Guid? id);

    public IEnumerable<UserDTO>? GetAllUser();
}