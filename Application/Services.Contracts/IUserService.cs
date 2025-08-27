using Application.DTOs;

namespace Application.Services.Contracts
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetUserProfileAsync(string userId);
        Task<UserProfileDto> UpdateUserProfileAsync(string userId, UpdateUserProfileDto updateDto);
        Task ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
        Task<int> GetTotalUsersCountAsync();
    }
}