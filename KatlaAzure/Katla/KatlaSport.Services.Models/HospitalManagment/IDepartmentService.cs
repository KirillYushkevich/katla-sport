namespace KatlaSport.Services.HospitalManagment
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using KatlaSport.DataAccess.Hospital;

    public interface IDepartmentService
    {
        Task<List<DepartmentRequest>> GetDepartmentsAsync();

        Task<DepartmentRequest> GetDepartmentAsync(int departmentId);

        Task<Department> CreateDepartmentAsync(DepartmentRequest request);

        Task<Department> UpdateDepartmentAsync(DepartmentRequest request, int departmentId);

        Task DeleteDepartmentAsync(int departmentId);
    }
}
