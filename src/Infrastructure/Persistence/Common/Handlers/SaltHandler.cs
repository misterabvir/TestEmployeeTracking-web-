using Dapper;
using Entities.Users.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class SaltHandler : SqlMapper.TypeHandler<Salt>
{
    public override Salt? Parse(object value)
    => Salt.Create((Guid)value);

    public override void SetValue(IDbDataParameter parameter, Salt? value)
    => parameter.Value = value?.Value;
}
