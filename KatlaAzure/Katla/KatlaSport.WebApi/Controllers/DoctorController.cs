using KatlaSport.Services.HospitalManagment;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.Web.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/doctor")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    [SwaggerResponseRemoveDefaults]
    public class DoctorController : ApiController
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService=doctorService ?? throw new ArgumentNullException(nameof(doctorService));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of doctors.", Type = typeof(DoctorRequest[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetDoctors()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            var accountants = await _doctorService.GetDoctorsAsync();
            return Ok(accountants);
        }

        [HttpGet]
        [Route("{doctorId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a doctor.", Type = typeof(DoctorRequest))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetDoctor(int doctorId)
        {
            var accountant = await _doctorService.GetDoctorAsync(doctorId);
            return Ok(accountant);
        }

        [HttpPost]
        [Route()]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new doctor.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddDoctor()
        {
            var httprequest = HttpContext.Current.Request;
            var file = httprequest.Files["Image"];
            var data = httprequest.Form["data"];
            var createRequest = JsonConvert.DeserializeObject<DoctorRequest>(data);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("webappstoragedotnet-imagecontainer");
            await blobContainer.CreateIfNotExistsAsync();
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var doctor = await _doctorService.CreateDoctorAsync(createRequest, blobContainer, file);
            var location = string.Format("/api/doctor/{0}", doctor.Id);
            return Created(location, doctor);
        }

        [HttpPut]
        [Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existed doctor.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateDoctor([FromUri] int id)
        {
            var httprequest = HttpContext.Current.Request;
            var file = httprequest.Files["Image"];
            var data = httprequest.Form["data"];
            var createRequest = JsonConvert.DeserializeObject<DoctorRequest>(data);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("webappstoragedotnet-imagecontainer");
            await blobContainer.CreateIfNotExistsAsync();
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            await _doctorService.UpdateDoctorAsync(createRequest, blobContainer, file, id);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed doctor.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteHive([FromUri] int id)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference("webappstoragedotnet-imagecontainer");
            await blobContainer.CreateIfNotExistsAsync();
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            await _doctorService.DeleteDoctorAsync(id, blobContainer);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}