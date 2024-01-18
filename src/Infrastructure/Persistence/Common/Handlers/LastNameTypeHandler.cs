using Dapper;
using Entities.Employees.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class LastNameTypeHandler : SqlMapper.TypeHandler<LastName>
{
    public override LastName? Parse(object value)
    => LastName.Create((string)value);

    public override void SetValue(IDbDataParameter parameter, LastName? value)
    => parameter.Value = value?.Value;
}
