using Core.Employees.Queries.GetAll;
using Grpc.Core;
using MediatR;
namespace Grpc.Services;

public class EmployeeService : EmployeesProto.EmployeesProtoBase
{
    private readonly ISender _sender;

    public EmployeeService(ISender sender)
    {
        _sender = sender;
    }

    public override async Task GetAllEmployee(
        GetAllRequest request,
        IServerStreamWriter<EmployeeDataModel> responseStream,
        ServerCallContext context)
    {
        var result = await _sender.Send(new GetAllEmployeeQuery());
        
        var employees = result.Value!.Select(s=>new EmployeeDataModel(){
              Id = s.Id.ToString(),
               Lastname = s.LastName,
               Firstname = s.FirstName,
               DepartmentId = s.DepartmentId.ToString(),   
        });

        foreach (var employee in employees)
        {
            await responseStream.WriteAsync(employee);
        }
    }
}
