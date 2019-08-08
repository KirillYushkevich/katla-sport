using KatlaSport.Services.HospitalManagment;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/department")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    [SwaggerResponseRemoveDefaults]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of departments.", Type = typeof(DepartmentRequest[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetDepartments()
        {
            var companys = await _departmentService.GetDepartmentsAsync();
            return Ok(companys);
        }

        [HttpGet]
        [Route("{departmentId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a department.", Type = typeof(DepartmentRequest))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetCompany(int departmentId)
        {
            var company = await _departmentService.GetDepartmentAsync(departmentId);
            return Ok(company);
        }

        [HttpPost]
        [Route()]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new department.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddCompany([FromBody] DepartmentRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = await _departmentService.CreateDepartmentAsync(createRequest);
            var location = string.Format("/api/department/{0}", department.Id);
            return Created<DepartmentRequest>(location, createRequest);
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existed department.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateDepartment([FromUri] int id, [FromBody] DepartmentRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _departmentService.UpdateDepartmentAsync(updateRequest,id);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed department.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteDepartment([FromUri] int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}