using Dapper;
using Entities.Users.ValueObjects;
using System.Data;

namespace Persistence.Common.Handlers;

internal class PasswordHandler : SqlMapper.TypeHandler<Password>
{
    public override Password? Parse(object value)
    => Password.Create((string)value);

    public override void SetValue(IDbDataParameter parameter, Password? value)
    => parameter.Value = value?.Value;
}
