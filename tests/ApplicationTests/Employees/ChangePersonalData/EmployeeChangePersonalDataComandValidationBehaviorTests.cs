using ApplicationCore.Employees.Commands.ChangePersonalData;
using Domain.Common;
using NSubstitute;
using FluentAssertions;
using ApplicationCore.Employees.Errors;
using ApplicationCore.Employees.Responses;
using MediatR;
using FluentValidation.Results;

namespace ApplicationTests.Employees.ChangePersonalData;

public class EmployeeChangePersonalDataCommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<EmployeeResultResponse>> _next;
    private readonly ChangePersonalDataCommandHandlerBehavior _behavior;

    public EmployeeChangePersonalDataCommandValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<EmployeeResultResponse>>>();
        _behavior = new();
    }

    [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {
        //Arrange
        ChangePersonalDataCommand command = new(new(Guid.NewGuid(), "lastname", "firstname"));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task EmptyLastnameShouldBeReturnValidationError()
    {
        //Arrange
        ChangePersonalDataCommand command = new(new(Guid.NewGuid(), "", "firstname"));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }

    [Fact]
    public async Task ShortLastnameShouldBeReturnValidationError()
    {
        //Arrange
        ChangePersonalDataCommand command = new(new(Guid.NewGuid(), "l", "firstname"));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }

    [Fact]
    public async Task NullLastnameShouldBeReturnValidationError()
    {
        //Arrange
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ChangePersonalDataCommand command = new(new(Guid.NewGuid(), null, "firstname"));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }

    [Fact]
    public async Task EmptyFirstnameShouldBeReturnValidationError()
    {
        //Arrange
        ChangePersonalDataCommand command = new(new(Guid.NewGuid(), "lastname", ""));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }

    [Fact]
    public async Task ShortFirstnameShouldBeReturnValidationError()
    {
        //Arrange
        ChangePersonalDataCommand command = new(new(Guid.NewGuid(), "lastname", "f"));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }


    [Fact]
    public async Task NullFirstnameShouldBeReturnValidationError()
    {
        //Arrange
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ChangePersonalDataCommand command = new(new(Guid.NewGuid(), "lastname", null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }

    [Fact]
    public async Task EmptyDepartmentShouldBeReturnValidationError()
    {
        //Arrange
        ChangePersonalDataCommand command = new(new(Guid.Empty, "lastname", "firstname"));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }
}