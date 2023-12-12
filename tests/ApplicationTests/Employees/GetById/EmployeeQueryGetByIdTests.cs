using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Employees.Errors;
using ApplicationCore.Employees.Queries.GetById;
using ApplicationCore.Employees.Responses;
using Domain.Common;
using Entities.Abstractions;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace ApplicationTests.Employees.GetById;

public class EmployeeQueryGetByIdTests
{
    private readonly IEmployeeRepository _employeeRepositoryMock;
    private readonly GetEmployeeByIdQueryHandler _handler;
    private readonly GetEmployeeByIdQuery _query;
    public EmployeeQueryGetByIdTests()
    {
        _query = new GetEmployeeByIdQuery(new(Guid.Empty));

        _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        _handler = new(_employeeRepositoryMock);
    }

    [Fact]
    public async Task ShouldBeCallRepository()
    {
        //Arrange
        _employeeRepositoryMock.Get(Substitute.For<Id>(), default)
            .Returns(Task.Run(() => (Employee?)null));

        //Act
        var result = await _handler.Handle(_query, default) as Result<EmployeeResultResponse>;

        //Assert
        _employeeRepositoryMock.Received(1);
    }

    [Fact]
    public async Task ResultFailureShouldBeTrueAndReturnNotFound()
    {
        //Arrange
        _employeeRepositoryMock.Get(EmployeeId.Create(_query.Request.EmployeeId), default)
            .Returns((Employee?)null);

        //Act
        var result = await _handler.Handle(_query, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Value.Should().BeNull();
        result.Error.Should().BeEquivalentTo(EmployeeErrors.NotFound(_query.Request.EmployeeId));
    }


    [Fact]
    public async Task ResultShouldBeSuccess()
    {
        //Arrange
        _employeeRepositoryMock.Get(EmployeeId.Create(_query.Request.EmployeeId), default)
            .Returns(
            Task.Run(() => (Employee?)Employee.Create(
                LastName.Create(string.Empty),
                FirstName.Create(string.Empty),
                DepartmentId.CreateUnique())));

        //Act
        var result = await _handler.Handle(_query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
