using Dapper;
using Entities.Users.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class UserIdHandler : SqlMapper.TypeHandler<UserId>
{
    public override UserId? Parse(object value)
    => UserId.Create((Guid)value);

    public override void SetValue(IDbDataParameter parameter, UserId? value)
    => parameter.Value = value?.Value;
}