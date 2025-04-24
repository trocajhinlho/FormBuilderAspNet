using System.ComponentModel.DataAnnotations;

namespace FormBuilder.API.Models.Dto.AuthDtos;

public record class LoginDto
{
    [Required(ErrorMessage = "Username is Required")]
    [MaxLength(40, ErrorMessage = "Username cannot exceed 40 characters")]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "Passoword is Required")]
    [MaxLength(40, ErrorMessage = "Password cannot exceed 40 characters")]
    public string Password { get; init; } = string.Empty;
}
