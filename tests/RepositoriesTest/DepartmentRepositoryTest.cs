using Entities.Departments;
using Entities.Departments.ValueObjects;
using Persistence.Common;
using Persistence.Repositories;

namespace RepositoriesTest;

public class DepartmentRepositoryTest
{
    private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeDb";
    private readonly DepartmentRepository _departmentRepository;

    public DepartmentRepositoryTest()
    {
        _departmentRepository = new DepartmentRepository(new DbService(ConnectionString));
    }

    private async Task<Department> GetDepartment()
    {
        Title title = Title.Create("department");
        Title parentTitle = Title.Create("parent");

        Department parent = Department.Create(parentTitle);
        Department department = Department.Create(title, parent.Id);

        await _departmentRepository.Create(parent, default);
        await _departmentRepository.Create(department, default);
        return department;
    }

    [Fact]
    private async Task DepartmentCreatedWithoutException()
    {
        using var scope = TransactionFactory.CreateTransaction();
        try
        {
            //Act
            Department expected = await GetDepartment();
        }
        catch (Exception exc)
        {
            //Assert
            Assert.Fail($"Unexpected Exception {exc.Message} {exc.StackTrace}");
        }
    }


    [Fact]
    private async Task DepartmentCreatedShouldBeSavedInDb()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Department expected = await GetDepartment();
        
        //Act
        Department? actual = await _departmentRepository.Get(expected.Id, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    private async Task DepartmentUpdatedShouldBeChanged()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Department department = await GetDepartment();
        Title title = Title.Create("NewDepartmentTitle");
        Department expected = Department.Create(department.Id, title, department.ParentId);

        //Act
        await _departmentRepository.Update(expected, default);
        Department? actual = await _departmentRepository.Get(expected.Id, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    private async Task DepartmentGetAllShouldBeContainsCreated()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Department expected = await GetDepartment();

        //Act
        var actual = await _departmentRepository.Get(default);

        //Assert
        Assert.Contains(expected, actual);
    }


    [Fact]
    private async Task DepartmentGetByNameAndParentShouldBeEqualsCreated()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Department expected = await GetDepartment();

        //Act
        var actual = await _departmentRepository.GetByNameAndParentId(expected.Title, expected.ParentId, default);

        //Assert
        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
    }


    [Fact]
    private async Task DepartmentDeleteShouldBeRemovedFromDb()
    {
        using var scope = TransactionFactory.CreateTransaction();

        //Arrange
        Department expected = await GetDepartment();

        //Act
        await _departmentRepository.Delete(expected.Id, default);
        var actual = await _departmentRepository.Get(expected.Id, default);
        //Assert
        Assert.Null(actual);
    }
}
