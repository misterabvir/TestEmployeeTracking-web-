using ApplicationCore.Employees.Errors;
using ApplicationCore.Employees.Queries.GetById;
using ApplicationCore.Employees.Responses;
using Domain.Common;
using FluentAssertions;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;

namespace ApplicationTests.Employees.GetById;

public class EmployeeGetByIdQueryValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<EmployeeResultResponse>> _next;
    private readonly GetEmployeeByIdQueryHandlerBehavior _behavior;

    public EmployeeGetByIdQueryValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<EmployeeResultResponse>>>();

        _behavior = new();
    }

    [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {

        //Arrange
        GetEmployeeByIdQuery query = new(new(Guid.NewGuid()));
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
        GetEmployeeByIdQuery query = new(new(Guid.Empty));

        //Act
        var result = await _behavior.Handle(query, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Title.Should().Be(EmployeeErrors.ValidationError(new ValidationResult()).Title);
    }
}
