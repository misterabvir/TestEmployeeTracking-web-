using Core.Users.Responses;
using Domain.Common;
using Grpc.Core;
using Grpc.Extensions;
using MediatR;
using ProtoContracts;

namespace Grpc.Services;

public class IdentityService : IdentityProto.IdentityProtoBase
{
    private readonly ISender _sender;

    public IdentityService(ISender sender)
    {
        _sender = sender;
    }

  
    public override async Task<AuthenticationResponse> Login(IdentityRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToLoginCommand());
        return result.ToResponse();
    }

    public override async Task<AuthenticationResponse> Register(IdentityRequest request, ServerCallContext context)
    {
        var result = await _sender.Send(request.ToRegisterCommand());
        var response = (result as Result<UserResultResponse>).ToResponse();
        return response;
    }
}