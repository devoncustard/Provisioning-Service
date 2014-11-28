using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BakeryAPI.Models;
using Provisioning_Service_Shared_Objects;

namespace BakeryAPI.Controllers
{
    public class ImageController : ApiController
    {
        static readonly IImageRepository repository = new ImageRepository();

        /*public IEnumerable<Images> GetAllImages()
        {
            return repository.GetAll();
        }
        */
        [HttpGet]
        public Images GetImageDetails([FromUri]ImageRequest request)
        {
            return repository.GetImageDetails(request);
        }
        public void DeleteImage(string name)
        {
            repository.DeleteImage(name);
        }
        public HttpResponseMessage AddImage(Image image)
        {
            image.Approved = false;
            var i=repository.AddImage(image);
            var response = Request.CreateResponse<Images>(HttpStatusCode.Created, i);
            return response;
        }
        public void  ApproveImage(string id)
        {
            repository.ApproveImage(id);
            return;
        }
    }


}
