using Core.Users.Responses;
using Domain.Common;

namespace Core;

public partial class Errors
{
   public record UserAlreadyExistError(string email) : Error<UserResultResponse>("User.Register", $"user with email {email} already registered", ResultErrorStatus.BadRequest);
   public record UserFailedLoginError(string email) : Error<UserResultResponse>("User.Login", $"check email {email} or password", ResultErrorStatus.BadRequest);
}