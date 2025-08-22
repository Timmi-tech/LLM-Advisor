using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.Json;
using Domain.Entities.Models;

namespace Application.DTOs
{
    // This DTO is used for user registration
    public record UserForRegistrationDto
    {
        public string Firstname { get; init; } = string.Empty;
        public string Lastname { get; init; } = string.Empty;
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
    // This DTO is used for user authentication
    public record UserForAuthenticationDto
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;

    }

    public record TokenDto(string AccessToken, string RefreshToken);

} 