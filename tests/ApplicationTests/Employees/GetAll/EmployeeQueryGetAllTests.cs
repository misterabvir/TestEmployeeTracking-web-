using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Employees.Queries.GetAll;
using Entities.Employees;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace ApplicationTests.Employees.GetAll;

public class EmployeeQueryGetAllTests
{
    private readonly IEmployeeRepository _employeeRepositoryMock;
    private readonly GetAllEmployeeQueryHandler _handler;
    private readonly GetAllEmployeeQuery _query = new();
    public EmployeeQueryGetAllTests()
    {
        _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        _handler = new(_employeeRepositoryMock);
    }

    [Fact]
    public async Task ShouldBeCallRepository()
    {
        //Arrange
        _employeeRepositoryMock.Get(default).Returns(
            Task.Run(() => Enumerable.Empty<Employee>()));

        //Act
        var result = await _handler.Handle(_query, default);

        //Assert
        _employeeRepositoryMock.Received(1);
    }

    [Fact]
    public async Task ResultShouldBeSuccess()
    {
        //Arrange
        _employeeRepositoryMock.Get(default).Returns(Task.Run(() => Enumerable.Empty<Employee>()));

        //Act
        var result = await _handler.Handle(_query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
