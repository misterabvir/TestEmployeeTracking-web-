using ApplicationCore.Abstractions.Repositories;
using ApplicationCore.Departments.Commands.Create;
using Core;
using Entities.Departments;
using Entities.Departments.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace ApplicationTests.Departments.Create;

public class CreateDepartmentCommandHandlerTests
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly CreateDepartmentCommandHandler _handler;
    private readonly CreateDepartmentCommand _command;

    public CreateDepartmentCommandHandlerTests()
    {
        _command = new(new(string.Empty, Guid.NewGuid()));
        _departmentRepository = Substitute.For<IDepartmentRepository>();
        _handler = new(_departmentRepository);
    }

    [Fact]
    public async Task SuccessWithParentShouldBeCalRepository()
    {
        //Arrange
        _departmentRepository.Get(Arg.Any<DepartmentId>(), default).Returns(
            Department.Create(DepartmentId.Create(
                _command.Request.ParentDepartmentId!.Value),
                Title.Create(_command.Request.Title)));

        _departmentRepository
            .GetByNameAndParentId(Arg.Any<Title>(), Arg.Any<DepartmentId>(), default)
            .Returns((Department?)null);

        //Act
        await _handler.Handle(_command, CancellationToken.None);

        //Assert
        await _departmentRepository.Received(1).Get(Arg.Any<DepartmentId>(), default);
        await _departmentRepository.Received(1).GetByNameAndParentId(Arg.Any<Title>(), Arg.Any<DepartmentId>(), default);
        await _departmentRepository.Received(1).Create(Arg.Any<Department>(), default);
    }


    [Fact]
    public async Task SuccessWithoutParentShouldBeCalRepository()
    {
        var command = _command with { Request = _command.Request with { ParentDepartmentId = null } };
        //Arrange
        _departmentRepository
            .GetByNameAndParentId(Arg.Any<Title>(), Arg.Any<DepartmentId>(), default)
            .Returns((Department?)null);

        //Act
        await _handler.Handle(command, CancellationToken.None);

        //Assert
        await _departmentRepository.Received(1).GetByNameAndParentId(Arg.Any<Title>(), Arg.Any<DepartmentId>(), default);
        await _departmentRepository.Received(1).Create(Arg.Any<Department>(), default);
    }

    [Fact]
    public async Task ParentIdIsNotNullButParentNotExistShouldBeFailWithParentDepartmentNotFoundError()
    {
        //Arrange
        _departmentRepository.Get(Arg.Any<DepartmentId>(), default).Returns((Department?)null);

        //Act
        var result = await _handler.Handle(_command, CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentParentNotFoundError>();
    }

    [Fact]
    public async Task DepartmentWithSameNameExistInRootShouldBeFailWithAlreadyExistError()
    {

        //Arrange
        var command = _command with { Request = _command.Request with { ParentDepartmentId = null } };
        DepartmentId departmentId = DepartmentId.CreateUnique();
        _departmentRepository
            .GetByNameAndParentId(Arg.Any<Title>(), null, default)
            .Returns(Department.Create(departmentId, Title.Create(command.Request.Title)));
        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentAlreadyExistError>();
    }

    [Fact]
    public async Task DepartmentWithSameNameInRootShouldBeFailWithAlreadyExistError()
    {
        //Arrange
        DepartmentId departmentId = DepartmentId.CreateUnique();

        _departmentRepository
            .Get(Arg.Any<DepartmentId>(), default)
            .Returns(Department.Create(Title.Create("parent")));
        _departmentRepository
            .GetByNameAndParentId(Arg.Any<Title>(), Arg.Any<DepartmentId>(), default)
            .Returns(Department.Create(departmentId, Title.Create(_command.Request.Title)));

        //Act
        var result = await _handler.Handle(_command, CancellationToken.None);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentAlreadyExistError>();
    }
}
