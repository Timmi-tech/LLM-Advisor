using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs;
using Application.Services.Contracts;
using Domain.Entities.ConfigurationsModels;
using Domain.Entities.Contracts;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<JwtConfiguration> _configuration;
        private readonly JwtConfiguration _jwtConfiguration;

        public AuthenticationService(ILoggerManager logger, UserManager<User> userManager, IOptions<JwtConfiguration> configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
            _jwtConfiguration = _configuration.Value;
            
        }
        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto
        userForRegistration)
        {
            User user = new()
            {
                FirstName = userForRegistration.Firstname,
                LastName = userForRegistration.Lastname,
                UserName = userForRegistration.Username,
                Email = userForRegistration.Email,
            };
            IdentityResult result = await _userManager.CreateAsync(user, userForRegistration.Password);
            return result;
        }
        public async Task<User> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            User? user = await _userManager.FindByEmailAsync(userForAuth.Email!);
            if (user == null)
            {
                _logger.LogWarn($"Authentication failed. No user found with email: {userForAuth.Email}");
                return null!;
            }
            if (!await _userManager.CheckPasswordAsync(user, userForAuth.Password!))
            {
                _logger.LogWarn($"Authentication failed. Invalid password for user: {userForAuth.Email}");
                return null!;
            }
            return user;
        }
        public async Task<TokenDto> CreateToken(User user,bool populateExp)
        {
            SigningCredentials signingCredentials = GetSigningCredentials();
            List<Claim> claims = await GetClaims(user);
            JwtSecurityToken tokenOptions = GenerateTokenOptions(signingCredentials, claims);


            string refreshToken = GenerateRefreshToken();

            user.RefreshToken = HashRefreshToken(refreshToken);

            if (populateExp)
            {
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            }

            await _userManager.UpdateAsync(user);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto(accessToken, refreshToken);
        }
        private SigningCredentials GetSigningCredentials()
        {
            byte[] key = Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!);
            SymmetricSecurityKey secret = new(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private static Task<List<Claim>> GetClaims(User user)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!),
            ];
            return Task.FromResult(claims);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) 
        { 
            JwtSecurityToken  tokenOptions = new( 
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                signingCredentials: signingCredentials
            ); 
                return tokenOptions;
            }
        private static string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private static string HashRefreshToken(string refreshToken)
        {
            byte[] tokenBytes = Encoding.UTF8.GetBytes(refreshToken);
            byte[] hashBytes = SHA256.HashData(tokenBytes);
            return Convert.ToBase64String(hashBytes);
        }
        // Add the GetPrincipalFromExpiredToken method to the AuthenticationService class.
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!)),
            ValidateLifetime = false,
            ValidIssuer = _jwtConfiguration.ValidIssuer,
            ValidAudience = _jwtConfiguration.ValidAudience,
            ClockSkew = TimeSpan.FromMinutes(5) 
        };

        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token Format");
            }

            return principal;   
        }
        
        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            string? username = principal.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new SecurityTokenException("Invalid token - username not found");

            User? user = await _userManager.FindByNameAsync(username) ?? throw new SecurityTokenException("Invalid token - user not found");
            string refreshTokenHash = HashRefreshToken(tokenDto.RefreshToken);
            if (user.RefreshToken != refreshTokenHash)
            {
                _logger.LogWarn($"Invalid refresh token attempt for user: {username}");
                throw new SecurityTokenException("Invalid refresh token");
            }
            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                _logger.LogWarn($"Expired refresh token attempt for user: {username}");
                throw new SecurityTokenException("Refresh token expired");
            }

            return await CreateToken(user, populateExp: true);
        }
    }
}