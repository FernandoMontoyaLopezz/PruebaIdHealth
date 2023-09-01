using Amazon.SecurityToken.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PruebaIdHealth.Entities;
using PruebaIdHealth.Repositories.Interfaces;
using PruebaIdHealth.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PruebaIdHealth.Services;

public class AuthService : IAuthService
{

    private readonly IAuthRepository _authRepo;
    private readonly IOptions<JwtSettings> _jwtSettings;

    public AuthService(IAuthRepository authRepo, IOptions<JwtSettings> jwtSettings)
    {
        _authRepo = authRepo;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> LoginAsync(Credential credentials)
    {
        if (credentials is not null)
        {
            credentials.Password = EncryptPassword(credentials.Password, credentials.Username);
            User user = await _authRepo.Login(credentials);
            if (user is not null)
            {
                return GenerateJSONWebToken(user);
            }
            else
            {
                throw new KeyNotFoundException("User not found");
            }
        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
    }
    public async Task RegisterAsync(User user)
    {
        if (user is not null)
        {
            user.Password = EncryptPassword(user.Password, user.Username);
            await _authRepo.Register(user);
        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
        return;
    }

    public async Task<List<User>> GetAsync()
    {
        return await _authRepo.Get();
    }

    public async Task UpdateAsync(string id, User user)
    {
        if (user is not null)
        {
            await _authRepo.Update(id, user);

        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
        return;
    }
    public async Task DeleteAsync(string id)
    {
        await _authRepo.Delete(id);
        return;
    }

    private string GenerateJSONWebToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Email,user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var token = new JwtSecurityToken(
            _jwtSettings.Value.Issuer,
            _jwtSettings.Value.Issuer,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string EncryptPassword(string password, string user)
    {
        using (var sha256 = SHA256.Create())
        {
            var saltedPassword = string.Format("{0}{1}", user, password);
            byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
            return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
        }
    }
}