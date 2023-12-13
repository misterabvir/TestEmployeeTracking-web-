using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Histories.Queries.GetEmployeeHistory;
using Entities.Employees.ValueObjects;
using Entities.Employees;
using Entities.Histories;
using FluentAssertions;
using NSubstitute;
using Entities.Departments.ValueObjects;

namespace ApplicationTests.Histories.GetEmployeeHistory;


public class GetEmployeeHistoryQueryHandlerTests
{
    private readonly IHistoryRepository _historyRepositoryMock;
    private readonly IEmployeeRepository _employeeRepositoryMock;
    private readonly GetEmployeeHistoryQueryHandler _handler;

    public GetEmployeeHistoryQueryHandlerTests()
    {
        _historyRepositoryMock = Substitute.For<IHistoryRepository>();
        _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        _handler = new(_historyRepositoryMock, _employeeRepositoryMock);
    }

    
    [Fact]
    public async Task SuccessShouldBeCallRepository()
    {
        //Arrange
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns(Employee.Create(LastName.Create(string.Empty), FirstName.Create(string.Empty), DepartmentId.CreateUnique()));
        _historyRepositoryMock.GetEmployeeHistory(Arg.Any<EmployeeId>(), default)
            .Returns(Enumerable.Empty<History>());

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), default);

        //Assert
        await _employeeRepositoryMock.Received(1).Get(Arg.Any<EmployeeId>(), default);
        await _historyRepositoryMock.Received(1).GetEmployeeHistory(Arg.Any<EmployeeId>(), default);
    }

        [Fact]
    public async Task NotExistShouldBeReturnNotFoundError()
    {
        //Arrange
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns((Employee?)null);

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Core.Errors.HistoryEmployeeNotFoundError>();
    }

        [Fact]
    public async Task SuccessShouldBeReturnSuccessResult()
    {
        //Arrange
        _employeeRepositoryMock.Get(Arg.Any<EmployeeId>(), default).Returns(Employee.Create(LastName.Create(string.Empty), FirstName.Create(string.Empty), DepartmentId.CreateUnique()));
        _historyRepositoryMock.GetEmployeeHistory(Arg.Any<EmployeeId>(), default)
            .Returns(Enumerable.Empty<History>());

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
