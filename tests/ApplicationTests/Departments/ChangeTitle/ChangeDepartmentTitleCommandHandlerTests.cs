using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Departments.Commands.ChangeTitle;
using ApplicationCore.Departments.Responses;
using Domain.Common;
using Entities.Abstractions.Services;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace ApplicationTests.Departments.ChangeTitle;

public class ChangeDepartmentTitleCommandHandlerTests
{
    private readonly IDepartmentRepository _departmentRepositoryMock;
    private readonly IDepartmentService _departmentServiceMock;

    private readonly ChangeDepartmentTitleCommandHandler _handler;

    public ChangeDepartmentTitleCommandHandlerTests()
    {
        _departmentRepositoryMock = Substitute.For<IDepartmentRepository>();
        _departmentServiceMock = Substitute.For<IDepartmentService>();
        _handler = new(_departmentRepositoryMock, _departmentServiceMock);
    }

    
    [Fact]
    public async Task SuccessWithoutParentShouldBeCalRepository()
    {
        //Arrange
        _departmentRepositoryMock
            .Get(Arg.Any<DepartmentId>(), default)
            .Returns(Department.Create(DepartmentId.CreateUnique(), Title.Create("title")));

        _departmentServiceMock.ChangeTitle(Arg.Any<Department>(), Arg.Any<Title>()).Returns(Result.Success());

        //Act
        await _handler.Handle(new(new(Guid.NewGuid(), "title")), CancellationToken.None);

        //Assert
        await _departmentRepositoryMock.Received(1).Get(Arg.Any<DepartmentId>(), default);
        await _departmentRepositoryMock.Received(1).Update(Arg.Any<Department>(), default);
        _departmentServiceMock.Received(1).ChangeTitle(Arg.Any<Department>(), Arg.Any<Title>());
    }

        [Fact]
    public async Task DepartmentNotExistShouldBeCalReturnNotFoundError()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns((Department?)null);
        
        //Act
        var result = await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), Title: "title")), CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Core.Errors.DepartmentNotFoundError>();
    }

    [Fact]
    public async Task SuccessShouldBeCalReturnSuccessResult()
    {
        //Arrange
        _departmentRepositoryMock
            .Get(Arg.Any<DepartmentId>(), default)
            .Returns(Department.Create(DepartmentId.CreateUnique(), Title.Create("title")));
        _departmentServiceMock.ChangeTitle(Arg.Any<Department>(), Arg.Any<Title>()).Returns(Result.Success());

        //Act
        var result = await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), Title: "title")), CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<DepartmentResultResponse>();
    }
}
