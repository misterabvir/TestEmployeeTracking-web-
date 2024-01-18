using Core.Abstractions.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace EncryptService;

/// <summary>
/// Service for encryption
/// </summary>
public class Encryption : IEncryption
{
   /// <summary>
   /// Number of bytes required
   /// </summary>
   private readonly int _numBytesRequired = 512 / 8;
   /// <summary>
   /// Count of iterations
   /// </summary>
   private readonly int _iterationCount = 5000;
   /// <summary>
   /// Derivation algorithm
   /// </summary>
   private readonly KeyDerivationPrf _keyDerivationPrf = KeyDerivationPrf.HMACSHA512;
   /// <summary>
   /// Encrypt password
   /// </summary>
   /// <param name="password"> Raw password to encrypt </param>
   /// <param name="salt"> Salt of <see cref="User"/> for encryption</param>
   /// <returns> Encrypted password </returns>
   public string EncryptPassword(string password, string salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password,
               Encoding.ASCII.GetBytes(salt),
               _keyDerivationPrf,
               _iterationCount,
               _numBytesRequired));
               
    }
}
