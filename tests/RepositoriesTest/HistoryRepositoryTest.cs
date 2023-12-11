using Entities.Departments.ValueObjects;
using Entities.Departments;
using Persistence.Common;
using Persistence.Repositories;
using Entities.Histories;
using Entities.Employees.ValueObjects;
using Entities.Employees;

namespace RepositoriesTest;

public class HistoryRepositoryTest
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly DepartmentRepository _departmentRepository;
    private readonly HistoryRepository _historyRepository;
    private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeDb";
    public HistoryRepositoryTest()
    {
        DbService _service = new DbService(ConnectionString);

        _employeeRepository = new(_service);
        _departmentRepository = new(_service);
        _historyRepository = new(_service);
    }

    private async Task<History> GetHistoryRow()
    {
        Title title = Title.Create("title");
        Department department = Department.Create(title);

        FirstName firstName = FirstName.Create("firstname");
        LastName lastName = LastName.Create("lastname");
        Employee employee = Employee.Create(lastName, firstName, department.Id);

        await _departmentRepository.Create(department, default);
        await _employeeRepository.Create(employee, default);

        History history = History.Create(employee.Id, department.Id, DateOnly.FromDateTime(DateTime.UtcNow));
        await _historyRepository.Create(history, default);

        return history;
    }

    [Fact]
    public async Task HistoryCreatedWithoutException()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        try
        {
            //Act
            History history = await GetHistoryRow();
        }
        catch (Exception exc)
        {
            //Assert
            Assert.Fail($"Unexpected Exception {exc.Message} {exc.StackTrace}");
        }
    }

    [Fact]
    public async Task HistoryCreatedShouldBeSavedInDb()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        History expected = await GetHistoryRow();

        //Act
        History? actual = await _historyRepository.Get(expected.Id, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task HistoryGetByEmployeeIdShouldBeExist()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        History expected = await GetHistoryRow();

        //Act
        var actual = await _historyRepository.GetEmployeeHistory(expected.EmployeeId, default);

        //Assert
        Assert.Contains(expected, actual);
    }

    [Fact]
    public async Task HistoryGetByDepartmentIdShouldBeExist()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        History expected = await GetHistoryRow();

        //Act
        var actual = await _historyRepository.GetDepartmentHistory(expected.DepartmentId, default);

        //Assert
        Assert.Contains(expected, actual);
    }

    [Fact]
    public async Task HistoryNotOverShouldBeExist()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        History expected = await GetHistoryRow();

        //Act
        var actual = await _historyRepository.Get(expected.EmployeeId, expected.DepartmentId, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }


    [Fact]
    public async Task HistoryUpdateShouldBeChanged()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        History expected = await GetHistoryRow();
        expected.Complete(DateOnly.FromDateTime(DateTime.UtcNow));

        //Act
        await _historyRepository.Update(expected, default);
        var actual = await _historyRepository.Get(expected.Id, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }


    [Fact]
    public async Task HistoryDeleteShouldBeRemoved()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        History expected = await GetHistoryRow();

        //Act
        await _historyRepository.Delete(expected.Id, default);
        var actual = await _historyRepository.Get(expected.Id, default);

        //Assert
        Assert.Null(actual);
    }
}
