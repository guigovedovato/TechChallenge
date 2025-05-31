using FCG.Domain.Common.Response;

namespace FCG.Domain.User;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<ResponseDto<UserDto>> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken);
    Task<bool> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<ResponseDto<UserDto>> UpdateUserAsync(Guid userId, UserDto userDto, CancellationToken cancellationToken);
}
