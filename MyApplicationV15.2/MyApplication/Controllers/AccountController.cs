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
    public class AccountController : Controller
    {

        private IAccountRepository accountRepo;
        private IProfileRepository profileRepo;

        public AccountController()
        {
            this.accountRepo = new AccountRepository(new MVCEntities());
            this.profileRepo = new ProfileRepository(new MVCEntities());
        }
        // test paging

        public ActionResult Index(int? page)
        {

            var userList = from user in accountRepo.GetUsers() select user;
            var users = new List<MyApplication.Member>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    users.Add(new MyApplication.Member()
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        Password = user.Password,
                        ConfirmPassword = user.ConfirmPassword,
                        RegisterCode = user.RegisterCode,
                        ConfirmRegisterCode = user.ConfirmRegisterCode,

                    });
                }
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        //    return View(users);
        }


        //

       /* public ActionResult Index()
        {

            var userList = from user in accountRepo.GetUsers() select user;
            var users = new List<MyApplication.Member>();
            if (userList.Any())
            {
                foreach (var user in userList)
                {
                    users.Add(new MyApplication.Member()
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        Password = user.Password,
                        ConfirmPassword = user.ConfirmPassword,
                        RegisterCode = user.RegisterCode,
                        ConfirmRegisterCode = user.ConfirmRegisterCode,

                    });
                }
            }

            return View(users);
        }*/
        //
        // GET: /Account/Create
        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Account/Create

        [HttpPost]
        public void Create(Member member)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1000000, 99999999);
            // Response.Write(numIterations.ToString());
            //  System.Diagnostics.Debug.Write("Test " + numIterations.ToString());
            member.RegisterCode = numIterations.ToString();
            member.ConfirmRegisterCode = member.RegisterCode;
            member.Name = "Member";

            try
            {

                //if email does not be contain in db
                if (IsExistEmail(member.Email))
                {

                    accountRepo.InsertUser(member);
                    accountRepo.Save();

                    System.Diagnostics.Debug.Write(member.Email + "FUCK ");
                    //var UID = accountRepo.GetUsers().FirstOrDefault(w => w.Email == member.Email).UserId;


                 //   MVCEntities db = new MVCEntities();

                    sendMail(member);

                   // return RedirectToAction("Index", "Home");
                    // return RedirectToAction("ConfirmRegister", "Account");  
                }
                else
                {
                  //  ModelState.AddModelError("", "this email is exist");
                }
            }
            catch {  }
           

        }


        [HttpGet]
        public ActionResult ConfirmRegister()
        {

            return View();

        }

        [HttpPost]
        public ActionResult ConfirmRegister(Member member)
        {
            try
            {
                /* if (IsExistUsername(member.Username))
                   {*/
                member.Username = member.Email;

                MVCEntities db = new MVCEntities();
            //    System.Diagnostics.Debug.Write("TEST1 " + member.Username + " " + member.ConfirmRegisterCode);


                var userList_Id = db.Members.FirstOrDefault(u => u.Email == member.Email && u.ConfirmRegisterCode == member.ConfirmRegisterCode).UserId;
                var userList_Type = db.Members.FirstOrDefault(u => u.Email == member.Email && u.ConfirmRegisterCode == member.ConfirmRegisterCode).UserType;
              //   var AccountComfirmCode = db.Members.FirstOrDefault(u => u.UserId == userList_Id).ConfirmRegisterCode;

                //  var person = accountRepo.GetUsers().FirstOrDefault(u => u.UserId == userList_Id);
                var person = db.Members.FirstOrDefault(u => u.UserId == userList_Id);
                //for uid in profile


                if (person != null)
                {

                    System.Diagnostics.Debug.Write("OK");
                    person.Username = member.Username;
                 //   System.Diagnostics.Debug.Write("Pure " + member.ConfirmRegisterCode + " " + AccountComfirmCode);
                    
                    //check confirm register code//
                /*    if (member.ConfirmRegisterCode.TrimEnd() != person.ConfirmRegisterCode.TrimEnd())
                    {
                        ModelState.AddModelError("", "The confirm register password is wrong!");

                    }
                    else
                    {*/
                        person.ConfirmRegisterCode = "";
                        person.Credit = 2;
                        System.Diagnostics.Debug.Write("OK2" + member.Username);


                        accountRepo.UpdateUser(person);
                        accountRepo.Save();
                        System.Diagnostics.Debug.Write("OK3");

                        ////////////////////////
                        Profile pro = new MyApplication.Profile();
                        pro.UserId = userList_Id;
                        pro.UserType = userList_Type;
                        System.Diagnostics.Debug.Write("pro.UserId " + pro.UserId + " and " + pro.UserType + " ");

                        profileRepo.InsertUser(pro);
                        profileRepo.Save();

                        System.Diagnostics.Debug.Write("OK4");
                  //  }

                    //////////////////////////////////

                    // delete editing information that com with the link by delete confirmRegisterCode
                  

                    if (IsValid(member.Username, member.Password))
                    {
                        FormsAuthentication.SetAuthCookie(member.Username, false);
                        // Session["Username"] = member.Username;

                        //////////////// don't forget to Edit(u => u.UserId == 434);
                        var dbContext = new MVCEntities();
                        var usersListRegis = dbContext.Members.FirstOrDefault(u => u.Username == member.Username && u.Password == member.Password);
                        //to get profile_ID
                        var personRegis = profileRepo.GetUsers().FirstOrDefault(u => u.UserId == usersListRegis.UserId);
                        //   Session["Username"] = member.Username;
                        Session["Name"] = usersListRegis.Name;
                        System.Diagnostics.Debug.Write("member.Name " + usersListRegis.UserId + "x" + usersListRegis.UserType);

                        Session["PID"] = personRegis.ProId;
                        Session["Online"] = "Online";
                        Session["UidInProfile"] = usersListRegis.UserId;
                        Session["UserType"] = usersListRegis.UserType;
                        //    Session["Picture"] = personRegis.ImageData;
                        //  System.Diagnostics.Debug.Write("usersList " + Session["UidInProfile"] + "x");

                        //System.Diagnostics.Debug.Write("Usertype " + Session["UserType"] + "x");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Login Data is incorrect");

                    }


                    //   return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "ConfirmRegister code has used already!");
                    return View();
                }
                /* }
                 else
                 {
                     ModelState.AddModelError("", "this Username is exist");
                 }*/
            }
            catch { ModelState.AddModelError("", "ConfirmRegister code is wrongs or used to register before!");  return View(); }
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Member member)
        {

            if (IsValid(member.Username, member.Password))
            {
                FormsAuthentication.SetAuthCookie(member.Username, false);
                // Session["Username"] = member.Username;
                //////////////// don't forget to Edit(u => u.UserId == 434);
                var dbContext = new MVCEntities();
                var usersList = dbContext.Members.FirstOrDefault(u => u.Username == member.Username && u.Password == member.Password);
                //to get profile_ID
                System.Diagnostics.Debug.Write("usersList " + usersList.Username + " ");
                if (usersList.Username.TrimEnd() == "TDADMIN")
                {

                    Session["UidInProfile"] = usersList.UserId;
                    Session["UserType"] = usersList.UserType;
                    Session["Online"] = "Online";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var person = profileRepo.GetUsers().FirstOrDefault(u => u.UserId == usersList.UserId);
                    Session["Name"] = usersList.Name;
                    Session["PID"] = person.ProId;
                    Session["Online"] = "Online";
                    Session["UidInProfile"] = usersList.UserId;
                    Session["UserType"] = usersList.UserType;
                    //  Session["Picture"] = person.ImageData;
                    System.Diagnostics.Debug.Write(" UserName =" + Session["Name"] + " ProID =" + Session["PID"]);
                    System.Diagnostics.Debug.Write("Online =" + Session["Online"] + " UID =" + Session["UidInProfile"]);
                    System.Diagnostics.Debug.Write("USertype =" + Session["UserType"]);

                    //System.Diagnostics.Debug.Write("Usertype " + Session["UserType"] + "x");
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login Data is incorrect");

            }

            return View();
        }


        private bool IsValid(String username, String password)
        {
            bool isValid = false;

            try
            {


                var dbContext = new MVCEntities();
                var usersList = dbContext.Members.FirstOrDefault(u => u.Username == username && u.Password == password);


                
                if (usersList != null)
                {

                    if (usersList.Username.TrimEnd() == (username) && usersList.Password.TrimEnd() == (password))
                    {

                        System.Diagnostics.Debug.Write("Pass");
                        isValid = true;
                    }
                    else
                    {
                        System.Diagnostics.Debug.Write("Fail");
                        isValid = false;
                    }

                    //isValid = true;
                    System.Diagnostics.Debug.Write("Login");
                    //System.Diagnostics.Debug.Write("Love2 " + Session["UidInProfile"]);
                    return isValid;
                }
                else
                {
                    System.Diagnostics.Debug.Write("Don't Login");
                    return isValid;
                }
            }
            catch { return isValid; }
        }

        private bool IsExistEmail(String Email)
        {
            bool isValid = false;
            try
            {

                System.Diagnostics.Debug.Write("Email " + Email);
                var dbContext = new MVCEntities();
                var usersList = dbContext.Members.FirstOrDefault(u => u.Email == Email);
                // var usersList = from user in dbContext.Members.FirstOrDefault(u => u.Username == username) select user;
                // System.Diagnostics.Debug.Write("usersList "+usersList);

                if (usersList == null)
                {
                    System.Diagnostics.Debug.Write("Email is not Exist");
                    isValid = true;

                    //System.Diagnostics.Debug.Write("Love2 " + Session["UidInProfile"]);
                    return isValid;
                }
                else
                {

                    System.Diagnostics.Debug.Write("Email is Exist");


                    return isValid;
                }
            }
            catch { return isValid; }
        }


        /*   private bool IsExistUsername(String Username)
            {
                bool isValid = false;
                try
                {

                    System.Diagnostics.Debug.Write("username " + Username);
                    var dbContext = new MVCEntities();
                    var usersList = dbContext.Members.FirstOrDefault(u => u.Username == Username);
                    //var usersList = from user in dbContext.Members.FirstOrDefault(u => u.Username == username) select user;
                    // System.Diagnostics.Debug.Write("usersList "+usersList);

                    if (usersList == null)
                    {
                        System.Diagnostics.Debug.Write("Username is not Exist");
                        isValid = true;

                        //System.Diagnostics.Debug.Write("Love2 " + Session["UidInProfile"]);
                        return isValid;
                    }
                    else
                    {

                        System.Diagnostics.Debug.Write("Username is Exist");


                        return isValid;
                    }
                }
                catch { return isValid; }
            }*/



        [HttpGet]
        public ActionResult ShowFreelancer(int? page)
        {



          /*  var userList = from user in accountRepo.GetUsers() select user;

            var users = new List<MyApplication.Member>();

            if (userList.Any())
            {
                foreach (var user in userList)
                {

                    //space bar 10 times - -'' i'll fix this point later.
                    if (user.UserType == "Freelancer          ")
                    {
                        users.Add(new MyApplication.Member()
                             {
                                 UserId = user.UserId,

                                 Name = user.Name,
                                 UserType = user.UserType

                             });
                    }
                }
            }

            return View(users);*/
            var userList = from user in accountRepo.GetUsers() select user;

            var users = new List<MyApplication.Member>();

            if (userList.Any())
            {
                foreach (var user in userList)
                {

                    //space bar 10 times - -'' i'll fix this point later.
                    if (user.UserType == "Freelancer          ")
                    {
                        users.Add(new MyApplication.Member()
                        {
                            
                            UserId = user.UserId,
                            Email = user.Email,
                            Name = user.Name,
                            UserType = user.UserType

                        });
                        ViewBag.UserId = user.UserId;
                    }
                }
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(users.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult ShowProfiles(int id)
        {
            ////////////////is implementing right here/////////////////
            try
            {

                return RedirectToAction("ShowProfiles", "Profile", new { pid = id });
            }
            catch
            {

                return View();
            }

        }




        [HttpGet]
        public ActionResult Logout()
        {
            ////////////////clear session/////////////////

            Session.Clear();
            FormsAuthentication.SignOut();


            return RedirectToAction("index", "Home");

        }
        [HttpGet]
        public ActionResult forgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult forgetPassword(Member mem)
        {
//39322328
           
                try
                { 
                    if (forgetPasswordService(mem.Email, mem.RegisterCode)){
            
                    MVCEntities db = new MVCEntities();

                    var userPassword = db.Members.SingleOrDefault(x => x.Email.Equals(mem.Email)).Password;
                   // System.Diagnostics.Debug.Write("Password " + userPassword);
                    sendForgetPassword(userPassword);
                    return RedirectToAction("index", "Home");
                      }
                    else
                     {
                         ViewData["ErrorText"] = "Email or registercode does not correct.";      
                         return View();
                      }
                }
                catch { return RedirectToAction("forgetPassword", "Account"); }
            
            
           
          
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        //service//
        public void sendMail(Member member)
        {
            string mail = "http://localhost:3883/Account/ConfirmRegister?&Email=" + member.Email + "&RegisterCode=" + member.RegisterCode + "&Password=" + member.Password + "&Username=" + member.Email;
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            message.From = new System.Net.Mail.MailAddress("coffeecold555@gmail.com");
            message.To.Add(new System.Net.Mail.MailAddress("pureman9@gmail.com"));
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "test";
            message.Body = "link to confirm register " + mail+"This is your register code: "+member.RegisterCode;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Send(message);

          
            System.Diagnostics.Debug.Write(mail);
        }

        public bool forgetPasswordService(String Email, String RegisterCode)
        {
              bool isValid = false;
              var dbContext = new MVCEntities();
                try{
                    var usersList = dbContext.Members.FirstOrDefault(u => u.Email == Email && u.RegisterCode == RegisterCode);
      
                if (usersList != null)
                {

                    if (usersList.Email.TrimEnd() == (Email) && usersList.RegisterCode.TrimEnd() == (RegisterCode))
                    {

                        System.Diagnostics.Debug.Write("right here");
                        isValid = true;
                      
                    }
                    else
                    {
                        System.Diagnostics.Debug.Write("right here2");
                        isValid = false;
                       

                    }
                    return isValid;
                }
                else
                {
                    System.Diagnostics.Debug.Write("right here2");
                    return isValid;
                   
                }               
            }
                catch { return isValid; }
               

        }
        public void sendForgetPassword(string password)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.From = new System.Net.Mail.MailAddress("coffeecold555@gmail.com");
            message.To.Add(new System.Net.Mail.MailAddress("pureman9@gmail.com"));
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = "test";
            message.Body = "This is your password " + password ;
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Send(message);
        }
        
            
    }
}
