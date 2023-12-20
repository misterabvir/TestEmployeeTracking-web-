namespace UI.Services.Abstractions;

public interface IDepartmentService
{
    Task<DepartmentResponse?> GetById(Guid id);
    Task<IEnumerable<DepartmentResponse?>> Get();
    Task<DepartmentResponse?> Create(string title, Guid? parentId = null);
    Task<DepartmentResponse?> ChangeTitle(Guid departmenId, string title);
    Task<DepartmentResponse?> SetParent(Guid departmenId, Guid? parentId);
    Task Delete(Guid departmenId);
}
