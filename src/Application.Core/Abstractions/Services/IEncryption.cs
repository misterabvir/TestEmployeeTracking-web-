namespace Core.Abstractions.Services;

/// <summary>
/// Service for encryption
/// </summary>
public interface IEncryption
{
    /// <summary>
    /// Encrypt password
    /// </summary>
    /// <param name="password"> Password to encrypt </param>
    /// <param name="salt"> Salt </param>
    /// <returns> Encrypted password </returns>
    string EncryptPassword(string password, string salt);
}
