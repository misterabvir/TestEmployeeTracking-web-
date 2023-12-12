using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Departments.Queries.GetAll;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using FluentAssertions;
using NSubstitute;

namespace ApplicationTests.Departments.GetAll;

public class GetAllDepartmentsQueryHandlerTests
{
    private readonly IDepartmentRepository _departmentRepositoryMock;
    private readonly GetAllDepartmentsQuery _query;
    private readonly GetAllDepartmentsQueryHandler _handler;

    public GetAllDepartmentsQueryHandlerTests()
    {
        _query = new();
        _departmentRepositoryMock = Substitute.For<IDepartmentRepository>();
        _handler = new(_departmentRepositoryMock);
    }

    [Fact]
    public async Task SuccessShouldBeCallRepository()
    {
        //Arrange
        _departmentRepositoryMock.Get(default).ReturnsForAnyArgs(Enumerable.Empty<Department>());

        //Act
        await _handler.Handle(_query, default);

        //Assert
        await _departmentRepositoryMock.Received(1).Get(default);
    }
    
    [Fact]
    public async Task SuccessShouldReturnResult()
    {
        var departments = Enumerable.Empty<Department>();

        //Arrange
        _departmentRepositoryMock.Get(default).ReturnsForAnyArgs(departments);

        //Act
        var result = await _handler.Handle(_query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(Enumerable.Empty<DepartmentResultResponse>());
    }
}