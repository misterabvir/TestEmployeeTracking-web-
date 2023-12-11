using Entities.Departments;
using Entities.Departments.ValueObjects;
using Entities.Employees;
using Entities.Employees.ValueObjects;
using Persistence.Common;
using Persistence.Repositories;

namespace RepositoriesTest;

public class EmployeeRepositoryTest
{
    private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeDb";


    private readonly DbService _service;
    private readonly EmployeeRepository _employeeRepository;
    private readonly DepartmentRepository _departmentRepository;
    public EmployeeRepositoryTest()
    {
        _service = new DbService(ConnectionString);
        _employeeRepository = new EmployeeRepository(_service);
        _departmentRepository = new DepartmentRepository(_service);

    }

    private async Task<Employee> GetEmployee()
    {
        Title title = Title.Create("title");
        Department department = Department.Create(title);

        FirstName firstName = FirstName.Create("firstname");
        LastName lastName = LastName.Create("lastname");
        Employee employee = Employee.Create(lastName, firstName, department.Id);

        await _departmentRepository.Create(department, default);
        await _employeeRepository.Create(employee, default);

        return employee;
    }
    private Department GetDepartment()
    {
        Title title = Title.Create("title");
        return Department.Create(title);
    }


    [Fact]
    public async Task EmployeeCreateShouldBeWithoutException()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        
        try
        {
            //Act
            Employee expected = await GetEmployee();
        }
        catch (Exception exc)
        {
            //Assert
            Assert.Fail($"Unexpected Exception {exc.Message} {exc.StackTrace}");
        }
    }

    [Fact]
    public async Task EmployeeInDbShouldBeNotNullAndEqualEmployeeCreated()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Employee expected = await GetEmployee();

        //Act
        Employee? actual = await _employeeRepository.Get(expected.Id, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task EmployeeUpdateShouldBeChanged()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Employee expected = await GetEmployee();
        LastName lastName = LastName.Create("NewLastName");
        FirstName firstName = FirstName.Create("NewFirstName");
        expected.ChangePersonalData(lastName, firstName);
        
        //Act
        await _employeeRepository.Update(expected, default);
        Employee? actual = await _employeeRepository.Get(expected.Id, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task EmployeeGetAllShouldContainsCreated()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Employee expected = await GetEmployee();

        //Act
        var actual = await _employeeRepository.Get(default);

        //Assert
        Assert.NotNull(actual);
        Assert.Contains(expected, actual);
    }


    [Fact]
    public async Task EmployeeGetAllFromDepartmentShouldContainsCreated()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Employee expected = await GetEmployee();

        //Act
        var actual = await _employeeRepository.GetByDepartmentId(expected.DepartmentId, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Contains(expected, actual);
    }


    [Fact]
    public async Task EmployeeDeleteShouldBeRemoveInstanceFromDb()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Employee expected = await GetEmployee();

        //Act
        await _employeeRepository.Delete(expected.Id, default);
        var actual = await _employeeRepository.Get(expected.Id, default);

        //Assert
        Assert.Null(actual);
    }
}