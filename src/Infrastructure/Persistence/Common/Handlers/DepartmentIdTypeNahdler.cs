using Dapper;
using Entities.Departments.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class DepartmentIdTypeNahdler : SqlMapper.TypeHandler<DepartmentId>
{
    public override DepartmentId? Parse(object value)
    => DepartmentId.Create((Guid)value);

    public override void SetValue(IDbDataParameter parameter, DepartmentId? value)
    => parameter.Value = value?.Value;
}
