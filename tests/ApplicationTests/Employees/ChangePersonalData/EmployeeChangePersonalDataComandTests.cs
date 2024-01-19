using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Employees.Commands.ChangePersonalData;
using Entities.Abstractions;
using Entities.Abstractions.Services;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using Domain.Common;
using NSubstitute;
using FluentAssertions;
using ApplicationCore.Employees.Responses;
using Core;
using Entities.Abstractions.Shared;

namespace ApplicationTests.Employees.ChangePersonalData;

public class EmployeeChangePersonalDataCommandTests
{
    private readonly ChangePersonalDataCommand _command;
    private readonly ChangePersonalDataCommandHandler _handler;
    private readonly IEmployeeRepository _employeeRepositoryMock;
    private readonly IEmployeeService _employeeServiceMock;

    public EmployeeChangePersonalDataCommandTests()
    {
        _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        _employeeServiceMock = Substitute.For<IEmployeeService>();
        _command = new(new(Guid.NewGuid(), "lastname", "firstname"));
        _handler = new(_employeeRepositoryMock, _employeeServiceMock);

    }

    [Fact]
    public async Task SuccessShouldBeCallRepositoryAndService()
    {
        //Arrange
        _employeeRepositoryMock.Get(Arg.Any<Id>(), default).Returns(
            Employee.Create(
                LastName.Create(_command.Request.LastName),
                FirstName.Create(_command.Request.FirstName),
                DepartmentId.CreateUnique()));
        _employeeServiceMock.ChangePersonalData(Arg.Any<Employee>(), Arg.Any<LastName>(), Arg.Any<FirstName>()).Returns(Result.Success());


        //Act
        await _handler.Handle(_command, default);

        //Assert
        await _employeeRepositoryMock.Received(1).Get(Arg.Any<Id>(), default);
        await _employeeRepositoryMock.Received(1).Update(Arg.Any<Employee>(), default);
        _employeeServiceMock.Received(1).ChangePersonalData(Arg.Any<Employee>(), Arg.Any<LastName>(), Arg.Any<FirstName>());
    }

    [Fact]
    public async Task NotExistEmployeeShouldBFailureWithNotFoundError()
    {
        //Arrange
        _employeeRepositoryMock.Get(Arg.Any<Id>(), default).Returns((Employee?)null);

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeNotFoundError>();
    }

    [Fact]
    public async Task InvalidServiceArgumentShouldBeFailureWithUnexpectedError()
    {
        Error error = new Error("unexpected", "message", ResultErrorStatus.InvalidArgument);
        
        //Arrange
        _employeeRepositoryMock
            .Get(Arg.Any<Id>(), default)
            .Returns(Employee.Create(
                LastName.Create(_command.Request.LastName),
                FirstName.Create(_command.Request.FirstName),
                DepartmentId.CreateUnique()));
        _employeeServiceMock
            .ChangePersonalData(Arg.Any<Employee>(), Arg.Any<LastName>(), Arg.Any<FirstName>())
            .Returns(Result.Failure(error));
        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeUnexpectedError>();
    }

    [Fact]
    public async Task SuccessShouldBeReturnResultWithValue()
    {

        //Arrange
        Employee employee = Employee.Create(
                LastName.Create(_command.Request.LastName),
                FirstName.Create(_command.Request.FirstName),
                DepartmentId.CreateUnique());
        _employeeRepositoryMock
            .Get(Arg.Any<Id>(), default)
            .Returns(employee);
        _employeeServiceMock
            .ChangePersonalData(Arg.Any<Employee>(), Arg.Any<LastName>(), Arg.Any<FirstName>())
            .Returns(Result.Success());
        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Should().Be(EmployeeResultResponse.FromDomain(employee));
    }
}
