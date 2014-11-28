using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using Provisioning_Service_Shared_Objects;



namespace BakeryAPI.Models
{
    public class ImageRepository :IImageRepository
    {
        //private DataContext db = new DataContext("Data Source = 10.52.24.153; Initial Catalog=Bakery; Integrated Security=False; User ID=Bakery;Password=bakery;");
        private DataContext db = new DataContext("Data Source = achilles; Initial Catalog=Bakery; Integrated Security=False; User ID=Bakery;Password=bakery;");

        public IEnumerable<Images> GetAllImages()
        {
            Table<Images> tblImages = db.GetTable<Images>();
            var images = from n in tblImages select n;
            return images;
        }
        public Images GetImageDetails(ImageRequest request)
        {
            Table<Images> tblImages=db.GetTable<Images>();
            var image=(from n in tblImages
                           where n.ImageType==request.ImageType
                           & n.Provider==request.Provider
                           & n.CommonName==request.CommonName
                           orderby n.BakedOn descending
                           select n).First();
            return image;
        }
        public void DeleteImage(string id)
        {
            Table<Images> tblImages = db.GetTable<Images>();
            var image = (from n in tblImages
                         where n.Id==id
                         select n).First();
            tblImages.DeleteOnSubmit(image);
            db.SubmitChanges();

        }
        public Images AddImage(Image image)
        {
            Table<Images> tblImages = db.GetTable<Images>();
            Images newimage = new Images();
            newimage.BakedOn = Convert.ToDateTime(image.BakedOn);
            newimage.Description = image.Description;
            newimage.Location = image.Location;
            newimage.Id = image.Id;
            newimage.OS_Family = (int)image.OS_Family;
            newimage.OS_Version = image.OS_Version;
            newimage.Provider = (int)image.Provider;
            newimage.CommonName = image.CommonName;
            newimage.ImageType = (int)image.ImageType;
            newimage.Approved = image.Approved;
            tblImages.InsertOnSubmit(newimage);
            db.SubmitChanges();
            return newimage;
        }
        public void ApproveImage(string id)
        {

            return;
        }
    }
}