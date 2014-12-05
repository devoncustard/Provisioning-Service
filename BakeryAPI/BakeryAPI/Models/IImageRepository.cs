using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using Provisioning_Service_Shared_Objects;

namespace BakeryAPI.Models
{
    public interface IImageRepository
    {
        IEnumerable<Images> GetAllImages();
        Images GetImageDetails(ImageRequest request);
        void DeleteImage(string id);
        Images AddImage(Image image);
        void ApproveImage(string id);
    }
}
