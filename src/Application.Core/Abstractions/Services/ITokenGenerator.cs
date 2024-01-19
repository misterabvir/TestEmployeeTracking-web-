namespace Core.Abstractions.Services;

/// <summary>
/// Token generator
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Generate token
    /// </summary>
    /// <param name="email"> Email of user </param>
    /// <returns> Token </returns>
    string Generate(string email);
}
