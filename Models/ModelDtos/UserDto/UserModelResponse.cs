using System.ComponentModel.DataAnnotations;

namespace itsc_dotnet_practice.Models.ModelDtos.UserDto
{
    public class CreateUserDtoResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
