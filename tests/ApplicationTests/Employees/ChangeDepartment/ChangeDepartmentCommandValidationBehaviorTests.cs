using ApplicationCore.Employees.Commands.ChangeDepartment;
using ApplicationCore.Employees.Errors;
using ApplicationCore.Employees.Responses;
using Domain.Common;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Employees.ChangeDepartment
{
public class ChangeDepartmentCommandValidationBehaviorTests{
    private readonly RequestHandlerDelegate<Result<EmployeeResultResponse>> _next;
    private readonly ChangeDepartmentCommandBehavior _behavior;

    public ChangeDepartmentCommandValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<EmployeeResultResponse>>>();
        _behavior = new();
    }

    [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {
        //Arrange
        ChangeDepartmentCommand command = new(new(EmployeeId: Guid.NewGuid(), DepartmentId: Guid.NewGuid()));

        //Act
        await _behavior.Handle(command, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    
    [Fact]
    public async Task EmptyEmployeeIdShouldBeReturnValidationError()
    {
        //Arrange
        ChangeDepartmentCommand command = new(new(EmployeeId: Guid.Empty, DepartmentId: Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }

    [Fact]
    public async Task EmptyDepartmentIdShouldBeReturnValidationError()
    {
        //Arrange
        ChangeDepartmentCommand command = new(new(EmployeeId: Guid.NewGuid(), DepartmentId: Guid.Empty));

        //Act
        var result = await _behavior.Handle(command, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }
}
}