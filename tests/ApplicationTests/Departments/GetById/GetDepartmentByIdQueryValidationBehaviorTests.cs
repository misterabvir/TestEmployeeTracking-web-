using ApplicationCore.Departments.Queries.GetById;
using ApplicationCore.Departments.Responses;
using Core;
using Domain.Common;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace ApplicationTests.Departments.GetById;

public class GetDepartmentByIdQueryValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<DepartmentResultResponse>> _next;
    private readonly GetDepartmentByIdQueryHandlerBehavior _behavior;

    public GetDepartmentByIdQueryValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<DepartmentResultResponse>>>();
        _behavior = new();
    }

    [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {
        //Arrange
        GetDepartmentByIdQuery query = new(new(Guid.NewGuid()));
        _behavior.Handle(query, _next, default).ReturnsNull();

        //Act
        var result = await _behavior.Handle(query, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task EmptyIdShouldBeReturnValidationError()
    {
        //Arrange
         GetDepartmentByIdQuery query = new(new(Guid.Empty));

        //Act
        var result = await _behavior.Handle(query, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.DepartmentValidationError>();
    }
}