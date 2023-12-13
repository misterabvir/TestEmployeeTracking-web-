using ApplicationCore.Employees.Commands.Create;
using ApplicationCore.Employees.Responses;
using Core;
using Domain.Common;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;

namespace ApplicationTests.Employees.Create;

public class EmployeeCreateCommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<EmployeeResultResponse>> _next;
    private readonly CreateEmployeeCommandHandlerBehavior _behavior;

    public EmployeeCreateCommandValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<EmployeeResultResponse>>>();

        _behavior = new();
    }

    [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {

        //Arrange
        CreateEmployeeCommand command = new(new("lastname", "firstname", Guid.NewGuid()));
        _behavior.Handle(command, _next, default).ReturnsNull();

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task EmptyLastnameShouldBeReturnValidationError()
    {
        //Arrange
        CreateEmployeeCommand command = new(new("", "firstname", Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeValidationError>();
    }

    [Fact]
    public async Task ShortLastnameShouldBeReturnValidationError()
    {
        //Arrange
        CreateEmployeeCommand command = new(new("l", "firstname", Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeValidationError>();
    }

    [Fact]
    public async Task NullLastnameShouldBeReturnValidationError()
    {
        //Arrange
        CreateEmployeeCommand command = new(new(null, "firstname", Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeValidationError>();
    }

    [Fact]
    public async Task EmptyFirstnameShouldBeReturnValidationError()
    {
        //Arrange
        CreateEmployeeCommand command = new(new("lastname", "", Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeValidationError>();
    }

    [Fact]
    public async Task ShortFirstnameShouldBeReturnValidationError()
    {
        //Arrange
        CreateEmployeeCommand command = new(new("lastname", "f", Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeValidationError>();
    }


    [Fact]
    public async Task NullFirstnameShouldBeReturnValidationError()
    {
        //Arrange
        CreateEmployeeCommand command = new(new("lastname", null, Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeValidationError>();
    }

    [Fact]
    public async Task EmptyDepartmentShouldBeReturnValidationError()
    {
        //Arrange
        CreateEmployeeCommand command = new(new("lastname", "firstname", Guid.Empty));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeValidationError>();
    }

}
