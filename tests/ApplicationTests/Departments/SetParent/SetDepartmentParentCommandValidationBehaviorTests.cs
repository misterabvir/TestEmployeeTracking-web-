using ApplicationCore.Departments.Commands.SetParent;
using ApplicationCore.Departments.Responses;
using Core;
using Domain.Common;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Departments.SetParent;

public class SetDepartmentParentCommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<DepartmentResultResponse>> _next;
    private readonly SetDepartmentParentCommandHandlerBehavior _behavior;

    public SetDepartmentParentCommandValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<DepartmentResultResponse>>>();
        _behavior = new();
    }

    [Fact]
    public async Task SuccessWithParentIdIsNotNullShouldBeCallHandlerMock()
    {
        //Arrange
        SetDepartmentParentCommand command = new(new(Guid.NewGuid(), Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task SuccessWithParentIdIsNullShouldBeCallHandlerMock()
    {
        //Arrange
        SetDepartmentParentCommand command = new(new(Guid.NewGuid(), null));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task DepartmentIdIsEmptyShouldBeReturnValidationError()
    {
        //Arrange
        SetDepartmentParentCommand command = new(new(Guid.Empty, Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentValidationError>();
    }

    [Fact]
    public async Task DepartmentParentIdIsEmptyShouldBeReturnValidationError()
    {
        //Arrange
        SetDepartmentParentCommand command = new(new(Guid.NewGuid(), Guid.Empty));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentValidationError>();
    }

    
    [Fact]
    public async Task SameIdsShouldBeReturnValidationError()
    {
        //Arrange
        Guid id = Guid.NewGuid();
        SetDepartmentParentCommand command = new(new(id, id));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentValidationError>();
    }
}