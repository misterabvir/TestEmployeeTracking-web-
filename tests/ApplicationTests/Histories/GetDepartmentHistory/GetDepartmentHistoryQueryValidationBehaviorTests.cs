using ApplicationCore.Histories.Queries.GetDepartmentHistory;
using ApplicationCore.Histories.Responses;
using Core;
using Domain.Common;
using FluentAssertions;
using MediatR;
using NSubstitute;

namespace ApplicationTests.Histories.GetDepartmentHistory;

public class GetDepartmentHistoryQueryValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<IEnumerable<HistoryResultResponse>>> _next;
    private readonly GetDepartmentHistoryQueryBehavior _behavior;

    public GetDepartmentHistoryQueryValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<IEnumerable<HistoryResultResponse>>>>();
        _behavior = new();
    }

    [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {
        //Arrange
       GetDepartmentHistoryQuery query = new(new(Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(query, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task EmptyIdShouldBeReturnValidationError()
    {
        //Arrange
         GetDepartmentHistoryQuery query = new(new(Guid.Empty));

        //Act
        var result = await _behavior.Handle(query, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.HistoryValidationError>();
    }
}