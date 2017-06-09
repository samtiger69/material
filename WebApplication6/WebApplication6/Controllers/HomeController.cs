using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : BaseController
    {

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase[] file)
        {
            try
            {
                var response = new List<string>();
                if(file != null && file.Length > 0)
                {
                    foreach(var part in file)
                    {
                        if(part != null && part.ContentLength > 0)
                        {
                            byte[] toBeSavedInTheDatabase;
                            using (var reader = new BinaryReader(part.InputStream))
                            {
                                toBeSavedInTheDatabase = reader.ReadBytes((int)part.InputStream.Length);
                            }
                            ImageContext db = new ImageContext();
                            var imgId = db.UploadImage(toBeSavedInTheDatabase);
                            response.Add(String.Format("<img id='{0}'  onclick='deleteImage(this)' style='height: 400px; width: 400px' src=data:image/jpg;base64,{1} />", imgId, Convert.ToBase64String(toBeSavedInTheDatabase)));
                        }
                    }
                }
                return new JsonResult
                {
                    MaxJsonLength = int.MaxValue,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = ex.Message
                };
            }
        }

        [HttpPost]
        public string DeleteImage(int id)
        {
            try
            {
                ImageContext db = new ImageContext();
                var toBeDeleted = db.DeleteImage(id);
                if(toBeDeleted == 1)
                {
                    return String.Format("Image with Id: {0} has been deleted from the database", id);
                }
                return String.Format("No such image with Id: {0} was found in the database", id);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public ActionResult Index()
        {
            ImageContext db = new ImageContext();
            var model = db.GetImages();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangeCurrentCulture(int id)
        {
            //  
            // Change the current culture for this user.  
            //  
            CultureHelper.CurrentCulture = id;
            //  
            // Cache the new current culture into the user HTTP session.   
            //  
            Session["CurrentCulture"] = id;
            //  
            // Redirect to the same page from where the request was made!   
            //  
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Form()
        {
            return View();
        }
    }
}