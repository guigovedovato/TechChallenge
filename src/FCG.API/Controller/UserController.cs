using FCG.Domain.Common.Response;
using FCG.Domain.Profile;
using FCG.Domain.Store;
using FCG.Infrastructure.Cache;
using MessagePack;

namespace FCG.API.Controller;

public static class UserController
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var userGroup = app
            .MapGroup("/user");

        userGroup.MapGet("/", GetAllUsers).CacheOutput(policy =>
        {
            policy.SetVaryByRouteValue("allUsers");
            policy.Expire(TimeSpan.FromMinutes(10));
        }).RequireAuthorization("Admin");
        userGroup.MapGet("/{id}", GetUser).CacheOutput(policy =>
        {
            policy.SetVaryByRouteValue("user");
            policy.Expire(TimeSpan.FromMinutes(10));
        }).RequireAuthorization();
        userGroup.MapPost("/", CreateUser);
        userGroup.MapDelete("/{id}", DeleteUser).RequireAuthorization();
        userGroup.MapPut("/{id}", UpdateUser).RequireAuthorization();
    }

    static async Task<IResult> UpdateUser(Guid id, UserRequest userRequest, IUserService _userService)
    {
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
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(
            MessagePackSerializer.SerializeToJson(
                new ApiResponse<UserResponse>(user.ToUserResponse(), $"User {user} found.")));
    }

    static async Task<IResult> GetAllUsers(IUserService _userService, ICacheService _cacheService)
    {
        var key = "UserList";

        if (_cacheService.Get(key) is List<UserResponse> cachedUser)
        {
            return TypedResults.Ok(
                MessagePackSerializer.SerializeToJson(
                    new ApiResponse<List<UserResponse>>(cachedUser, $"Users found.")));
        }

        var userList = await _userService.GetAllUsersAsync(new CancellationToken());
        var userListResponse = userList.Select(user => user.ToUserResponse()).ToList();

        _cacheService.Set(key, userListResponse);

        return TypedResults.Ok(
            MessagePackSerializer.SerializeToJson(
                new ApiResponse<List<UserResponse>>(userListResponse, $"Users found.")));
    }

    static async Task<IResult> CreateUser(UserRequest userRequest, IUserService _userService)
    {
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
