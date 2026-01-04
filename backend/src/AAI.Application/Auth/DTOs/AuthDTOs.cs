using System.ComponentModel.DataAnnotations;

namespace AAI.Application.Auth.DTOs;

/// <summary>
/// DTO for user login request
/// </summary>
public class LoginRequestDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// DTO for password setup/change request
/// </summary>
public class SetupPasswordRequestDTO
{
    [Required(ErrorMessage = "Current password is required")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "New password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)", 
        ErrorMessage = "Password must contain uppercase, lowercase and digits")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

/// <summary>
/// DTO for password refresh token request
/// </summary>
public class RefreshTokenRequestDTO
{
    [Required(ErrorMessage = "Refresh token is required")]
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
/// DTO for authentication response with tokens
/// </summary>
public class AuthTokenResponseDTO
{
    [Required]
    public string AccessToken { get; set; } = string.Empty;

    [Required]
    public string RefreshToken { get; set; } = string.Empty;

    public int ExpiresIn { get; set; }

    public string TokenType { get; set; } = "Bearer";
}

/// <summary>
/// DTO for authenticated user information
/// </summary>
public class AuthUserDTO
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Name { get; set; }

    public string? RiskProfile { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool HasPassword { get; set; }
}

/// <summary>
/// DTO for complete login response with user and tokens
/// </summary>
public class LoginResponseDTO
{
    [Required]
    public AuthUserDTO User { get; set; } = new();

    [Required]
    public AuthTokenResponseDTO Tokens { get; set; } = new();
}

/// <summary>
/// DTO for PIN setup request
/// </summary>
public class PinSetupRequestDTO
{
    [Required(ErrorMessage = "PIN is required")]
    [RegularExpression(@"^\d{4,6}$", ErrorMessage = "PIN must be 4-6 digits")]
    public string Pin { get; set; } = string.Empty;

    [Required(ErrorMessage = "PIN confirmation is required")]
    [Compare("Pin", ErrorMessage = "PINs do not match")]
    public string ConfirmPin { get; set; } = string.Empty;
}

/// <summary>
/// DTO for PIN verification request
/// </summary>
public class PinVerificationRequestDTO
{
    [Required(ErrorMessage = "PIN is required")]
    [RegularExpression(@"^\d{4,6}$", ErrorMessage = "PIN must be 4-6 digits")]
    public string Pin { get; set; } = string.Empty;
}
