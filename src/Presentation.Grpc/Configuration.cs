using Grpc.Services;

namespace Grpc;

public static class Configuration
{ 
    public static WebApplication UseIdentity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static WebApplication UseGrpc(this WebApplication app)
    {
        app.UseGrpcWeb();
        app.UseCors();
        return app;
    }

    public static WebApplication UseMapServices(this WebApplication app, IConfiguration configuration)
    {
        var policy = configuration.GetSection("Policy").Value;
        if (string.IsNullOrEmpty(policy)) throw new Exception("can't find policy");
        app.MapGrpcService<IdentityService>().EnableGrpcWeb().RequireCors(policy);
        app.MapGrpcService<EmployeeService>().EnableGrpcWeb().RequireCors(policy);
        app.MapGrpcService<DepartmentService>().EnableGrpcWeb().RequireCors(policy);
        app.MapGrpcService<HistoryService>().EnableGrpcWeb().RequireCors(policy);
        return app;
    }
}