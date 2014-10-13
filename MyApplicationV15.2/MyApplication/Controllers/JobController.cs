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

namespace MyApplication.Controllers
{
    public class JobController : Controller
    {
        //
        // GET: /Job/
 
        private IJobRepository jobRepo;
        private IAccountRepository accountRepo;
        private IProfileRepository profileRepo;
        private INotificationRepository notiRepo;
        public JobController()
        {
            this.profileRepo = new ProfileRepository(new MVCEntities());
            this.jobRepo = new JobRepository(new MVCEntities());
            this.accountRepo = new AccountRepository(new MVCEntities());
            this.notiRepo = new NotificationRepository(new MVCEntities());
        }
        [HttpGet]
        public ActionResult PostJob()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostJob(Job job)
        {
            int TotalCredit;
            int reduceCredit = 1;
            bool checkNotiSkill=false;

            int JobCount = 0;
            string checkCategory = "";
          
          
           

            int uid = (int)Session["UidInProfile"];
            job.UserId = uid;
            string strDate = DateTime.Now.ToString("MM/dd/yy H:mm:ss");
            System.Diagnostics.Debug.Write("strDate " + strDate);
            job.Date = strDate;
            job.Status = true;
            //string strDate = DateTime.Now.Date;
            MVCEntities db = new MVCEntities();
            var member = db.Members.SingleOrDefault(u => u.UserId.Equals(uid));
            ////deal with budget//////

            ///// deal with Credit////////
            System.Diagnostics.Debug.Write("Credit " + member.Credit);

            TotalCredit = member.Credit;
            TotalCredit = TotalCredit - reduceCredit;

            //  System.Diagnostics.Debug.Write("TotalCredit " + TotalCredit);

            if (job.finalBudget <= job.InitialBudget)
            {
                ModelState.AddModelError("", "final budget should more than initial budget!");

            }
            else
            {

                if (TotalCredit == -1)
                {
                    ModelState.AddModelError("", "Credit is equal to zero So, can not post job");


                }
                else
                {
                    if (member != null)
                    {
                        member.Credit = TotalCredit;
                        accountRepo.UpdateUser(member);
                        accountRepo.Save();
                    }
                    // IF USER DOES NOT INPUT OTHER SKILL//
                    if (job.Other == null)
                    {
                        job.Other = "Not Identify";
                    }


                    jobRepo.InsertUser(job);
                    jobRepo.Save();

                    ///// Notification Here///

                    var getUser = db.Jobs.SingleOrDefault(u => u.UserId.Equals(uid)&&u.Date.Equals(job.Date));
                   // System.Diagnostics.Debug.Write("1 " + getUser.Other.TrimEnd() + " 2 " + getUser.UserId + " 3 " + getUser.JobId);
                    //////// check match skill///////////////

                    var skillJobList = db.Jobs.SingleOrDefault(d => d.Date.Equals(job.Date));
                    var userList = from user in profileRepo.GetUsers() select user;
                    var users = new List<MyApplication.Profile>();
                    if (userList.Any())
                    {
                        foreach (var user in userList)
                        {

                            if (user.UserType.Equals("Freelancer          "))
                            {
                               // System.Diagnostics.Debug.Write(user.Tester + " , " +user.UserId+" , "+checkTester);
                             
                                // else if here //

                               if (user.Java)
                                {
                                    if (user.Java.Equals(skillJobList.Java))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book1!" + user.UserId);
                                }
                                 if (user.Javascript)
                                {
                                    if (user.Javascript.Equals(skillJobList.Javascript))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book2!" + user.UserId);
                                }
                                 if (user.C)
                                {
                                    if (user.C.Equals(skillJobList.C))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book3!" + user.UserId);
                                }
                                 if (user.Cplus)
                                {
                                    if (user.Cplus.Equals(skillJobList.Cplus))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book4!" + user.UserId);
                                }
                                 if (user.Csharp)
                                {
                                    if (user.Csharp.Equals(skillJobList.Csharp))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book5!" + user.UserId);
                                }
                                 if (user.PHP)
                                {
                                    if (user.PHP.Equals(skillJobList.PHP))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book6!" + user.UserId);
                                }
                                 if (user.Objective_C)
                                {
                                    if (user.Objective_C.Equals(skillJobList.Objective_C))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book7!" + user.UserId);
                                }
                                 if (user.Python)
                                {
                                    if (user.Python.Equals(skillJobList.Python))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book8!" + user.UserId);
                                }
                                 if (user.ASP_NET)
                                {
                                    if (user.ASP_NET.Equals(skillJobList.ASP_NET))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book9!" + user.UserId);
                                }
                                 if (user.Ruby)
                                {
                                    if (user.Ruby.Equals(skillJobList.Ruby))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book10!" + user.UserId);
                                }
                                 if (user.SQL)
                                {
                                    if (user.SQL.Equals(skillJobList.SQL))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book11!" + user.UserId);
                                }
                                 if (user.Html)
                                {
                                    if (user.Html.Equals(skillJobList.Html))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book12!" + user.UserId);
                                }
                                 if (user.Css)
                                {
                                    if (user.Css.Equals(skillJobList.Css))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book13!" + user.UserId);
                                }
                                 if (user.Android)
                                {
                                    if (user.Android.Equals(skillJobList.Android))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book14!" + user.UserId);
                                }
                                 if (user.XML)
                                {
                                    if (user.XML.Equals(skillJobList.XML))
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book15!" + user.UserId);
                                }
                                
                                if (user.WebDeveloper)
                                {
                                    checkCategory = "Web Developer";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book1!" + user.UserId);
                                    }                                                                      
                                }
                                if (user.Tester)
                                {
                                    checkCategory = "Tester";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book2!" + user.UserId);
                                    }
                                }
                                if (user.MobileDeveloper)
                                {
                                    checkCategory = "Mobile Devolpoer";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book3!" + user.UserId);
                                    }
                                }
                                if (user.SystemDesign)
                                {
                                    checkCategory = "System & Graphic design";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book4!" + user.UserId);
                                    }
                                }
                                if (user.SystemAnalyst)
                                {
                                    checkCategory = "System Analyst";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book5!" + user.UserId);
                                    }
                                }
                                if (user.ProjectManager)
                                {
                                    checkCategory = "Project Manager";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book6!" + user.UserId);
                                    }
                                }
                                if (user.GameDeveloper)
                                {
                                    checkCategory = "Game Developer";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book7!" + user.UserId);
                                    }
                                }
                                if (user.Marketing)
                                {
                                    checkCategory = "Marketing";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book8!" + user.UserId);
                                    }
                                }
                                if (user.OthersJob)
                                {
                                    checkCategory = "Others";
                                    if (checkCategory.Equals(job.Category))
                                    {
                                        checkNotiSkill = true;
                                        System.Diagnostics.Debug.Write("This is a book9!" + user.UserId);
                                    }
                                }

                                 if (checkNotiSkill)
                                 {
                                 
                                       System.Diagnostics.Debug.Write("slot1 ");
                                      /* Notification notificationSkill = new Notification();
                                       notificationSkill.JobId = getUser.JobId;
                                       notificationSkill.EmployerId = getUser.UserId;
                                       notificationSkill.FreelancerId = user.UserId;
                                       notificationSkill.EmployerMsg = "http://localhost:3883/DisplayJob/ShowDetailCategoryJob?jid" + getUser.JobId;
                                       notificationSkill.Status = false;
                                       notificationSkill.Date = job.Date;
                                       notificationSkill.Type = "Skill";*/

                                       Notification notificationSkill = new Notification();


                                       notificationSkill.FreelancerId = uid;
                                       notificationSkill.EmployerMsg = "I need your skill to do this project";
                                       notificationSkill.EmployerId = getUser.UserId;
                                       notificationSkill.JobId = getUser.JobId;
                                       notificationSkill.Date = job.Date;
                                       notificationSkill.Status = false;

                                       notificationSkill.Type = "Skill";

                                
                                       System.Diagnostics.Debug.Write("1 " + getUser.JobId + " 2 " + (int)Session["UidInProfile"] + " " + getUser.UserId + " " + job.Date);
                                       notiRepo.InsertNotification(notificationSkill);
                                       notiRepo.Save();
                                    
                                     checkNotiSkill = false;
                                 }
                                 if (!checkNotiSkill && JobCount==0)
                                 {
                                     checkNotiSkill = false;
                                     JobCount = 1;
                                     System.Diagnostics.Debug.Write("Slot2");
                                     notifySkill(getUser.Other.TrimEnd(), getUser.UserId, getUser.JobId, getUser.Date);
                                 }
                                
                          
                                
                            }
                          //  System.Diagnostics.Debug.Write("checkNotiSkill " + checkNotiSkill);
                           
                           
                        }
                       
                       
                         
                     
                    }

                  


                    //////////////////////////////////////////
                   // notifySkill(getUser.Other.TrimEnd(),getUser.UserId,getUser.JobId,getUser.Date);

                    //////////////////////////
                    return RedirectToAction("ShowJob", "Job"); 
                }
            }



            return View();
        }
     
          
       [HttpGet]
        public ActionResult ShowJob()
        {
            int uid = (int)Session["UidInProfile"];
            using(MVCEntities db = new MVCEntities()){
           //    var JobList = from user in jobRepo.GetUsers() select user;
                var JobList = jobRepo.GetUsers().OrderByDescending(u=>u.JobId);
              
               var jobs = new List<MyApplication.Job>();
               if (JobList.Any())
               {
                   foreach (var job in JobList)
                   {

                       //space bar 10 times - -'' i'll fix this point later.
                       if (job.UserId.Equals(uid)&&job.Status)
                       {
                           jobs.Add(new MyApplication.Job()
                           {
                               JobId = job.JobId,
                               Title = job.Title,
                               InitialBudget = job.InitialBudget,
                               finalBudget = job.finalBudget,
                               Category = job.Category,
                               Date= job.Date
                              

                           });
                       }
                   }
               }
         

            return View(jobs);
           }
           
        }
       public ActionResult DeleteJob(int id)
       {
           try
           {
               int TotalCredit;
               int reduceCredit = 1;
               MVCEntities db = new MVCEntities();

               var delJob = db.Jobs.SingleOrDefault(u => u.JobId.Equals(id));
               var member = db.Members.SingleOrDefault(u => u.UserId.Equals(delJob.UserId));


               if (delJob != null)
               {
                   delJob.Status = false ;
                  ///////////////////////
                  TotalCredit= member.Credit;
                  TotalCredit = TotalCredit + reduceCredit;
                   member.Credit=TotalCredit;
                   if(TotalCredit==3){
                   
                   //do nothhing
                   }
                   else
                   {
                       accountRepo.UpdateUser(member);
                       accountRepo.Save();
                   }

                   jobRepo.UpdateUser(delJob);
                   jobRepo.Save();


                   //notification//
                   
                   //
                   return RedirectToAction("ShowJob","Job");
               }
           }
           catch {
               return View();
           }
           return View();
       }


       [HttpGet]
       public ActionResult GotoShowDetailCategoryJob(int jid)
       {
         System.Diagnostics.Debug.Write("jid is " + jid);

           return RedirectToAction("ShowDetailCategoryJob", "DisplayJob", new { jid = jid });
         //  return View("ShowDetailCategoryJob", JobModel);
       }
        /////////////////////////
        //service//
       public void notifySkill(string OtherSkills,int EmployerId,int JobId,string Date)
       {
      //     System.Diagnostics.Debug.Write("4 " + OtherSkills + " 5 " + EmployerId + " 6 " + JobId);
                
           bool checktxt = false;
           bool checkSkill = false;
           ////////////
            


           ///////////
           for (int index = 0; index < OtherSkills.Length; index++)
           {
               if (OtherSkills[index].Equals(','))
               {
                   checktxt = true;
               }
           }
           if (checktxt)
           {

               String[] EmArray = OtherSkills.Split(',');
               int EmLen = EmArray.Length;

               MVCEntities db = new MVCEntities();
               var usersList = profileRepo.GetUsers();
               var users = new List<MyApplication.Profile>();

               String[] FreeArray;
               if (usersList.Any())
               {
                   foreach (var user in usersList)
                   {
                       if (user.UserType == "Freelancer          " && user.Others != null)
                       {
                           // int d = 0;
                           FreeArray = user.Others.TrimEnd().Split(',');
                           int Freelen = FreeArray.Length;

                           for (int z = 0; z < EmLen; z++)
                           {
                               for (int x = 0; x < Freelen; x++)
                               {
                                   //  System.Diagnostics.Debug.Write("Value " + EmArray[z] + " and " + FreeArray[x]);

                                   //check skill match with job : return true
                                   if (EmArray[z] == FreeArray[x])
                                   {
                                       // do the notification
                                       checkSkill = true;
                                   }
                               }//end first for loop
                               // System.Diagnostics.Debug.Write(" Next ");
                           }// end second for loop
                       }//end if-else
                       //
                       if (checkSkill)
                       {
                          /* Notification notificationSkill = new Notification();
                           notificationSkill.JobId = JobId;
                           notificationSkill.EmployerId = EmployerId;
                           notificationSkill.FreelancerId = user.UserId;
                           notificationSkill.EmployerMsg = "http://localhost:3883/DisplayJob/ShowDetailCategoryJob?jid" + JobId;
                           notificationSkill.Status = false;
                           notificationSkill.Date = Date;
                           notificationSkill.Type = "Skill";*/
                           Notification notificationSkill = new Notification();


                           notificationSkill.FreelancerId = user.UserId;
                           notificationSkill.EmployerMsg = "I need your skill to do this project";
                           notificationSkill.EmployerId = EmployerId;
                           notificationSkill.JobId = JobId;
                           notificationSkill.Date = Date;
                           notificationSkill.Status = false;

                           notificationSkill.Type = "Skill";

                           System.Diagnostics.Debug.Write("Slot1");
                           notiRepo.InsertNotification(notificationSkill);
                           notiRepo.Save();
                           checkSkill = false;

                       }

                       //
                   }// end foreach
               }//end if-else
           }//end if-else
           else
           {
               if (OtherSkills.Length!=0)
               {
                   string EmTxt = OtherSkills;

                   MVCEntities db = new MVCEntities();
                   var usersList = profileRepo.GetUsers();
                   var users = new List<MyApplication.Profile>();

                   String[] FreeArray;
                   if (usersList.Any())
                   {
                       foreach (var user in usersList)
                       {
                           if (user.UserType == "Freelancer          " && user.Others != null)
                           {
                               System.Diagnostics.Debug.Write("Name " + user.UserId);
                               FreeArray = user.Others.TrimEnd().Split(',');
                               int Freelen = FreeArray.Length;
                               for (int i = 0; i < FreeArray.Length; i++)
                               {
                                   if (FreeArray[i].Equals(EmTxt))
                                   {
                                       System.Diagnostics.Debug.Write("Slot2" + user.UserId);
                                       // do the notification

                                       checkSkill = true;
                                   }

                               }
                           }
                           //

                           if (checkSkill)
                           {
                               Notification notificationSkill = new Notification();
                               notificationSkill.JobId = JobId;
                               notificationSkill.EmployerId = EmployerId;
                               notificationSkill.FreelancerId = user.UserId;
                               notificationSkill.EmployerMsg = "http://localhost:3883/DisplayJob/ShowDetailCategoryJob?jid" + JobId;
                               notificationSkill.Status = false;
                               notificationSkill.Date = Date;
                               notificationSkill.Type = "Skill";
                               notiRepo.InsertNotification(notificationSkill);
                               notiRepo.Save();
                               checkSkill = false;
                           }

                           //
                       }//end foreach
                   }
                   //do something
               }
               else
               {

               }
           }

       }

       [HttpGet]
       public ActionResult showJobRequest(int FreelanceId)
       {
           System.Diagnostics.Debug.Write("Slot2" + FreelanceId);
           int uid = (int)Session["UidInProfile"];
           using (MVCEntities db = new MVCEntities())
           {
               //    var JobList = from user in jobRepo.GetUsers() select user;
               var JobList = jobRepo.GetUsers().OrderByDescending(u => u.JobId);

               var jobs = new List<MyApplication.Job>();
               if (JobList.Any())
               {
                   foreach (var job in JobList)
                   {

                       //space bar 10 times - -'' i'll fix this point later.
                       if (job.UserId.Equals(uid) && job.Status)
                       {
                           jobs.Add(new MyApplication.Job()
                           {
                               JobId = job.JobId,
                               Title = job.Title,
                               InitialBudget = job.InitialBudget,
                               finalBudget = job.finalBudget,
                               Category = job.Category,
                               Date = job.Date


                           });
                       }
                   }
               }

               ViewBag.FreelanceId = FreelanceId;
               return View(jobs);
           }

       }

       [HttpPost]
       public ActionResult showJobRequest(int FreeId,int JobId)

       {
           System.Diagnostics.Debug.Write("Credit " + FreeId + " " + JobId + " " + Session["UidInProfile"]);
           NotificationController noti = new NotificationController();
           noti.FreeNotificationJob(FreeId,(int)Session["UidInProfile"],JobId);
         

           return RedirectToAction("showJobRequest", "Job");
       }
        //////////service//////////////
      


        

    }
}


