using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyApplication.Models;
using System.Text;
using System.Data;
using MyApplication.Repository;
using System.Web.Security;
using System.IO;
using PagedList;
using System.Diagnostics;
using System.Web.Hosting;

namespace MyApplication.Controllers
{
    public class ProjectController : Controller
    {

        private IProjectRepository projectRepo;
        private INotificationRepository notiRepo;

        public ProjectController()
        {
            this.projectRepo = new ProjectRepository(new MVCEntities());
            this.notiRepo = new NotificationRepository(new MVCEntities());
        }
        [HttpPost]
        public void CreateProject(int JobId, int EmId, int FreeId)
        {
            Project project = new Project();
            project.EmployerId = EmId;
            project.FreelancerId = FreeId;

            System.Diagnostics.Debug.Write("JobId" + JobId);

            try
            {
                MVCEntities db = new MVCEntities();
                var EmName = db.Members.FirstOrDefault(u => u.UserId.Equals(EmId)).Name;
                var FreeName = db.Members.FirstOrDefault(u => u.UserId.Equals(FreeId)).Name;

                System.Diagnostics.Debug.Write(EmName + " and " + FreeName);

                project.EmployerName = EmName;
                project.FreelancerName = FreeName;
                project.JobId = JobId;
                project.CheckConfirm = true;

                projectRepo.InsertUser(project);
                projectRepo.Save();

                string strDate = DateTime.Now.ToString("MM/dd/yy H:mm:ss");

                Notification EmNoti = new Notification();
                EmNoti.JobId = JobId;
                EmNoti.EmployerId = EmId;
                EmNoti.FreelancerId = FreeId;
                EmNoti.FreelancerMsg = "I confirm your job";
                EmNoti.Date = strDate;
                EmNoti.Type = "Job";

                notiRepo.InsertNotification(EmNoti);
                notiRepo.Save();




                System.Diagnostics.Debug.Write("PassForProject");
                // return RedirectToAction("Index","Home");
            }
            catch
            {
                //  return RedirectToAction("Index", "Home");
            }

        }
        [HttpGet]
        public ActionResult showProjects()
        {

            int uid = (int)Session["UidInProfile"];
            System.Diagnostics.Debug.Write("asdascxascasd " + uid);
            var ProjectList = projectRepo.GetUsers();
            var Jobs = new List<MyApplication.Job>();
            var UserType = Session["UserType"];


            System.Diagnostics.Debug.Write("Freelencer");

            if (ProjectList.Any())
            {
                foreach (var proj in ProjectList)
                {

                    if (proj.FreelancerId.Equals(uid))
                    {
                        var x = proj.JobId;
                        var db = new MVCEntities();

                        var getJObId = db.Jobs.FirstOrDefault(u => u.JobId.Equals(x));

                        Jobs.Add(new MyApplication.Job()
                        {
                            JobId = getJObId.JobId,
                            Title = getJObId.Title,
                            InitialBudget = getJObId.InitialBudget,
                            finalBudget = getJObId.finalBudget,
                            Category = getJObId.Category,
                            Date = getJObId.Date




                        });

                    }
                    if (proj.EmployerId.Equals(uid))
                    {
                        var x = proj.JobId;
                        var db = new MVCEntities();

                        var getJObId = db.Jobs.FirstOrDefault(u => u.JobId.Equals(x));

                        Jobs.Add(new MyApplication.Job()
                        {
                            JobId = getJObId.JobId,
                            Title = getJObId.Title,
                            InitialBudget = getJObId.InitialBudget,
                            finalBudget = getJObId.finalBudget,
                            Category = getJObId.Category,
                            Date = getJObId.Date




                        });

                    }


                }

            }



            return View(Jobs);
        }

        public ActionResult progressOftheProject(int jobId)
        {
            ViewBag.files = Directory.EnumerateFiles(Server.MapPath("~/images_upload"));

            //show all image/////////////////////////////////////////////
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/images_upload"))
                              .Select(fn => "~/images_upload/" + Path.GetFileName(fn));
            /////////////////////////////////////////////////////////////

            // int uid = (int)Session["UidInProfile"];
            // System.Diagnostics.Debug.Write("asdascxascasd "+uid);
            var ProjectList = projectRepo.GetUsers();
            var proJs = new List<MyApplication.Project>();


            // System.Diagnostics.Debug.Write("Freelencer");

            if (ProjectList.Any())
            {
                foreach (var proj in ProjectList)
                {

                    if (proj.JobId.Equals(jobId))
                    {
                        ViewBag.EmployerName = proj.EmployerName;
                        ViewBag.JobId = jobId;

                        var db = new MVCEntities();

                        //  var getJObId = db.Projects.FirstOrDefault(u => u.JobId.Equals(x));

                        proJs.Add(new MyApplication.Project()
                        {
                            // EmployerName = proj.EmployerName,
                            JobId = proj.JobId,
                            FreelancerName = proj.FreelancerName,
                        });

                    }
                }
            }

            ViewBag.check = false;

            return View(proJs);
        }
        [HttpPost]
        public void Test(int jid)
        {


           /* string path = @"C:\" + jid;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            */
            string folder = jid.ToString();
            var path = Server.MapPath("~/AllProject/" + folder);
            var directory = new DirectoryInfo(path);
            if (directory.Exists == false)
            {
                directory.Create();
            }
        }
        [HttpPost]
        public void CallFile(int jid)
        {
            Process.Start("explorer.exe", HostingEnvironment.MapPath("~/files"));
        }

        [HttpPost]
        public ActionResult Uploads(IEnumerable<HttpPostedFileBase> files)
        {
            
                foreach (var file in files)
                {
                    try
                    {
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/files"), fileName);
                            file.SaveAs(path);
                        }
                    }
                    catch { }

                }
            
            return RedirectToAction("Index","Account");
        }
          [HttpPost]
        public ActionResult Downloads(bool check)
        {
            
            if (check)
            {
                ViewBag.check = check;
            }
           
            System.Diagnostics.Debug.Write("Download DIWA ");
         

            return RedirectToAction("progressOftheProject","Project");
        }
       
    }
}
