using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.UserDto;

namespace itsc_dotnet_practice.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<CreateUserDtoResponse>> GetAllUsersAsync();
        Task<CreateUserDtoResponse?> GetUserByIdAsync(int id);
        Task<CreateUserDtoResponse> CreateUserAsync(CreateUserDtoRequest dto);
        Task<bool> UpdateUserAsync(int id, UserUpdateDtoRequest user);
        Task<bool> DeleteUserAsync(int id);
    }
}
