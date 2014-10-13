using MyApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyApplication.Controllers
{
    public class HomeController : Controller
    {
        private IAccountRepository accountRepo;
        private IProfileRepository profileRepo;
        private INotificationRepository notiRepo;

        public HomeController()
        {
            this.accountRepo = new AccountRepository(new MVCEntities());
            this.profileRepo = new ProfileRepository(new MVCEntities());
            this.notiRepo = new NotificationRepository(new MVCEntities());
        }
        public ActionResult Index()
        {
          // notifySkill("xx");
          
            return View();
        }
        public ActionResult Index2()
        {
            // notifySkill("xx");

            return View();
        }

        public void notifySkill(string txt)
        {
            bool check = false;
            for (int e = 0; e < txt.Length; e++)
            {
                System.Diagnostics.Debug.Write(txt[e] + " ");
                if (txt[e].Equals(','))
                {
                 
                    check=true;

                }
            }
            System.Diagnostics.Debug.Write("Check "+check);
            if (check)
            {

                String strString = txt;
                System.Diagnostics.Debug.Write("Not Alone "+txt);
                //String strString = otherSkill;
                String[] EmArray = strString.Split(',');
                int i = 0;
                int EmLen = EmArray.Length;
                foreach (String skillList in EmArray)
                {
                    System.Diagnostics.Debug.Write("Index at ( " + i + ") is " + skillList+" ");
                    i = i + 1;

                }
            }
            else
            {
                System.Diagnostics.Debug.Write("Alone");
            }

          
            
        }


    }
}
