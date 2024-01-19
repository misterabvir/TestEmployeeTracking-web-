using Dapper;
using Entities.Departments.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class TitleTypeHandler : SqlMapper.TypeHandler<Title>
{
    public override Title? Parse(object value)
    => Title.Create((string)value);

    public override void SetValue(IDbDataParameter parameter, Title? value)
    => parameter.Value = value?.Value;
}
