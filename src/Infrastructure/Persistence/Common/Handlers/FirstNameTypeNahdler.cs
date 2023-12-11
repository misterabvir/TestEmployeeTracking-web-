using Dapper;
using Entities.Employees.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class FirstNameTypeNahdler : SqlMapper.TypeHandler<FirstName>
{
    public override FirstName? Parse(object value)
    => FirstName.Create((string)value);

    public override void SetValue(IDbDataParameter parameter, FirstName? value)
    => parameter.Value = value?.Value;
}
