using ApplicationCore.Employees.Commands.Delete;
using Core;
using Domain.Common;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Employees.Delete;

public class DeleteEmployeeCommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result> _next;
    private readonly DeleteEmployeeCommandHandlerBehavior _behavior;

    public DeleteEmployeeCommandValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result>>();

        _behavior = new();
    }

        [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {

        //Arrange
        DeleteEmployeeCommand command = new(new( Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task EmptyLastnameShouldBeReturnValidationError()
    {
        //Arrange
        DeleteEmployeeCommand command = new(new(Guid.Empty));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeValidationError>();
    }
}