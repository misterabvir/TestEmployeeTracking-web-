using Dapper;
using System.Data;

namespace Persistence.Common;

internal class DapperSqlDateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly?>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly? date)
        => parameter.Value = date?.ToDateTime(new TimeOnly(0, 0));

    public override DateOnly? Parse(object? value)
        => value is null ? null : DateOnly.FromDateTime((DateTime)value);
}