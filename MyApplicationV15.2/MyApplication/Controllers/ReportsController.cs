using MyApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyApplication.Controllers
{
    public class ReportsController : Controller
    {
        DataClasses objData;

        public ReportsController()
        {
            objData = new DataClasses();
        }

        //
        // GET: /Reports/

        public ActionResult Index()
        {
            var files = objData.GetFiles();
          
            return PartialView(files);
        }


        public FileResult Download(string id)
        {
            int fid = Convert.ToInt32(id);
            var files = objData.GetFiles();
            string filename = (from f in files
                               where f.FileId == fid
                               select f.FilePath).First();
           
            string contentType = "application/pdf";
            //Parameters to file are
            //1. The File Path on the File Server
            //2. The content type MIME type
            //3. The parameter for the file save by the browser
            return File(filename, contentType, "Report.pdf");
        }

    }
}
