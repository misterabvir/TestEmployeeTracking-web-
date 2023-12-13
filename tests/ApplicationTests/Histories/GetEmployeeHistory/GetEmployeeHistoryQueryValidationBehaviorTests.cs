using ApplicationCore.Histories.Queries.GetEmployeeHistory;
using FluentAssertions;
using NSubstitute;
using MediatR;
using Domain.Common;
using ApplicationCore.Histories.Responses;

namespace ApplicationTests.Histories.GetEmployeeHistory;

public class GetEmployeeHistoryQueryValidationBehaviorTests
{
    private readonly RequestHandlerDelegate<Result<IEnumerable<HistoryResultResponse>>> _next;
    private readonly GetEmployeeHistoryQueryHandlerBehavior _behavior;

    public GetEmployeeHistoryQueryValidationBehaviorTests()
    {
        _next = Substitute.For<RequestHandlerDelegate<Result<IEnumerable<HistoryResultResponse>>>>();
        _behavior = new();
    }

    [Fact]
    public async Task SuccessShouldBeCallHandlerMock()
    {
        //Arrange
       GetEmployeeHistoryQuery query = new(new(Guid.NewGuid()));

        //Act
        var result = await _behavior.Handle(query, _next, default);

        //Assert
        await _next.Received(1).Invoke();
    }

    [Fact]
    public async Task EmptyIdShouldBeReturnValidationError()
    {
        //Arrange
         GetEmployeeHistoryQuery query = new(new(Guid.Empty));

        //Act
        var result = await _behavior.Handle(query, _next, default);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Core.Errors.HistoryValidationError>();
    }
}