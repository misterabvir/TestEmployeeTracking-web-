using Dapper;
using Entities.Employees.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class EmployeeIdTypeNahdler : SqlMapper.TypeHandler<EmployeeId>
{
    public override EmployeeId? Parse(object value)
    => EmployeeId.Create((Guid)value);

    public override void SetValue(IDbDataParameter parameter, EmployeeId? value)
    => parameter.Value = value?.Value;
}
