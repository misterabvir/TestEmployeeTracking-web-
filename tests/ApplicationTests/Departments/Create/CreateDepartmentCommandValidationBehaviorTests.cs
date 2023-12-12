using ApplicationCore.Departments.Commands.Create;
using ApplicationCore.Departments.Errors;
using ApplicationCore.Departments.Responses;
using Domain.Common;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Departments.Create;

public class CreateDepartmentCommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<DepartmentResultResponse>> _next;
    private readonly CreateDepartmentCommandHandlerBehavior _behavior;

    public CreateDepartmentCommandValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<DepartmentResultResponse>>>();
        _behavior = new();
    }

        [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {
        //Arrange
        CreateDepartmentCommand command = new(new("title", Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task EmptyTitleShouldBeReturnValidationError()
    {
        //Arrange
         CreateDepartmentCommand command = new(new(string.Empty, Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(DepartmentErrors.ValidationError(new ValidationResult()).Title);
    }

    [Fact]
    public async Task NullTitleShouldBeReturnValidationError()
    {
        //Arrange
         CreateDepartmentCommand command = new(new(null, Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(DepartmentErrors.ValidationError(new ValidationResult()).Title);
    }

        [Fact]
    public async Task EmptyParentIdShouldBeReturnValidationError()
    {
        //Arrange
        CreateDepartmentCommand command = new(new("title", Guid.Empty));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(DepartmentErrors.ValidationError(new ValidationResult()).Title);
    }
}