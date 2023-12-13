using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Departments.Commands.SetParent;
using ApplicationCore.Departments.Responses;
using Core;
using Domain.Common;
using Entities.Abstractions.Services;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace ApplicationTests.Departments.SetParent;

public class SetDepartmentParentCommandHandlerTests
{
    private readonly IDepartmentRepository _departmentRepositoryMock;
    private readonly IDepartmentService _departmentServiceMock;
    private readonly SetDepartmentParentCommandHandler _handler;

    public SetDepartmentParentCommandHandlerTests()
    {
        _departmentRepositoryMock = Substitute.For<IDepartmentRepository>();
        _departmentServiceMock = Substitute.For<IDepartmentService>();
        _handler = new SetDepartmentParentCommandHandler(_departmentRepositoryMock, _departmentServiceMock);
    }

    [Fact]
    public async Task SuccessWithParentIdInCommandAndEntityShouldBeCallRepository()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(departmentId: DepartmentId.Create(Guid.NewGuid()), title: Title.Create("title"), parentDepartmentId: DepartmentId.Create(Guid.NewGuid())));

        _departmentServiceMock.ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>())
            .Returns(Result.Success());

        //Act
        await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: Guid.NewGuid())), CancellationToken.None);

        //Assert
        await _departmentRepositoryMock.Received(2).Get(Arg.Any<DepartmentId>(), default);
        await _departmentRepositoryMock.Received(1).Update(Arg.Any<Department>(), default);
        _departmentServiceMock.Received(1).ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>());
    }

    [Fact]
    public async Task SuccessWithoutParenIdInCommandButWithParentIdInEntityShouldBeCallRepository()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(departmentId: DepartmentId.Create(Guid.NewGuid()), title: Title.Create("title"), parentDepartmentId: DepartmentId.Create(Guid.NewGuid())));

        _departmentServiceMock.ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>())
            .Returns(Result.Success());

        //Act
        await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: null)), CancellationToken.None);

        //Assert
        await _departmentRepositoryMock.Received(1).Get(Arg.Any<DepartmentId>(), default);
        await _departmentRepositoryMock.Received(1).Update(Arg.Any<Department>(), default);
        _departmentServiceMock.Received(1).ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>());
    }

    [Fact]
    public async Task SuccessWithParenIdInCommandButWithoutParentIdInEntityShouldBeCallRepository()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(departmentId: DepartmentId.Create(Guid.NewGuid()), title: Title.Create("title")));

        _departmentServiceMock.ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>())
            .Returns(Result.Success());

        //Act
        await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: Guid.NewGuid())), CancellationToken.None);

        //Assert
        await _departmentRepositoryMock.Received(2).Get(Arg.Any<DepartmentId>(), default);
        await _departmentRepositoryMock.Received(1).Update(Arg.Any<Department>(), default);
        _departmentServiceMock.Received(1).ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>());
    }


    [Fact]
    public async Task DepartmentNotExistShouldBeCalReturnNotFoundError()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns((Department?)null);

        //Act
        var result = await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: Guid.NewGuid())), CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentNotFoundError>();
    }

    [Fact]
    public async Task DepartmentParentAndCommandParentAreNullShouldBeCalReturnAlreadyRootError()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(departmentId: DepartmentId.Create(Guid.NewGuid()), title: Title.Create("title")));

        //Act
        var result = await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: null)), CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentAlreadyRootError>();
    }

    [Fact]
    public async Task CommandParentNotExistShouldBeCalReturnAlreadyRootError()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(departmentId: DepartmentId.Create(
                Guid.NewGuid()), title: Title.Create("title"), parentDepartmentId: DepartmentId.Create(Guid.NewGuid())),
            (Department?)null);

        //Act
        var result = await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: Guid.NewGuid())), CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentParentNotFoundError>();
    }

    [Fact]
    public async Task SuccessWithParentIdInCommandAndEntityShouldBeReturnSuccessResult()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(departmentId: DepartmentId.Create(Guid.NewGuid()), title: Title.Create("title"), parentDepartmentId: DepartmentId.Create(Guid.NewGuid())));

        _departmentServiceMock.ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>())
            .Returns(Result.Success());

        //Act
        var result = await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: Guid.NewGuid())), CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<DepartmentResultResponse>();
    }

    [Fact]
    public async Task SuccessWithoutParenIdInCommandButWithParentIdInEntityShouldBeReturnSuccessResult()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(departmentId: DepartmentId.Create(Guid.NewGuid()), title: Title.Create("title"), parentDepartmentId: DepartmentId.Create(Guid.NewGuid())));

        _departmentServiceMock.ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>())
            .Returns(Result.Success());

        //Act
        var result = await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: Guid.NewGuid())), CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<DepartmentResultResponse>();
    }

    [Fact]
    public async Task SuccessWithParenIdInCommandButWithoutParentIdInEntityShouldBeReturnSuccessResult()
    {
        //Arrange
        _departmentRepositoryMock.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(departmentId: DepartmentId.Create(Guid.NewGuid()), title: Title.Create("title")));

        _departmentServiceMock.ChangeParentDepartment(Arg.Any<Department>(), Arg.Any<DepartmentId>())
            .Returns(Result.Success());

        //Act
        var result = await _handler.Handle(new(new(DepartmentId: Guid.NewGuid(), ParentDepartmentId: Guid.NewGuid())), CancellationToken.None);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<DepartmentResultResponse>();
    }
}
