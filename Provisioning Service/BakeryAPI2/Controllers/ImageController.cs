using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Web.Http.Filters;
using System.Threading;
using PSSO;
using BakeryAPI2.Models;


namespace BakeryAPI2.Controllers
{
    public class ImageController : ApiController
    {
        static readonly IImageRepository repository = new ImageRepository();

        [Route("api/image/all")]
        [HttpGet]
        public IEnumerable<Images> GetAllImages()
        {
            return repository.GetAllImages();
        }
        


        [HttpGet]
        public Images GetImageDetails([FromUri]ImageRequest request)
        {
            return repository.GetImageDetails(request);
        }
        [Authorize(Roles="BakeryAdmins")]
        [HttpGet]
        public Images GetImageDetails([FromUri]ImageRequest request,bool latest)
        {
            return repository.GetImageDetails(request,latest);
        }



        [HttpDelete]
        [Authorize(Roles = "BakeryAdmins")]
        public void DeleteImage(string id)
        {
            repository.DeleteImage(id);
        }
        [HttpPost]
        [Authorize(Roles = "BakeryAdmins")]
        public HttpResponseMessage AddImage(Image image)
        {
            image.Approved = false;
            var i = repository.AddImage(image);
            var response = Request.CreateResponse<Images>(HttpStatusCode.Created, i);
            return response;
        }
        [HttpPut]
        [Authorize(Roles = "BakeryAdmins")]
        public void ApproveImage(string id)
        {
            repository.ApproveImage(id);
            return;
        }
    }


}