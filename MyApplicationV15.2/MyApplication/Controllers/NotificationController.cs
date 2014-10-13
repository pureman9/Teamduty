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

namespace MyApplication.Controllers
{
    public class NotificationController : Controller
    {
        private  INotificationRepository notiRepo;

        public NotificationController()
        {
            notiRepo = new NotificationRepository(new MVCEntities());
        }
        public ActionResult EmNotifications()
        {
            int uid = (int)Session["UidInProfile"];
            System.Diagnostics.Debug.Write("Pass " + uid );
            var NotiList = notiRepo.GetNotifications();
            var Notis = new List<MyApplication.Notification>();
            if (NotiList.Any())
            {
                foreach (var noti in NotiList)
                {

                    if (noti.EmployerId.Equals(uid)&& noti.FreelancerMsg!=null)
                    {
                        Notis.Add(new MyApplication.Notification()
                        {
                            NotiId = noti.NotiId,
                            JobId = noti.JobId,
                            FreelancerMsg = noti.FreelancerMsg,
                            EmployerId = noti.EmployerId,
                            Read = noti.Read,
                            Date = noti.Date,
                            Type = noti.Type,


                        });
                       
                    }
                }
            }

            return View(Notis);
        }
        public ActionResult FreeNotifications()
        {
            try
            {
                int uid = (int)Session["UidInProfile"];
                var NotiList = notiRepo.GetNotifications();
                var Notis = new List<MyApplication.Notification>();
                if (NotiList.Any())
                {
                    foreach (var noti in NotiList)
                    {

                        if (noti.FreelancerId.Equals(uid)&&noti.EmployerMsg!=null)
                        {
                            Notis.Add(new MyApplication.Notification()
                            {
                                NotiId = noti.NotiId,
                                JobId = noti.JobId,
                                EmployerMsg = noti.EmployerMsg,
                                FreelancerId = noti.FreelancerId,
                                Read = noti.Read,
                                Date = noti.Date,
                                Type = noti.Type,


                            });
                            ViewBag.Type = "skill";
                        }

                        /* if (noti.FreelancerId.Equals(uid)&&noti.Type.TrimEnd().Equals("Skill"))
                         {
                             Notis.Add(new MyApplication.Notification()
                             {
                                 NotiId = noti.NotiId,
                                 JobId = noti.JobId,
                                 EmployerMsg = noti.EmployerMsg,
                                 FreelancerId = noti.FreelancerId,
                                 Read = noti.Read,
                                 Date=noti.Date,
                            
                        
                             });
                             ViewBag.Type = "skill";
                         }
                       /*  else if (noti.FreelancerId.Equals(uid) && noti.Type.TrimEnd().Equals("Job"))
                         {
                        
                             Notis.Add(new MyApplication.Notification()
                             {
                                 NotiId = noti.NotiId,
                                 JobId = noti.JobId,
                                 EmployerMsg = noti.EmployerMsg,
                                 FreelancerId = noti.FreelancerId,
                                 Read = noti.Read,
                                 Date = noti.Date,

                             });
                             ViewBag.TypeJob = "job";
                         }*/

                    }

                }
                return View(Notis);
            }
            catch { }

            return View();
        }
        //request freelance to do the project
        [HttpGet]
        public void FreeNotificationJob(int uid,int EmployerId,int jobId)
        {
            string strDate = DateTime.Now.ToString("MM/dd/yy H:mm:ss");
            System.Diagnostics.Debug.Write("Pass " + uid + " " +EmployerId+ " " + jobId);
            // store in db here//
            Notification notificationSkill = new Notification();
            
       
            notificationSkill.FreelancerId = uid;
            notificationSkill.EmployerMsg = "I need you to do this project";
            notificationSkill.EmployerId = EmployerId;
            notificationSkill.JobId = jobId;
            notificationSkill.Date = strDate;
            notificationSkill.Status = false;
            
            notificationSkill.Type = "Job";
            System.Diagnostics.Debug.Write("Slot1");
            notiRepo.InsertNotification(notificationSkill);
            notiRepo.Save();
          //  return View();
        }
        [HttpGet]
        public ActionResult GotoShowDetailCategoryJob(int jid,int uid,string date,string type)
        {
            // check Read to true//
            System.Diagnostics.Debug.Write("Slotx "+date+"x"+type+"r");
            MVCEntities db = new MVCEntities();
            Notification editRead = db.Notifications.FirstOrDefault(j => j.JobId.Equals(jid) && j.FreelancerId.Equals(uid)&&j.Date.Equals(date));
            editRead.Read = true;
             Session["EmId"] = editRead.EmployerId;
            Session["FreeId"]  = editRead.FreelancerId;

            notiRepo.UpdateNotification(editRead);
            notiRepo.Save();
            // error around here 
            System.Diagnostics.Debug.Write("jid is " + jid);
            Session["JobId"] = jid;
           // return RedirectToAction("ShowDetailCategoryJob", "DisplayJob", new { jid = jid });  
            if(type.Equals("Skill     ")){
              
                //fuck
                Session["JobId"] = jid;
               // Session["UserType"] = "Freelancer          ";
              //  System.Diagnostics.Debug.Write("cat" + " " + Session["UserType"]+"sS");
                return RedirectToAction("ShowDetailCategoryJob", "DisplayJob", new { jid = jid });    
            }
            else
            {
                System.Diagnostics.Debug.Write("dog");

               // System.Diagnostics.Debug.Write("sus" +ViewBag.EmId);
                return RedirectToAction("ShowJobNotification", "DisplayJob", new { jid = jid });         
                  
            }

           // return RedirectToAction("ShowJobNotification", "DisplayJob", new { jid = jid });  
        }
        [HttpGet]
        public ActionResult ConfirmProject(int JobId,int EmId, int FreeId)
        {
            ProjectController project = new ProjectController();
            project.CreateProject(JobId,EmId, FreeId);
            /* Project project = new Project();
             project.EmployerId = EmId;
             project.FreelancerId = FreeId;

             try
             {
                 MVCEntities db = new MVCEntities();
                 var EmName = db.Members.FirstOrDefault(u => u.UserId.Equals(EmId)).Name;
                 var FreeName = db.Members.FirstOrDefault(u => u.UserId.Equals(FreeId)).Name;

                 System.Diagnostics.Debug.Write(EmName + " and " + FreeName);

               

             }
             catch
             {

             }*/





            //  System.Diagnostics.Debug.Write(EmId + " and "+FreeId);
            return RedirectToAction("CreateProject", "Project");

        }

        public ActionResult GotoShowProjects()
        {
            return RedirectToAction("showProjects", "Project");

        }

      

    }
}
