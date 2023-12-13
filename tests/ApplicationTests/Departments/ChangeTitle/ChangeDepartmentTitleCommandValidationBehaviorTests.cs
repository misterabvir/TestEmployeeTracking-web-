using ApplicationCore.Departments.Commands.ChangeTitle;
using ApplicationCore.Departments.Responses;
using Domain.Common;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Departments.ChangeTitle;

public class ChangeDepartmentTitleCommandValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<DepartmentResultResponse>> _next;
    private readonly ChangeDepartmentTitleCommandHandlerBehavior _behavior;

    public ChangeDepartmentTitleCommandValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<DepartmentResultResponse>>>();
        _behavior = new();
    }

        [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {
        //Arrange
        ChangeDepartmentTitleCommand command = new(new(DepartmentId: Guid.NewGuid(), Title: "title"));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task TitleIsNullShouldBeCallReturnValidationError()
    {
        //Arrange
        ChangeDepartmentTitleCommand command = new(new(DepartmentId: Guid.NewGuid(), Title: null));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Core.Errors.DepartmentValidationError>();
    }

        [Fact]
    public async Task TitleIsEmptyShouldBeCallReturnValidationError()
    {
        //Arrange
        ChangeDepartmentTitleCommand command = new(new(DepartmentId: Guid.NewGuid(), Title: string.Empty));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Core.Errors.DepartmentValidationError>();
    }

    [Fact]
    public async Task TitleIsShortShouldBeCallReturnValidationError()
    {
        //Arrange
        ChangeDepartmentTitleCommand command = new(new(DepartmentId: Guid.NewGuid(), Title: "sh"));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Core.Errors.DepartmentValidationError>();
    }
}