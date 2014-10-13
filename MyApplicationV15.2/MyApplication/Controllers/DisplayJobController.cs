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

namespace MyApplication.Controllers
{
    public class DisplayJobController : Controller
    {
        private IJobRepository jobRepo;
        private IAccountRepository accountRepo;
        private IProjectRepository projRepo;

        public DisplayJobController()
        {
            this.accountRepo = new AccountRepository(new MVCEntities());
            this.jobRepo = new JobRepository(new MVCEntities());
            this.projRepo = new ProjectRepository(new MVCEntities());
        }

        public ActionResult WebDev(int? page)
        {
         
            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("Web Developer            ") && user.Status.Equals(true))
                    {
                        var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                        ViewBag.Name = Name.Username;
                        ViewBag.Head = "Web Developer";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }
          
            int pageSize = 20;
            int pageNumber = (page ?? 1);
         
            return View("CategoryJob",users.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Tester(int? page )
        {


            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("Tester                   ") && user.Status.Equals(true))
                    {
                        var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                        ViewBag.Name = Name.Username;
                        ViewBag.Head = "Tester";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View("CategoryJob", users.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult MobileDev(int? page)
        {
            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("Mobile Devolpoer         ") && user.Status.Equals(true))
                    {
                        var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                        ViewBag.Name = Name.Username;
                        ViewBag.Head = "Mobile Devolpoer";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View("CategoryJob", users.ToPagedList(pageNumber, pageSize));

           
            
        }
        public ActionResult SystemDesign(int? page)
        {
          /*  using (MVCEntities db = new MVCEntities())
            {
                var JobList = from job in jobRepo.GetUsers() select job;

                var jobs = new List<MyApplication.Job>();
                if (JobList.Any())
                {
                    foreach (var job in JobList)
                    {

                        //space bar 10 times - -'' i'll fix this point later.
                        if (job.Category.Equals("System & Graphic design  ") && job.Status.Equals(true))
                        {
                            var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(job.UserId));
                            ViewBag.Name = Name.Username;
                            ViewBag.Head = "System & Graphic design";
                            jobs.Add(new MyApplication.Job()
                            {


                                Title = job.Title,
                                InitialBudget = job.InitialBudget,
                                finalBudget = job.finalBudget,
                                Category = job.Category,
                                JobId = job.JobId,
                                Date = job.Date


                            });
                        }
                    }
                }

                return View("CategoryJob", jobs);
            }*/
            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("System & Graphic design  ") && user.Status.Equals(true))
                    {
                        var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                        ViewBag.Name = Name.Username;
                        ViewBag.Head = "System & Graphic design";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View("CategoryJob", users.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SystemAnalyst(int? page)
        {

            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("System Analyst           ") && user.Status.Equals(true))
                    {
                        var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                        ViewBag.Name = Name.Username;
                        ViewBag.Head = "System Analyst";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View("CategoryJob", users.ToPagedList(pageNumber, pageSize));
            
        }
        public ActionResult ProjectManager(int? page)
        {
         

            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("Project Manager          ") && user.Status.Equals(true))
                    {
                        var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                        ViewBag.Name = Name.Username;
                        ViewBag.Head = "Project Manager";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View("CategoryJob", users.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult GameDev(int? page)
        {
            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("Game Developer           ") && user.Status.Equals(true))
                        {
                            var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                            ViewBag.Name = Name.Username;
                            ViewBag.Head = "Game Developer";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View("CategoryJob", users.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Marketing(int? page)
        {
           
            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("Marketing                ") && user.Status.Equals(true))
                    {

                        var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                        ViewBag.Name = Name.Username;
                        ViewBag.Head = "Marketing";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View("CategoryJob", users.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Others(int? page)
        {
           /* using (MVCEntities db = new MVCEntities())
            {
                var JobList = from job in jobRepo.GetUsers() select job;

                var jobs = new List<MyApplication.Job>();
                if (JobList.Any())
                {
                    foreach (var job in JobList)
                    {

                        //space bar 10 times - -'' i'll fix this point later.
                        if (job.Category.Equals("Others                   ") && job.Status.Equals(true))
                        {
                            var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(job.UserId));
                            ViewBag.Name = Name.Username;
                            ViewBag.Head = "Others";
                            jobs.Add(new MyApplication.Job()
                            {


                                Title = job.Title,
                                InitialBudget = job.InitialBudget,
                                finalBudget = job.finalBudget,
                                Category = job.Category,
                                JobId = job.JobId,
                                Date = job.Date
                               


                            });
                        }
                    }
                }


                return View("CategoryJob", jobs);
            }*/


            var userList = from user in jobRepo.GetUsers() select user;
            var users = new List<MyApplication.Job>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    if (user.Category.Equals("Others                   ") && user.Status.Equals(true))
                    {
                        var Name = accountRepo.GetUsers().FirstOrDefault(u => u.UserId.Equals(user.UserId));
                        ViewBag.Name = Name.Username;
                        ViewBag.Head = "Others";
                        users.Add(new MyApplication.Job()
                        {
                            Title = user.Title,
                            InitialBudget = user.InitialBudget,
                            finalBudget = user.finalBudget,
                            Category = user.Category,
                            JobId = user.JobId,
                            Date = user.Date
                        });
                    }
                }
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            return View("CategoryJob", users.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult ShowDetailCategoryJob(int jid)
        {

             try
                    {
                        System.Diagnostics.Debug.Write("This is spartan " + Session["UserType"]);
                     
                        var JobDetail = jobRepo.GetUsers().FirstOrDefault(u=>u.JobId == jid);

                        if (!JobDetail.Status)
                        {
                               System.Diagnostics.Debug.Write("DRAGON" + jid);
                               ViewBag.AvailableJob = "Not Now";
                        }

                        var JobModel = new MyApplication.Job(){
                        

                                    Title  = JobDetail.Title,
                                    Android = JobDetail.Android,
                                    ASP_NET = JobDetail.ASP_NET,


                                    InitialBudget = JobDetail.InitialBudget,
                                    finalBudget = JobDetail.finalBudget,
                             
                                    C = JobDetail.C,
                                    Category = JobDetail.Category,
                                    Cplus = JobDetail.Cplus,
                                    Csharp= JobDetail.Csharp,
                                      Css = JobDetail.Css,
                                        Description= JobDetail.Description,
                                          Html= JobDetail.Html,
                                            Java= JobDetail.Java,
                                              Javascript= JobDetail.Javascript,
                                                Objective_C= JobDetail.Objective_C,
                                                PHP= JobDetail.PHP,
                                                Python= JobDetail.Python,
                                                Ruby= JobDetail.Ruby,
                                                SQL= JobDetail.SQL,
                                                XML= JobDetail.XML,   
                                                Other=JobDetail.Other,
                                    Date = JobDetail.Date,
                                   
                                 
                        };
                        try
                        {

                            System.Diagnostics.Debug.Write("xxx40" + jid);
                            if (Session["UserType"].Equals("Freelancer          "))
                            {

                               
                                ViewBag.UserType = "Freelancer          ";
                            }
                            else
                            {
                                System.Diagnostics.Debug.Write("xxx41" + jid);
                            }
                        }
                        catch {
                            System.Diagnostics.Debug.Write("xxx42" + jid);
                        }
                        Session["JobId"] = jid;
                        return View("ShowDetailCategoryJob",JobModel);
                    }
                    catch
                    {
                       // return RedirectToAction("Index", "Home");
                        return View();
                    }

                }


        [HttpGet]
        public ActionResult ShowJobNotification(int jid)
        {

            try
            {

                int FreelancerId = (int)Session["UidInProfile"];

                ViewBag.CheckConfirm = false;
                var JobDetail = jobRepo.GetUsers().FirstOrDefault(u => u.JobId == jid);
               try
                {
                    var GetCheckConfirm = projRepo.GetUsers().FirstOrDefault(c => c.FreelancerId.Equals(FreelancerId) && c.JobId.Equals(jid));
                    System.Diagnostics.Debug.Write(" I am yours0" + GetCheckConfirm.CheckConfirm);
                    if (GetCheckConfirm.CheckConfirm)
                    {
                        ViewBag.CheckConfirm = GetCheckConfirm.CheckConfirm;
                    }
                    
                }
                catch {
                    System.Diagnostics.Debug.Write("I am yours");
                }
               
                 
                

                if (!JobDetail.Status)
                {
                    System.Diagnostics.Debug.Write("DRAGON" + jid);
                    ViewBag.AvailableJob = "Not Now";
                }

                var JobModel = new MyApplication.Job()
                {


                    Title = JobDetail.Title,
                    Android = JobDetail.Android,
                    ASP_NET = JobDetail.ASP_NET,


                    InitialBudget = JobDetail.InitialBudget,
                    finalBudget = JobDetail.finalBudget,

                    C = JobDetail.C,
                    Category = JobDetail.Category,
                    Cplus = JobDetail.Cplus,
                    Csharp = JobDetail.Csharp,
                    Css = JobDetail.Css,
                    Description = JobDetail.Description,
                    Html = JobDetail.Html,
                    Java = JobDetail.Java,
                    Javascript = JobDetail.Javascript,
                    Objective_C = JobDetail.Objective_C,
                    PHP = JobDetail.PHP,
                    Python = JobDetail.Python,
                    Ruby = JobDetail.Ruby,
                    SQL = JobDetail.SQL,
                    XML = JobDetail.XML,
                    Other = JobDetail.Other,
                    Date = JobDetail.Date,


                };
         
               /* try
                {
                   
                    if (Session["UserType"].Equals("Freelancer          "))
                    {
                        ViewBag.UserType = "Freelancer          ";
                    }
                    else
                    {

                    }
                }
                catch
                {

                }*/
                Session["JobId"] = jid;
                return View("ShowJobNotification", JobModel);
            }
            catch
            {
                // return RedirectToAction("Index", "Home");
                return View();
            }

        }


    }
}
