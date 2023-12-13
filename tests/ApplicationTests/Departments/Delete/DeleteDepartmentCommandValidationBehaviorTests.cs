using ApplicationCore.Departments.Commands.Delete;
using Core;
using Domain.Common;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Departments.Delete;

public class DeleteDepartmentCommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result> _next;
    private readonly DeleteDepartmentCommandHandlerBehavior _behavior;

    public DeleteDepartmentCommandValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result>>();
        _behavior = new();
    }

        [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {
        //Arrange
        DeleteDepartmentCommand command = new(new(DepartmentId: Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task EmptyIdShouldBeReturnValidationError()
    {
        //Arrange
        DeleteDepartmentCommand command = new(new(DepartmentId: Guid.Empty));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentValidationError>();
    }
}