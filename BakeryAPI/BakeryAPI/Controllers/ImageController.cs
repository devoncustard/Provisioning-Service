using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Web.Http.Filters;
using System.Threading;

using BakeryAPI.Models;
using Provisioning_Service_Shared_Objects;

namespace BakeryAPI.Controllers
{
    
    public class ImageController : ApiController
    {
        static readonly IImageRepository repository = new ImageRepository();

        public IEnumerable<Images> GetAllImages()
        {
            return repository.GetAllImages();
        }
        
        
        
        [HttpGet]
        public Images GetImageDetails([FromUri]ImageRequest request)
        {
            return repository.GetImageDetails(request);
        }
        
        [HttpDelete]
        [Authorize]
        public void DeleteImage(string id)
        {
            repository.DeleteImage(id);
        }
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddImage(Image image)
        {
            image.Approved = false;
            var i=repository.AddImage(image);
            var response = Request.CreateResponse<Images>(HttpStatusCode.Created, i);
            return response;
        }
        [HttpPut]
        [Authorize]
        public void  ApproveImage(string id)
        {
            repository.ApproveImage(id);
            return;
        }
    }


}
