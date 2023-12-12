using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Abstractions.Services;
using ApplicationCore.Employees.Commands.ChangeDepartment;
using ApplicationCore.Employees.Errors;
using ApplicationCore.Employees.Responses;
using Domain.Common;
using Entities.Abstractions.Services;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using Entities.Histories;
using Entities.Histories.ValueObjects;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Employees.ChangeDepartment;

public class ChangeDepartmentCommandTests
{
    private readonly ChangeDepartmentCommand _command;
    private readonly ChangeDepartmentCommandHandler _handler;

    private readonly IEmployeeRepository _employeeRepositoryMock;
    private readonly IDepartmentRepository _departmentRepositoryMock;
    private readonly IHistoryRepository _historyRepositoryMock;
    private readonly IDateTimeService _dateTimeServiceMock;
    private readonly IEmployeeService _employeeServiceMock;
    private readonly IHistoryService _historyServiceMock;

    private readonly DateOnly _today = DateOnly.FromDateTime(DateTime.Today);

    public ChangeDepartmentCommandTests()
    {
        _command = new(new(
            EmployeeId: Guid.NewGuid(), 
            DepartmentId: Guid.NewGuid()));
        _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        _departmentRepositoryMock = Substitute.For<IDepartmentRepository>();
        _historyRepositoryMock = Substitute.For<IHistoryRepository>();
        _dateTimeServiceMock = Substitute.For<IDateTimeService>();
        _employeeServiceMock = Substitute.For<IEmployeeService>();
        _historyServiceMock = Substitute.For<IHistoryService>();
        _handler = new(
            _employeeRepositoryMock, 
            _departmentRepositoryMock, 
            _historyRepositoryMock,
            _dateTimeServiceMock, 
            _employeeServiceMock, 
            _historyServiceMock);
    }

    [Fact]
    public async Task SuccessShouldBeCallRepositoriesAndServices()
    {
        //Arrange
        Employee employee = Employee.Create(
                EmployeeId.Create(_command.Request.EmployeeId),
                LastName.Create("lastname"),
                FirstName.Create("firstname"),
                DepartmentId.CreateUnique());
        
        Department department = Department.Create(
            DepartmentId.Create(_command.Request.DepartmentId),
            Title.Create("title"));

        History history = History.Create(
            HistoryId.CreateUnique(),
            employee.Id,
            department.Id,
            _today,
            null);
            
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns(employee);
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(department);
        _historyRepositoryMock.Get(Arg.Any<EmployeeId>(), Arg.Any<DepartmentId>(), default).Returns(history);
        _dateTimeServiceMock.Today.Returns(_today);
        _employeeServiceMock.ChangeDepartment(Arg.Any<Employee>(), Arg.Any<DepartmentId>()).Returns(Result.Success());
        _historyServiceMock.Complete(Arg.Any<History>(), Arg.Any<DateOnly>()).Returns(Result<History>.Success(history));
        
        //Act
        await _handler.Handle(_command, default);
        
        //Assert 
        await _employeeRepositoryMock.Received(1).Get(Arg.Any<EmployeeId>(), default);
        await _departmentRepositoryMock.Received(1).Get(Arg.Any<DepartmentId>(), default);
        await _historyRepositoryMock.Received(1).Get(Arg.Any<EmployeeId>(), Arg.Any<DepartmentId>(), default);
        await _employeeRepositoryMock.Received(1).Update(Arg.Any<Employee>(), default);
        await _historyRepositoryMock.Received(1).Update(Arg.Any<History>(), default);
        await _historyRepositoryMock.Received(1).Create(Arg.Any<History>(), default);
        _employeeServiceMock.Received(1).ChangeDepartment(Arg.Any<Employee>(), Arg.Any<DepartmentId>());
        _historyServiceMock.Received(1).Complete(Arg.Any<History>(), Arg.Any<DateOnly>());
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
    public async Task NotExistDepartmentShouldBeFailWithDepartmentNotExistError()
    {
        //Arrange
        Employee employee = Employee.Create(
                EmployeeId.Create(_command.Request.EmployeeId),
                LastName.Create("lastname"),
                FirstName.Create("firstname"),
                DepartmentId.CreateUnique());           
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns(employee);
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns((Department?)null);
        
        //Act
        var result = await _handler.Handle(_command, default);
        
        //Assert 
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmployeeErrors.DepartmentNotExist(_command.Request.DepartmentId));
    }

        [Fact]
    public async Task SameDepartmentsShouldBeFailWithAlreadyInDepartmentError()
    {
        //Arrange
        Department department = Department.Create(
            DepartmentId.Create(_command.Request.DepartmentId),
            Title.Create("title"));
        Employee employee = Employee.Create(
                EmployeeId.Create(_command.Request.EmployeeId),
                LastName.Create("lastname"),
                FirstName.Create("firstname"),
                department.Id);
            
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns(employee);
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(department);

        //Act
        var result = await _handler.Handle(_command, default);
        
        //Assert 
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmployeeErrors.AlreadyInDepartment(_command.Request.EmployeeId));
    }

    [Fact]
    public async Task FailureChangeBeFailWithUnexpectedError()
    {
        //Arrange
        Employee employee = Employee.Create(
                EmployeeId.Create(_command.Request.EmployeeId),
                LastName.Create("lastname"),
                FirstName.Create("firstname"),
                DepartmentId.CreateUnique());
        
        Department department = Department.Create(
            DepartmentId.Create(_command.Request.DepartmentId),
            Title.Create("title"));
            
        Error error = new("unexpected", "message", ResultErrorStatus.InvalidArgument);    
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns(employee);
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(department);
        _employeeServiceMock.ChangeDepartment(Arg.Any<Employee>(), Arg.Any<DepartmentId>()).Returns(Result.Failure(error));
        
        //Act
        var result = await _handler.Handle(_command, default);
        
        //Assert 
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmployeeErrors.UnexpectedError(error));
    }

    
    [Fact]
    public async Task FailureCompleteBeFailWithUnexpectedError()
    {
        //Arrange
        Employee employee = Employee.Create(
                EmployeeId.Create(_command.Request.EmployeeId),
                LastName.Create("lastname"),
                FirstName.Create("firstname"),
                DepartmentId.CreateUnique());
        
        Department department = Department.Create(
            DepartmentId.Create(_command.Request.DepartmentId),
            Title.Create("title"));

        History history = History.Create(
            HistoryId.CreateUnique(),
            employee.Id,
            department.Id,
            _today,
            null);
        Error<History> error = new("unexpected", "message", ResultErrorStatus.InvalidArgument); 
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns(employee);
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(department);
        _historyRepositoryMock.Get(Arg.Any<EmployeeId>(), Arg.Any<DepartmentId>(), default).Returns(history);
        _dateTimeServiceMock.Today.Returns(_today);
        _employeeServiceMock.ChangeDepartment(Arg.Any<Employee>(), Arg.Any<DepartmentId>()).Returns(Result.Success());
        _historyServiceMock.Complete(Arg.Any<History>(), Arg.Any<DateOnly>()).Returns(Result<History>.Failure(error));
        
        //Act
        var result = await _handler.Handle(_command, default);
        
        //Assert 
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(EmployeeErrors.UnexpectedError(error));
    }

        [Fact]
    public async Task SuccessShouldBeReturnResultWithValue()
    {
        //Arrange
        Employee employee = Employee.Create(
                EmployeeId.Create(_command.Request.EmployeeId),
                LastName.Create("lastname"),
                FirstName.Create("firstname"),
                DepartmentId.CreateUnique());
        
        Department department = Department.Create(
            DepartmentId.Create(_command.Request.DepartmentId),
            Title.Create("title"));

        History history = History.Create(
            HistoryId.CreateUnique(),
            employee.Id,
            department.Id,
            _today,
            null);
            
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns(employee);
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(department);
        _historyRepositoryMock.Get(Arg.Any<EmployeeId>(), Arg.Any<DepartmentId>(), default).Returns(history);
        _dateTimeServiceMock.Today.Returns(_today);
        _employeeServiceMock.ChangeDepartment(Arg.Any<Employee>(), Arg.Any<DepartmentId>()).Returns(Result.Success());
        _historyServiceMock.Complete(Arg.Any<History>(), Arg.Any<DateOnly>()).Returns(Result<History>.Success(history));
        
        //Act
        var result = await _handler.Handle(_command, default);
        
        //Assert 
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(EmployeeResultResponse.FromDomain(employee));
    }
}


