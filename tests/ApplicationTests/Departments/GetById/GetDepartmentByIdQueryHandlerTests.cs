using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Departments.Errors;
using ApplicationCore.Departments.Queries.GetById;
using ApplicationCore.Departments.Responses;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace ApplicationTests.Departments.GetById;

public class GetDepartmentByIdQueryHandlerTests
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly GetDepartmentByIdQuery _query;
    private readonly GetDepartmentByIdQueryHandler _handler;

    public GetDepartmentByIdQueryHandlerTests()
    {
        _query = new(new(DepartmentId: Guid.NewGuid()));
        _departmentRepository = Substitute.For<IDepartmentRepository>();
        _handler = new(_departmentRepository);
    }

    [Fact]
    public async Task SuccessShouldBeCalRepository()
    {
        //Arrange
        _departmentRepository.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(DepartmentId.Create(_query.Request.DepartmentId), Title.Create("title")));
        //Act
        await _handler.Handle(_query, CancellationToken.None);

        //Assert
        await _departmentRepository.Received(1).Get(Arg.Any<DepartmentId>(), default);
    }

    [Fact]
    public async Task EmptyDepartmentIdShouldBeFailWithNotFoundError()
    {
        //Arrange
        _departmentRepository.Get(Arg.Any<DepartmentId>(), default).Returns((Department?)null);

        //Act
        var result = await _handler.Handle(_query, CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DepartmentErrors.NotFound(_query.Request.DepartmentId));
    }

    [Fact]
    public async Task SuccessShouldBeReturnResult()
    {
        //Arrange
        Department department = Department.Create(DepartmentId.Create(_query.Request.DepartmentId), Title.Create("title"));
        _departmentRepository.Get(Arg.Any<DepartmentId>(), default).Returns(department);

        //Act
        var result = await _handler.Handle(_query, CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(DepartmentResultResponse.FromDomain(department));
    }
}
