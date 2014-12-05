using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using PSSO;

namespace BakeryAPI2.Models
{
    public interface IImageRepository
    {
        IEnumerable<Images> GetAllImages();
        Images GetImageDetails(ImageRequest request);
        Images GetImageDetails(ImageRequest request,bool latest);
        void DeleteImage(string id);
        Images AddImage(Image image);
        void ApproveImage(string id);
    }
}
