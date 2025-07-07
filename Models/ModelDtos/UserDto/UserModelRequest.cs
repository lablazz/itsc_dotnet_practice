using System.ComponentModel.DataAnnotations;

namespace itsc_dotnet_practice.Models.ModelDtos.UserDto
{
    public class CreateUserDtoRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = "";

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = "";

        public string Phone { get; set; } = "";
    }

    public class UserUpdateDtoRequest
    {
        [Required]
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        public string? NewPassword { get; set; }
        public string Phone { get; set; } = "";
    }
}