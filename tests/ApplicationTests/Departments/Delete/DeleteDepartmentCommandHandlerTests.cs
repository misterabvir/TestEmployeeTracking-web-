using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Departments.Commands.Delete;
using Core;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace ApplicationTests.Departments.Delete;

public class DeleteDepartmentCommandHandlerTests
{
    private readonly IDepartmentRepository _departmentRepositoryMock;
    private readonly IEmployeeRepository _employeeRepositoryMock;

    private readonly DeleteDepartmentCommandHandler _handler;

    public DeleteDepartmentCommandHandlerTests()
    {
        _departmentRepositoryMock = Substitute.For<IDepartmentRepository>();
        _employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        _handler = new DeleteDepartmentCommandHandler(_departmentRepositoryMock, _employeeRepositoryMock);
    }

    [Fact]
    public async Task SuccessShouldBeCallRepository()
    {
        //Arrange
        _departmentRepositoryMock
            .Get(Arg.Any<DepartmentId>(), default)
            .Returns(Department.Create(DepartmentId.CreateUnique(), Title.Create("title")));

        _employeeRepositoryMock.GetByDepartmentId(Arg.Any<DepartmentId>(), default).Returns(Enumerable.Empty<Employee>());

        //Act
        await _handler.Handle(new(new(Guid.NewGuid())), CancellationToken.None);

        //Assert
        await _departmentRepositoryMock.Received(1).Get(Arg.Any<DepartmentId>(), default);
        await _employeeRepositoryMock.Received(1).GetByDepartmentId(Arg.Any<DepartmentId>(),default);
        await _departmentRepositoryMock.Received(1).Delete(Arg.Any<DepartmentId>(), default);
    }

    
    [Fact]
    public async Task DepartmentNotExistShouldBeReturnNotFoundError()
    {
        //Arrange
        _departmentRepositoryMock
            .Get(Arg.Any<DepartmentId>(), default)
            .Returns((Department?)null);

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentNotFoundError>();
    }

    [Fact]
    public async Task DepartmentNotEmptyShouldBeReturnDepartmentCantDeleteNotEmptyError()
    {
        //Arrange
        _departmentRepositoryMock
            .Get(Arg.Any<DepartmentId>(), default)
            .Returns(Department.Create(DepartmentId.CreateUnique(), Title.Create("title")));

        _employeeRepositoryMock.GetByDepartmentId(Arg.Any<DepartmentId>(), default).Returns(new List<Employee>{Employee.Create(LastName.Create(""), FirstName.Create(""), DepartmentId.CreateUnique())});

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentCantDeleteNotEmptyError>();
    }

    [Fact]
    public async Task SuccessShouldBeReturnSuccessResult()
    {
        //Arrange
        _departmentRepositoryMock
            .Get(Arg.Any<DepartmentId>(), default)
            .Returns(Department.Create(DepartmentId.CreateUnique(), Title.Create("title")));

        _employeeRepositoryMock.GetByDepartmentId(Arg.Any<DepartmentId>(), default).Returns(Enumerable.Empty<Employee>());

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
