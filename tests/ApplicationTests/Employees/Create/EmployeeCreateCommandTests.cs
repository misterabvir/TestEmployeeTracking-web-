using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Abstractions.Services;
using ApplicationCore.Employees.Commands.Create;
using Core;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Histories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace ApplicationTests.Employees.Create;

public class EmployeeCreateCommandHandlerTests
{
    private readonly CreateEmployeeCommand _command;
    private readonly CreateEmployeeCommandHandler _handler;
    private readonly IDepartmentRepository _departmentRepositoryMock;
    private readonly IEmployeeRepository _employeeRepositoryMock;
    private readonly IHistoryRepository _historyRepositoryMock;
    private readonly IDateTimeService _dateTimeServiceMock;
    private readonly Department _department;
    private readonly DateOnly _today;

    public EmployeeCreateCommandHandlerTests()
    {
        _command = new(new("lastname", "firstname", Guid.NewGuid()));
        _departmentRepositoryMock = Substitute.For<IDepartmentRepository>();

        _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        _historyRepositoryMock = Substitute.For<IHistoryRepository>();
        _dateTimeServiceMock = Substitute.For<IDateTimeService>();
        _handler = new(
            _employeeRepositoryMock,
            _departmentRepositoryMock,
            _historyRepositoryMock,
            _dateTimeServiceMock);
        _today = DateOnly.FromDateTime(DateTime.UtcNow);
        _department = Department.Create(Title.Create(string.Empty), null);
        _dateTimeServiceMock.Today.Returns(_today);
    }

    [Fact]
    public async Task ShouldBeCallRepositories()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default)
            .Returns(Task.Run(() => (Department?)_department));
        _employeeRepositoryMock.Create(Arg.Any<Employee>(), default)
            .Returns(Task.CompletedTask);
        _historyRepositoryMock.Create(Arg.Any<History>(), default)
            .Returns(Task.CompletedTask);

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        _departmentRepositoryMock.Received(1).Get(Arg.Any<DepartmentId>(), default).Wait();
        _employeeRepositoryMock.Received(1).Create(Arg.Any<Employee>(), default).Wait();
        _historyRepositoryMock.Received(1).Create(Arg.Any<History>(), default).Wait();
    }

    [Fact]
    public async Task ResultEmployeeCreateWithInvalidDepartmentShouldBeFailure()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default)
            .Returns(Task.Run(() => (Department?)null));

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.EmployeeDepartmentNotExistError>();
    }

    [Fact]
    public async Task ResultShouldBeSuccess()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default)
            .Returns(Task.Run(() => (Department?)_department));

        //Act
        var result = await _handler.Handle(_command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
