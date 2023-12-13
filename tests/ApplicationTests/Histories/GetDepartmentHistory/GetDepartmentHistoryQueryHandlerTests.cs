using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Histories.Queries.GetDepartmentHistory;
using Core;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Histories;
using FluentAssertions;
using NSubstitute;

namespace ApplicationTests.Histories.GetDepartmentHistory;

public class GetDepartmentHistoryQueryHandlerTests
{
    private readonly IHistoryRepository _historyRepositoryMock;
    private readonly IDepartmentRepository _departmentRepositoryMock;
    private readonly GetDepartmentHistoryQueryHandler _handler;

    public GetDepartmentHistoryQueryHandlerTests()
    {
        _historyRepositoryMock = Substitute.For<IHistoryRepository>();
        _departmentRepositoryMock = Substitute.For<IDepartmentRepository>();
        _handler = new(_historyRepositoryMock, _departmentRepositoryMock);
    }

    
    [Fact]
    public async Task SuccessShouldBeCallRepository()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(Department.Create(Title.Create(string.Empty)));
        _historyRepositoryMock.GetDepartmentHistory(Arg.Any<DepartmentId>(), default)
            .Returns(Enumerable.Empty<History>());

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), default);

        //Assert
        await _departmentRepositoryMock.Received(1).Get(Arg.Any<DepartmentId>(), default);
        await _historyRepositoryMock.Received(1).GetDepartmentHistory(Arg.Any<DepartmentId>(), default);
    }

        [Fact]
    public async Task NotExistShouldBeReturnNotFoundError()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns((Department?)null);

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.HistoryDepartmentNotFoundError>();
    }

        [Fact]
    public async Task SuccessShouldBeReturnSuccessResult()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(Department.Create(Title.Create(string.Empty)));
        _historyRepositoryMock.GetDepartmentHistory(Arg.Any<DepartmentId>(), default)
            .Returns(Enumerable.Empty<History>());

        //Act
        var result = await _handler.Handle(new(new(Guid.NewGuid())), default);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
