namespace KatlaSport.Services.HospitalManagment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using KatlaSport.DataAccess;
    using KatlaSport.DataAccess.Hospital;

    internal sealed class DepartmentService : IDepartmentService
    {
        private readonly IHospitalContext _context;

        public DepartmentService(IHospitalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Department> CreateDepartmentAsync(DepartmentRequest request)
        {
            var dbDepartments = await _context.Departments.Where(d => (d.Name == request.Name)).ToArrayAsync();
            if (dbDepartments.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            var departments = Mapper.Map<DepartmentRequest, Department>(request);
            _context.Departments.Add(departments);

            await _context.SaveChangesAsync();

            return departments;
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var dbDepartments = await _context.Departments.Where(d => d.Id == departmentId).ToArrayAsync();
            if (dbDepartments.Length == 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            var dbChildren = await _context.Departments.Where(p => p.ParentId == departmentId).ToArrayAsync();

            foreach (Department d in dbChildren)
            {
                d.ParentId = dbDepartments[0].ParentId;
            }

            _context.Departments.Remove(dbDepartments[0]);
            await _context.SaveChangesAsync();
        }

        public async Task<DepartmentRequest> GetDepartmentAsync(int departmentId)
        {
            var dbDepartments = await _context.Departments.Where(d => d.Id == departmentId).ToArrayAsync();
            if (dbDepartments.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var departments = Mapper.Map<Department, DepartmentRequest>(dbDepartments[0]);
            return departments;
        }

        public async Task<List<DepartmentRequest>> GetDepartmentsAsync()
        {
            var dbDepartments = await _context.Departments.OrderBy(d => d.Id).ToListAsync();
            var departments = dbDepartments.Select(d => Mapper.Map<Department, DepartmentRequest>(d)).ToList();

            return departments;
        }

        public async Task<Department> UpdateDepartmentAsync(DepartmentRequest request,int departmentId)
        {
            var dbDepartments = await _context.Departments.Where(d => d.Name == request.Name && d.Id != departmentId).ToArrayAsync();
            if (dbDepartments.Length > 0)
            {
                throw new RequestedResourceHasConflictException("code");
            }

            dbDepartments = _context.Departments.Where(p => p.Id == request.Id).ToArray();
            if (dbDepartments.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbDepartment = dbDepartments[0];

            dbDepartment.Name = request.Name;
            dbDepartment.Description = request.Description;
            await _context.SaveChangesAsync();

            return dbDepartment;
        }
    }
}
