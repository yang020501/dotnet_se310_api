using AutoMapper;
using BLL.Common;
using BLL.DTOs.Users;
using BLL.Exceptions;
using DAL.Aggregates;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services;

public class UserService : IUserServices
{
    private readonly ISharedRepositories _sharedRepositories;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IGenericRepository<User> _userRepository;

    public UserService(ISharedRepositories sharedRepositories, IMapper mapper, IConfiguration configuration)
    {
        _sharedRepositories = sharedRepositories;
        _mapper = mapper;
        _configuration = configuration;
        _userRepository = _sharedRepositories.RepositoriesManager.UserRepository;
    }

    private static bool CheckRole(string role)
    {
        return !(role != "admin" && role  != "mod" && role != "lecturer" && role != "student");

    }

    public Guid Register(RegisterRequest request)
    {
        var foundUser = _userRepository.Get(user => user.Username == request.Username, null, 1);
        if (foundUser.Any())
        {
            throw new InvalidOperationException($"Username {request.Username} has existed in the current context");
        }

        User newUser = _mapper.Map<User>(request);
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        if (newUser.Role is null || !CheckRole(newUser.Role)) 
            throw new NotAllowedException();
        

        _userRepository.Insert(newUser);
        _sharedRepositories.RepositoriesManager.Saves();
        return newUser.Id;
    }
    public AuthenticationResponse Authenticate(LoginRequest loginRequest)
    {
        if (loginRequest.Username is null || loginRequest.RawPassword is null) throw new UnauthorizedException();
        User? user = GetUserByUsername(loginRequest.Username);

        if (user is null) throw new UnauthorizedException();

        

        if(user.Password is not null && !Verify(loginRequest.RawPassword,user.Password))
        {
            throw new UnauthorizedException();
        }

        if(user.Role is null || !CheckRole(user.Role))
        {
            throw new UnauthorizedException();
        }

        string? accessToken = GenerateJWT(user);

        return new AuthenticationResponse
        {
            Token = accessToken,
            Role = user.Role
        };
    }

    private string? GenerateJWT(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);
        if (user.Username is null || user.Role is null) return null;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWT:Expires"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public User? GetUserByUsername(string? username)
    {
        if (!_userRepository.Get(user => user.Username == username, null, 1).Any()) return null;
        return _userRepository.Get(user => user.Username == username, null, 1).First();
    }
    
    private static bool Verify(string rawPasword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(rawPasword, hashedPassword);
    }

    public IEnumerable<User>? GetAllByRole(string? role)
    {
        IEnumerable<User> students = _userRepository.GetAll().Where(p => p.Role == role);
        return students;
    }

    public User? GetUserById(string? id)
    {
        try
        {
            if (!_userRepository.Get(user => user.Id.ToString() == id).Any())
            {
                throw new ResourceNotFoundException("UserId is invalid");
            }
            return _userRepository.Get(user => user.Id.ToString() == id).FirstOrDefault();
        }
        catch(Exception e)
        {
            throw new ResourceConflictException(e.Message);
        }
    }

    public UserDTO? UpdateUserInfo(UpdateInfoRequest request)
    {
        try
        {
            if (!_userRepository.Get(user => user.Username == request.Username).Any())
            {
                throw new ResourceNotFoundException("UserName is invalid");
            }
            var updated_user = _userRepository.Get(user => user.Username == request.Username).FirstOrDefault();
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            updated_user.FullName = request.Fullname;
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            updated_user.Avatar = request.Avatar;
            updated_user.Email = request.Email;

            _userRepository.Update(updated_user);
            _sharedRepositories.RepositoriesManager.Saves();

            return new UserDTO(updated_user.Id, updated_user.Username, updated_user.Email, updated_user.FullName, updated_user.Avatar);
        }
        catch (Exception e)
        {
            throw new ResourceConflictException(e.Message);
        }
    }

    public User? ChangePassword(ChangePasswordRequest request)
    {
        try
        {
            if (!_userRepository.Get(user => user.Username == request.Username).Any())
            {
                throw new ResourceNotFoundException("UserName is invalid");
            }

            var updated_user = _userRepository.Get(user => user.Username == request.Username).FirstOrDefault();

            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (!Verify(request.OldPassword, updated_user.Password))
            {
                throw new ResourceConflictException("Wrong password");
            }
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            #pragma warning restore CS8604 // Possible null reference argument.

            updated_user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            _userRepository.Update(updated_user);
            _sharedRepositories.RepositoriesManager.Saves();

            return updated_user;
        }
        catch (Exception e)
        {
            throw new ResourceConflictException(e.Message);
        }
    }

    public Guid DeleteUserById(Guid? id)
    {
        try
        {
            if (!_userRepository.Get(user => user.Id == id).Any())
            {
                throw new ResourceNotFoundException("UserName is invalid");
            }

            User user = _userRepository.Get(user => user.Id == id).FirstOrDefault();
            _userRepository.Delete(id);
            _sharedRepositories.RepositoriesManager.Saves();
            return user.Id;
        }
        catch (Exception)
        {
            throw new ResourceConflictException();
        }
    }
}