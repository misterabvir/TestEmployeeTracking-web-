using Client.Models;

namespace Client.Extensions;

public static class DepartmentExtensions
{

    internal static List<Department> ToLineList(this IEnumerable<Department> tree, Department without = null)
    {
        List<Department> departments = new();
        foreach (var department in tree)
        {
            if (department != without)
            {
                departments.Add(department);

                if (department.Children is not null)

                    departments.AddRange(department.Children.Where(department=>department!=without).ToLineList(without));
            }
        }
        return departments;
    }



    internal static List<Department> FromResponse(this IEnumerable<DepartmentModel> list,
    string parentId = "")
    {
        var currents = list.Where(d => d.ParentId == parentId);
        List<Department> departments = new();
        foreach (var item in currents)
        {
            departments.Add(new()
            {
                Id = item.Id,
                Title = item.Title,
                ParentId = parentId,
                Children = FromResponse(list, item.Id)
            });
        }
        return departments;
    }
}
