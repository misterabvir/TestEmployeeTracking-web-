using Dapper;
using Entities.Histories.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class HistoryIdTypeHandler : SqlMapper.TypeHandler<HistoryId>
{
    public override HistoryId? Parse(object value)
    => HistoryId.Create((Guid)value);

    public override void SetValue(IDbDataParameter parameter, HistoryId? value)
    => parameter.Value = value?.Value;
}