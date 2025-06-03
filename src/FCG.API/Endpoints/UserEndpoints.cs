using FCG.Domain.Common.Response;
using FCG.Domain.User;
using FCG.Domain.Game;
using FCG.Infrastructure.Cache;
using MessagePack;

namespace FCG.API.Endpoints;

public static class UserEndpoints
{
    private const string USER_LIST_KEY = "UserList";
    
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var userGroup = app
            .MapGroup("/user")
            .WithOpenApi();

        userGroup.MapGet("/", GetAllUsers).CacheOutput(policy =>
        {
            policy.SetVaryByRouteValue("AllUsers");
            policy.Expire(TimeSpan.FromMinutes(10));
        }).RequireAuthorization("Admin");
        userGroup.MapGet("/{id}", GetUser).CacheOutput(policy =>
        {
            policy.SetVaryByRouteValue("User");
            policy.Expire(TimeSpan.FromMinutes(10));
        }).RequireAuthorization();
        userGroup.MapPost("/", CreateUser).RequireAuthorization("Admin");
        userGroup.MapDelete("/{id}", DeleteUser).RequireAuthorization("Admin");
        userGroup.MapPut("/{id}", UpdateUser).RequireAuthorization();
    }

    static async Task<IResult> UpdateUser(Guid id, UserRequest userRequest, IUserService _userService, ICacheService _cacheService)
    {
        _cacheService.Remove(USER_LIST_KEY);
        
        var user = await _userService.UpdateUserAsync(id, userRequest.ToUserDto(), new CancellationToken());
        return user.Data is not null ? TypedResults.NoContent() :
            TypedResults.BadRequest(
            MessagePackSerializer.SerializeToJson(
                new ApiResponse<GameResponse>(null, user.Message)));
    }

    static async Task<IResult> GetUser(Guid id, IUserService _userService)
    {
        var user = await _userService.GetUserByIdAsync(id, new CancellationToken());

        if (user is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(
            MessagePackSerializer.SerializeToJson(
                new ApiResponse<UserResponse>(user.ToUserResponse(), $"User {user} found.")));
    }

    static async Task<IResult> GetAllUsers(IUserService _userService, ICacheService _cacheService)
    {
        if (_cacheService.Get(USER_LIST_KEY) is List<UserResponse> cachedUser)
            return TypedResults.Ok(
                MessagePackSerializer.SerializeToJson(
                    new ApiResponse<List<UserResponse>>(cachedUser, $"Users found.")));

        var userList = await _userService.GetAllUsersAsync(new CancellationToken());
        var userListResponse = userList.Select(user => user.ToUserResponse()).ToList();

        if (userListResponse.Count == 0)
            return TypedResults.NotFound();

        _cacheService.Set(USER_LIST_KEY, userListResponse);

        return TypedResults.Ok(
            MessagePackSerializer.SerializeToJson(
                new ApiResponse<List<UserResponse>>(userListResponse, $"Users found.")));
    }

    static async Task<IResult> CreateUser(UserRequest userRequest, IUserService _userService, ICacheService _cacheService)
    {
        _cacheService.Remove(USER_LIST_KEY);
        
        var user = await _userService.CreateUserAsync(userRequest.ToUserDto(), new CancellationToken());
        return user.Data is not null ? TypedResults.Created($"/user/{user.Data.Id}", user.Data) :
            TypedResults.BadRequest(
            MessagePackSerializer.SerializeToJson(
                new ApiResponse<GameResponse>(null, user.Message)));
    }

    static async Task<IResult> DeleteUser(Guid id, IUserService _userService)
    {
        var success = await _userService.DeleteUserAsync(id, new CancellationToken());
        return success ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}
