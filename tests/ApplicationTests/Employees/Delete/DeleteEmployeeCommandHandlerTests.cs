using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Employees.Commands.Delete;
using Core;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using FluentAssertions;
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
        result.Error.Should().BeOfType<Errors.EmployeeNotFoundError>();
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
