using Entities.Abstractions.General;

namespace Entities.Departments.ValueObjects;

/// <summary>
/// Department title
/// </summary>
public sealed class Title : ValueObject
{
    private Title() { }
    
    /// <summary>
    /// Value of title
    /// </summary>
    public string Value { get; private set; } = default!;

    /// <summary>
    /// Factory for <see cref="Title"/>
    /// </summary>
    /// <param name="title"> Value of title </param>
    /// <returns> Domain entity of <see cref="Title"/> </returns>
    public static Title Create(string title)
        => new() { Value = title };

    /// <summary>
    /// Implementing <see cref="ValueObject"/>
    /// </summary>
    /// <returns>Components for comparison</returns>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
