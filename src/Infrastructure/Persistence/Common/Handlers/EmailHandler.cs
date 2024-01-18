using Dapper;
using Entities.Users.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class EmailHandler : SqlMapper.TypeHandler<Email>
{
    public override Email? Parse(object value)
    => Email.Create((string)value);

    public override void SetValue(IDbDataParameter parameter, Email? value)
    => parameter.Value = value?.Value;
}
