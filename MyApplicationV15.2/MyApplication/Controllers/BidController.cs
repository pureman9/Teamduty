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
    public class BidController : Controller
    {
        
        private IAccountRepository accountRepo;
        private IJobRepository jobRepo;
        private IBidRepository bidRepo;
        private IDisplayBidRepository disbidRepo;

        public BidController()
        {
            this.accountRepo = new AccountRepository(new MVCEntities());
            this.jobRepo = new JobRepository(new MVCEntities());
            this.bidRepo = new BidRepository(new MVCEntities());
            this.disbidRepo = new DisplayBidRepository(new MVCEntities());
        }
        [HttpGet]
        public ActionResult Bidding()
        {
            return PartialView("Bidding");
        }
        [HttpPost]
        public ActionResult Bidding(DisplayBid disbid)
        {
            try
            {

                //System.Diagnostics.Debug.Write("DRAGONX" + Session["UidInProfile"]);
                //System.Diagnostics.Debug.Write("DRAGONX" + Session["JobId"]);

               int  JobIdinBid = (int)Session["JobId"];
                int UserIdinBid = (int)Session["UidInProfile"];
                /*   
                 bidRepo.InsertUser(bid);
                 bidRepo.Save();*/
                // insert bidID.
                try
                {

                    var db = new MVCEntities();
                    var userList_Id = db.Bids.FirstOrDefault(u => u.JobId == JobIdinBid && u.UserId == UserIdinBid);
                    System.Diagnostics.Debug.Write("DRAGONE" + (int)Session["UidInProfile"]);
                  
                    var NameOfUser = db.Members.FirstOrDefault(n => n.UserId.Equals(UserIdinBid)).Name;
                 

                       disbid.JobId = (int)Session["JobId"];
                       disbid.UserId = (int)Session["UidInProfile"];                 
                       disbid.BidId = userList_Id.BidId;
                       disbid.EmployerId = userList_Id.EmployerId;
                       disbid.Name = NameOfUser;
                       System.Diagnostics.Debug.Write("Passaaaa "+disbid.BidId);

                       disbidRepo.InsertUser(disbid);
                       disbidRepo.Save();

                       return RedirectToAction("ShowDetailCategoryJob", "DisplayJob", new { jid = (int)Session["JobId"] }); 

                   // return PartialView("Bidding");
                    // return RedirectToAction("ShowDetailCategoryJob", "DisplayJob", new { jid = (int)Session["JobId"] });
                }
                    //if there is bidID and want bid more than 1 time.
                  catch {
                      var db = new MVCEntities();
                      var EmployerId = db.Jobs.FirstOrDefault(u => u.JobId == JobIdinBid).UserId;
                 
                    Bid bid = new Bid();
                    bid.JobId = (int)Session["JobId"];
                    bid.UserId = (int)Session["UidInProfile"];
                    bid.Status = true;
                    bid.EmployerId = EmployerId;

                    bidRepo.InsertUser(bid);
                    bidRepo.Save();
                    ///////////////////////////
                    System.Diagnostics.Debug.Write("DRAGOND" + (int)Session["UidInProfile"]);
                  
                        var userList_Id = db.Bids.FirstOrDefault(u => u.JobId == JobIdinBid && u.UserId == UserIdinBid).BidId;
                        var NameOfUser = db.Members.FirstOrDefault(n => n.UserId.Equals(UserIdinBid)).Name;
                        //  System.Diagnostics.Debug.Write("Pass0 "+userList_Id.BidId);
                    //////////////////////////
                       disbid.JobId = (int)Session["JobId"];
                       disbid.UserId = (int)Session["UidInProfile"];
                       disbid.BidId = userList_Id;
                       disbid.EmployerId = EmployerId;
                       disbid.Name = NameOfUser;

                       disbidRepo.InsertUser(disbid);
                       disbidRepo.Save();
                       System.Diagnostics.Debug.Write("Pass");
                       return RedirectToAction("ShowDetailCategoryJob", "DisplayJob", new { jid = (int)Session["JobId"] }); 

                  //  return PartialView("Bidding");
                }
            }
            catch (Exception)
            {
                return PartialView("Bidding");

            }

        }

        [HttpGet]
        public ActionResult showBiddingList()
        {
            try
            {
               
              // var disbidList = from user in disbidRepo.GetUsers(). select user;
                var disbidList = disbidRepo.GetUsers().OrderByDescending(u => u.DispBidId);
                
                var bids = new List<MyApplication.DisplayBid>();
                if (disbidList.Any())
                {
                    foreach (var bid in disbidList)
                    {
                        if (bid.JobId.Equals((int)Session["JobId"]))
                        {
                            ViewBag.JobId = (int)Session["JobId"];
                            bids.Add(new MyApplication.DisplayBid()
                            {
                                EmployerId = bid.EmployerId,
                                JobId = bid.JobId,
                                UserId = bid.UserId,
                                BidId = bid.BidId,
                                Detail = bid.Detail,
                                Budget = bid.Budget,
                                Name = bid.Name,
                            });
                        }
                    }
                }
                //get employer ID//
                MVCEntities db = new MVCEntities();
               // var employeeId = db.Jobs.FirstOrDefault(j => j.JobId.Equals((int)Session["JobId"])).UserId;
                //ViewBag.employeeId = employeeId;

                //////////////////
                try
                {
                    if (Session["UserType"].Equals("Employer            "))
                    {
                        ViewBag.UserType = "Employer            ";
                    }
                }
                catch
                {
                   
                }
              //  return View();
                return PartialView("showBiddingList", bids);
            }
            catch
            {                
                return View();
            }  
        }

         [HttpGet]
        public ActionResult ShowProfilesFromBid(int id)
        {
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
        public ActionResult RequestFreelancer()
        {
           

            return View("Bidding");
        }
        
        

    }
}
