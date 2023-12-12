using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Employees.Commands.Delete;
using ApplicationCore.Employees.Errors;
using ApplicationCore.Employees.Responses;
using Domain.Common;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Employees.Delete;

public class DeleteEmployeeCommandHandlerTests
{
    private readonly IEmployeeRepository _employeeRepositoryMock;
    private readonly DeleteEmployeeCommand _command;
    private readonly DeleteEmployeeCommandHandler _handler;

    public DeleteEmployeeCommandHandlerTests()
    {
        _command = new(new(Guid.NewGuid()));
        _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        _handler = new(_employeeRepositoryMock);
    }

     [Fact]
    public async Task SuccessShouldBeCallRepository()
    {
        //Arrange
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default)
            .Returns(Employee.Create(
                LastName.Create("lastname"), 
                FirstName.Create("firstname"),
                DepartmentId.CreateUnique()));

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        await _employeeRepositoryMock.Received(1).Delete(Arg.Any<EmployeeId>(), default);
        await _employeeRepositoryMock.Received(1).Get(Arg.Any<EmployeeId>(), default);
    }

     [Fact]
    public async Task NotExistEmployeeShouldBeFailWithNotFoundError()
    {
        //Arrange
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns((Employee?)null);

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmployeeErrors.NotFound(_command.Request.EmployeeId));
    }

     [Fact]
    public async Task SuccessShouldBeReturnSuccessResult()
    {
        //Arrange
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default)
            .Returns(Employee.Create(
                LastName.Create("lastname"), 
                FirstName.Create("firstname"),
                DepartmentId.CreateUnique()));

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}

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
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }
}