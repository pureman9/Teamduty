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
    public class ProfileController : Controller
    {
        private IProfileRepository profileRepo;
        private IAccountRepository accountRepo;

        public ProfileController()
        {
            this.accountRepo = new AccountRepository(new MVCEntities());

            this.profileRepo = new ProfileRepository(new MVCEntities());
        }

        [HttpGet]
        public ActionResult Profiles()
        {
            ////////////////is implementing right here/////////////////
            // ViewBag.ID = id;
            try
            {
                int uid = (int)Session["UidInProfile"];
                // System.Diagnostics.Debug.Write("Uid in Profile is equal " + uid + " ");

                var dbContext = new MVCEntities();
                var profile = dbContext.Profiles.FirstOrDefault(u => u.UserId == uid);
                System.Diagnostics.Debug.Write("UIS is equal " + uid + " ");
                System.Diagnostics.Debug.Write("Pid is equal " + profile.ProId + " ");
                Session["ProfileId"] = profile.ProId;



                //   var profileDetail = dbContext.Profiles.FirstOrDefault(p => p.ProId == profile.ProId);


                try
                {
                    if (Session["UserType"].Equals("Freelancer          "))
                    {
                       // var profileDetail = dbContext.Profiles.FirstOrDefault(p => p.ProId == (int)Session["ProfileId"]);

                        var ProfileModel = new MyApplication.Profile();
                      
                         System.Diagnostics.Debug.Write("TTTxx");
                         ProfileModel.Name = profile.Name;
                         System.Diagnostics.Debug.Write("TTT " + ProfileModel.Name + " " + profile.Name);

                        if (ProfileModel.Name == null)
                        {
                            ProfileModel.Name = " ";
                        }
                        else
                        {
                            ProfileModel.Name = profile.Name.TrimEnd();
                        }

                         ProfileModel.Telephone = profile.Telephone;

                        if (ProfileModel.Telephone == null)
                        {
                            ProfileModel.Telephone = " ";
                        }
                        else
                        {
                            ProfileModel.Telephone = profile.Telephone.TrimEnd();
                        }

                         
                          
                         
                          ProfileModel.Status = profile.Status;

                        if (ProfileModel.Status == null)
                        {
                            ProfileModel.Status = " ";
                        }
                        else
                        {
                            ProfileModel.Status = profile.Status.TrimEnd();
                        }


                       

                        ProfileModel.Fax = profile.Fax;

                        if (ProfileModel.Fax == null)
                        {
                            ProfileModel.Fax = " ";
                        }
                        else
                        {
                            ProfileModel.Fax = profile.Fax.TrimEnd();
                        }

                           

                          ProfileModel.Address = profile.Address;

                        if (ProfileModel.Address == null)
                        {
                            ProfileModel.Address = " ";
                        }
                        else
                        {
                            ProfileModel.Address = profile.Address.TrimEnd();
                        }

                         
                               ProfileModel.CompanyName = profile.CompanyName;

                        if (ProfileModel.CompanyName == null)
                        {
                            ProfileModel.CompanyName = " ";
                        }
                        else
                        {
                            ProfileModel.CompanyName = profile.CompanyName.TrimEnd();
                        }
                          
                        
                               ProfileModel.About_Yourself = profile.About_Yourself;

                        if (ProfileModel.About_Yourself == null)
                        {
                            ProfileModel.About_Yourself = " ";
                        }
                        else
                        {
                            ProfileModel.About_Yourself = profile.About_Yourself.TrimEnd();
                        }
                        ProfileModel.Android = profile.Android;
                        ProfileModel.ASP_NET = profile.ASP_NET;
                        ProfileModel.C = profile.C;
                        ProfileModel.Cplus = profile.Cplus;
                        ProfileModel.Csharp = profile.Csharp;
                        ProfileModel.Css = profile.Css;
                        ProfileModel.Html = profile.Html;
                             ProfileModel.Java = profile.Java;
                             ProfileModel.Javascript = profile.Javascript;
                             ProfileModel.Objective_C = profile.Objective_C;
                             ProfileModel.PHP = profile.PHP;
                             ProfileModel.Python = profile.Python;
                             ProfileModel.Ruby = profile.Ruby;
                             ProfileModel.SQL = profile.SQL;
                             ProfileModel.XML = profile.XML;
                            

                             ProfileModel.Others = profile.Others;

                             if (ProfileModel.Others == null)
                             {
                                 ProfileModel.Others = " ";
                             }
                             else
                             {
                                 ProfileModel.Others = profile.Others.TrimEnd();
                             }

                             ProfileModel.WebDeveloper = profile.WebDeveloper;
                             ProfileModel.Tester = profile.Tester;
                             ProfileModel.MobileDeveloper = profile.MobileDeveloper;
                             ProfileModel.SystemDesign = profile.SystemDesign;
                             ProfileModel.SystemAnalyst = profile.SystemAnalyst;
                             ProfileModel.ProjectManager = profile.ProjectManager;
                             ProfileModel.GameDeveloper = profile.GameDeveloper;
                             ProfileModel.Marketing = profile.Marketing;
                             ProfileModel.OthersJob = profile.OthersJob;
                        return View(ProfileModel);
                    }
                    else if (Session["UserType"].Equals("Employer            "))
                    {
                        int PID = (int)Session["ProfileId"];
                        System.Diagnostics.Debug.Write("HAVE++1 " + PID);

                        var profileDetail = dbContext.Profiles.FirstOrDefault(p => p.ProId.Equals(PID));

                        System.Diagnostics.Debug.Write("HAVE++ " + profileDetail.Name);


                        var ProfileModel = new MyApplication.Profile();
                        ProfileModel.Name = profile.Name;

                        if (ProfileModel.Name == null)
                        {
                            ProfileModel.Name = " ";
                        }
                        else
                        {
                            ProfileModel.Name = profile.Name.TrimEnd();
                        }

                        ProfileModel.CompanyName = profile.CompanyName;
                        System.Diagnostics.Debug.Write("HAVE++2 " + ProfileModel.Name);
                        if (ProfileModel.CompanyName == null)
                        {
                            ProfileModel.CompanyName = " ";
                        }
                        else
                        {
                            ProfileModel.CompanyName = profile.CompanyName.TrimEnd();
                        }

                        ProfileModel.Address = profile.Address;
                        //   System.Diagnostics.Debug.Write("HAVE++2 " + ProfileModel.Name);
                        if (ProfileModel.Address == null)
                        {
                            ProfileModel.Address = " ";
                        }
                        else
                        {
                            ProfileModel.Address = profile.Address.TrimEnd();
                        }

                        ProfileModel.About_Yourself = profile.About_Yourself;
                        //   System.Diagnostics.Debug.Write("HAVE++2 " + ProfileModel.Name);
                        if (ProfileModel.About_Yourself == null)
                        {
                            ProfileModel.About_Yourself = " ";
                        }
                        else
                        {
                            ProfileModel.About_Yourself = profile.About_Yourself.TrimEnd();
                        }

                        ProfileModel.Telephone = profile.Telephone;
                        //   System.Diagnostics.Debug.Write("HAVE++2 " + ProfileModel.Name);
                        if (ProfileModel.Telephone == null)
                        {
                            ProfileModel.Telephone = " ";
                        }
                        else
                        {
                            ProfileModel.Telephone = profile.Telephone.TrimEnd();
                        }
                        ProfileModel.Fax = profile.Fax;
                        //   System.Diagnostics.Debug.Write("HAVE++2 " + ProfileModel.Name);
                        if (ProfileModel.Fax == null)
                        {
                            ProfileModel.Fax = " ";
                        }
                        else
                        {
                            ProfileModel.Fax = profile.Fax.TrimEnd();
                        }


                        System.Diagnostics.Debug.Write("Right here xxxx" + profile.Name);

                        return View(ProfileModel);
                    }
                }
                catch
                {

                }

                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult Profiles(Profile profile, HttpPostedFileBase file)
        {
            // System.Diagnostics.Debug.Write("Right here " + file.FileName.ToString() + " ");
            int uid = (int)Session["UidInProfile"];
            try
            {
                // MVCEntities dbContext = new MVCEntities();
                var account = accountRepo.GetUserByID(uid);
                System.Diagnostics.Debug.Write("account Here " + account.Name + " ");

                if (Session["Usertype"].Equals("Employer            "))
                {
                    UploadImageToDB(file);

                    var profileToDb = profileRepo.GetUsers().FirstOrDefault(u => u.ProId == (int)Session["ProfileId"]);
                    // System.Diagnostics.Debug.Write("Right hereZero " + profileToDb.ProId + " ");
                    //    System.Diagnostics.Debug.Write("SUS " + Session["UidInProfile"] + " sus2 " + profile.Name);

                    if (profileToDb != null)
                    {
                        System.Diagnostics.Debug.Write("Profile To Database ");

                        profileToDb.Name = profile.Name;
                        profileToDb.Telephone = profile.Telephone;
                        profileToDb.Status = profile.Status;
                        profileToDb.Fax = profile.Fax;
                        profileToDb.Address = profile.Address;
                        profileToDb.CompanyName = profile.CompanyName;
                        profileToDb.About_Yourself = profile.About_Yourself;
                        profileToDb.HideText = "cannot see";
                        profileToDb.Android = false;
                        profileToDb.ASP_NET = false;
                        profileToDb.C = false;
                        profileToDb.Cplus = false;
                        profileToDb.Csharp = false;
                        profileToDb.Css = false;
                        profileToDb.Html = false;
                        profileToDb.Java = false;
                        profileToDb.Javascript = false;
                        profileToDb.Objective_C = false;
                        profileToDb.PHP = false;
                        profileToDb.Python = false;
                        profileToDb.Ruby = false;
                        profileToDb.SQL = false;
                        profileToDb.XML = false;
                        //skill of Employer = Nothing
                        profileToDb.Others = "Nothing";
                        System.Diagnostics.Debug.Write("Profile To Database1 ");
                        //set name to show in system//
                        account.Name = profile.CompanyName;
                        accountRepo.UpdateUser(account);
                        accountRepo.Save();
                        Session["Name"] = account.Name;
                       
                        //////////////////////
                        profileRepo.UpdateUser(profileToDb);
                        profileRepo.Save();

                    }

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    UploadImageToDB(file);

                    var profileToDb = profileRepo.GetUsers().FirstOrDefault(u => u.ProId == (int)Session["ProfileId"]);
                    System.Diagnostics.Debug.Write("Right here " + profileToDb.ProId + " ");

                    if (profileToDb != null)
                    {
                        System.Diagnostics.Debug.Write("Profile To Database ");

                        profileToDb.Name = profile.Name;
                        profileToDb.Telephone = profile.Telephone;
                        profileToDb.Status = profile.Status;
                        profileToDb.Fax = profile.Fax;
                        profileToDb.Address = profile.Address;
                        profileToDb.CompanyName = profile.CompanyName;
                        profileToDb.About_Yourself = profile.About_Yourself;
                        profileToDb.HideText = "xxxxxxxx";

                        profileToDb.Android = profile.Android;
                        profileToDb.ASP_NET = profile.ASP_NET;
                        profileToDb.C = profile.C;
                        profileToDb.Cplus = profile.Cplus;
                        profileToDb.Csharp = profile.Csharp;
                        profileToDb.Css = profile.Css;

                        profileToDb.Html = profile.Html;
                        profileToDb.Java = profile.Java;
                        profileToDb.Javascript = profile.Javascript;
                        profileToDb.Objective_C = profile.Objective_C;
                        profileToDb.PHP = profile.PHP;
                        profileToDb.Python = profile.Python;
                        profileToDb.Ruby = profile.Ruby;
                        profileToDb.SQL = profile.SQL;
                        profileToDb.XML = profile.XML;
                        profileToDb.Others = profile.Others;

                        profileToDb.WebDeveloper = profile.WebDeveloper;
                        profileToDb.Tester = profile.Tester;
                        profileToDb.MobileDeveloper = profile.MobileDeveloper;
                        profileToDb.SystemDesign = profile.SystemDesign;
                        profileToDb.SystemAnalyst = profile.SystemAnalyst;
                        profileToDb.ProjectManager = profile.ProjectManager;
                        profileToDb.GameDeveloper = profile.GameDeveloper;
                        profileToDb.Marketing = profile.Marketing;
                        profileToDb.OthersJob = profile.OthersJob;
                        //set name in account//
                        account.Name = profile.Name;
                        accountRepo.UpdateUser(account);
                        accountRepo.Save();
                        Session["Name"] = account.Name;

                        profileRepo.UpdateUser(profileToDb);
                        profileRepo.Save();
                    }


                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {
                return View();

            }
        }



        //////////////////service///////////////////////
        public void UploadImageToDB(HttpPostedFileBase file)
        {
            Profile img = new Profile();
            // img.Name = file.ContentType;
            //  img.ImageMimeType = file.FileName;
            //   img.ImageData = ConverToBytes(file);

            int ProId = (int)Session["UidInProfile"];

            //var dbContext = new MVCEntities();


            var person = profileRepo.GetUsers().FirstOrDefault(u => u.UserId == ProId);
            //   System.Diagnostics.Debug.Write("Right here " + person.ProId + " ");




            if (person != null)
            {
                if (file == null)
                {


                    //  System.Diagnostics.Debug.Write("Right here xx" + person.PicName + " "+person.ImageMimeType+" "+person.ImageData);

                    profileRepo.UpdateUser(person);
                    profileRepo.Save();
                }
                else
                {
                    person.PicName = file.ContentType;
                    person.ImageMimeType = file.FileName;
                    person.ImageData = ConverToBytes(file);
                    System.Diagnostics.Debug.Write("Right here xx2 " + person.PicName + " " + person.ImageMimeType + " " + person.ImageData);

                    profileRepo.UpdateUser(person);
                    profileRepo.Save();
                }

            }


        }
        public byte[] ConverToBytes(HttpPostedFileBase Image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(Image.InputStream);
            imageBytes = reader.ReadBytes((int)Image.ContentLength);
            return imageBytes;
        }


        public byte[] GetImageBytesByImageId(int ImageId)
        {

            using (MVCEntities dbContext = new MVCEntities())
            {

                return dbContext.Profiles.Where(p => p.UserId == ImageId).SingleOrDefault().ImageData;
            }


        }
        public FileResult GetImage(int ProId)
        {
            try
            {
                byte[] byteArray = GetImageBytesByImageId(ProId);
                if (byteArray != null)
                {
                    return new FileContentResult(byteArray, "image/jpeg");
                }
                ////// if database does not have picture 
                else
                {
                    return new FilePathResult(HttpContext.Server.MapPath("~/Content/Pic1.png"), "image/png");

                    //                       return null;

                }
            }
            catch
            {
                return new FilePathResult(HttpContext.Server.MapPath("~/Content/Pic1.png"), "image/png");

            }

            //                   if (byteArray != null)
            //                   {
            //                       return new FileContentResult(byteArray, "image/jpeg");
            //                   }
            //                       ////// if database does not have picture 
            //                   else
            //                   {
            //                       return new FilePathResult(HttpContext.Server.MapPath("~/Content/Pic1.png"), "image/png");

            ////                       return null;



            //                   }
        }


        [HttpGet]
        public ActionResult ShowProfiles(int pid)
        {
            // actually pid form parameter is UID kiki
            ////////////////is implementing right here/////////////////
            // ViewBag.ID = id;
            try
            {
                //  System.Diagnostics.Debug.Write("Uid in ShowProfile is equal " + pid + " ");

                var profile = profileRepo.GetUsers().FirstOrDefault(u => u.UserId == pid);
                //  System.Diagnostics.Debug.Write("XIS" + profile.ProId + " " + profile.Name);
                var usersProfile = new MyApplication.Profile()
                {


                   
                    ProId = profile.ProId,
                    Name = profile.Name,
                    Telephone = profile.Telephone,
                    About_Yourself = profile.About_Yourself,
                    Address = profile.Address,
                    CompanyName = profile.CompanyName,
                    Fax = profile.Fax,
                    HideText = profile.HideText,
                    

                        Android = profile.Android,
                        ASP_NET = profile.ASP_NET,
                        C = profile.C,
                        Cplus = profile.Cplus,
                        Csharp = profile.Csharp,
                        Css = profile.Css,

                        Html = profile.Html,
                        Java = profile.Java,
                        Javascript = profile.Javascript,
                        Objective_C = profile.Objective_C,
                        PHP = profile.PHP,
                        Python = profile.Python,
                        Ruby = profile.Ruby,
                        SQL = profile.SQL,
                        XML = profile.XML,
                        Others = profile.Others,

                        WebDeveloper = profile.WebDeveloper,
                        Tester = profile.Tester,
                        MobileDeveloper = profile.MobileDeveloper,
                        SystemDesign = profile.SystemDesign,
                        SystemAnalyst = profile.SystemAnalyst,
                        ProjectManager = profile.ProjectManager,
                        GameDeveloper = profile.GameDeveloper,
                        Marketing = profile.Marketing,
                        OthersJob = profile.OthersJob,

                };
                ViewBag.UserId = pid;


                return View(usersProfile);
            }
            catch
            {
                // return RedirectToAction("Index", "Home");
                return View();
            }

        }

         [HttpGet]
        public ActionResult GotoShowjobRequest(int FreelanceId)
        {
            return RedirectToAction("showJobRequest", "Job", new { FreelanceId = FreelanceId });
        }
        

    }
}
