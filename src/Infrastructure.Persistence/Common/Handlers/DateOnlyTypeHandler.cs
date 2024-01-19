using Dapper;
using Entities.Abstractions;
using Entities.Departments.ValueObjects;
using Entities.Employees.ValueObjects;
using Entities.Histories.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly?>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly? date)
        => parameter.Value = date?.ToDateTime(new TimeOnly(0, 0));

    public override DateOnly? Parse(object? value)
        => value is null ? null : DateOnly.FromDateTime((DateTime)value);
}
