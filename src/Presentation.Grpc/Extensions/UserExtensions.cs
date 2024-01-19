using Core.Users.Commands.Login;
using Core.Users.Commands.Register;
using Core.Users.Responses;
using Domain.Common;
using ProtoContracts;

namespace Grpc.Extensions;

public static class UserExtensions
{
    public static LoginCommand ToLoginCommand(this IdentityRequest request)
        => new(new(request.Email, request.Password));

    public static RegisterCommand ToRegisterCommand(this IdentityRequest request)
        => new(new(request.Email, request.Password));

    public static AuthenticationResponse ToResponse(this Result<UserResultResponse> result)
    {
        var reply = new AuthenticationResponse();
        if (result.IsSuccess)
        {
            reply.IsSucces = true;
            reply.User = new UserResponse()
            {                 
                Email = result.Value!.Email,
                Token = result.Value!.Token
            };
        }
        else
        {
            reply.Error = result.Error.ToErrorModel();
        }
        return reply;
    }


}
