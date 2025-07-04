// Services/UserService/Interface/IUserService.cs
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.UserDto;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<User?> GetUserByIdAsync(int id);

    Task<CreateUserDtoResponse> CreateUserAsync(CreateUserDtoRequest dto);

    Task<bool> UpdateUserAsync(int id, UserUpdateDtoRequest user);

    Task<bool> DeleteUserAsync(int id);
}
