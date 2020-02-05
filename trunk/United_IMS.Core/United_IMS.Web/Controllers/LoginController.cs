using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using United_IMS.Web.Models;
using United_IMS.Web.Repository;
using United_IMS.Web.Security;
using United_IMS.Web.ViewModel;

namespace United_IMS.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        GetDropdown ddl = new GetDropdown();
        LoginRepo db = new LoginRepo();
        
        public ActionResult Index(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.SuccessMessage = "";
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Message = "";
            ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Email, string Password,string OrganizationId, string returnUrl)
        {
            ViewBag.Message = "";
            ViewBag.SuccessMessage = "";
            if (Membership.ValidateUser(Email, Password))
            // if (userrepo.ValidateUser(loginView.Email, loginView.Password))
            {
                var user = (CustomMembershipUser)Membership.GetUser(Email, false);
                var userdetail = db.getPersonalDetail(user.UserId);
                if (user != null)
                {
                    SessionVM userModel = new SessionVM()
                    {
                        UserId = user.UserId,
                        FullName = user.FullName,
                        Email= user.Email,
                        //ActualRoleId = user.RoleId,
                        RoleId = user.RoleId,
                        IsAdmin = user.IsAdmin,
                       
                        //IsManager= confirmer.IsReportingManager(user.PersonalId);
                    };
                    SessionRepo sesrepo = new SessionRepo();
                    var ses=sesrepo.GetSessionById(user.UserId);
                    if (ses == null)
                    {
                        sesrepo.AddSession(new SC_LoginHistory()
                        {
                            UserId = user.UserId,
                            LoginDate = DateTime.Now,
                            RoleId = 0,
                            OrganizationId = (int)userdetail.OrganizationId,//Convert.ToInt32(OrganizationId),
                            ActualOrganizationId = (int)userdetail.OrganizationId,// Convert.ToInt32(OrganizationId),
                            LogOutDate = DateTime.Now.AddMinutes(1200),

                        });
                    }
                    else
                    {
                        sesrepo.EditSession(new SC_LoginHistory()
                        {
                            UserId = user.UserId,
                            LoginDate = DateTime.Now,
                            RoleId = 0,
                            OrganizationId = (int)userdetail.OrganizationId,//Convert.ToInt32(OrganizationId),
                            ActualOrganizationId = (int)userdetail.OrganizationId,// Convert.ToInt32(OrganizationId),
                            LogOutDate = DateTime.Now.AddMinutes(1200),
                            LoginId=ses.LoginId

                        });
                    }
                    string userData = JsonConvert.SerializeObject(userModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                        (
                        1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(1200), false, userData
                        );

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("CookieUNITED1", enTicket);
                    faCookie.Expires = DateTime.Now.AddMinutes(1200);
                    Response.Cookies.Add(faCookie);
                }

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    if (Password == "password")
                    {
                        return RedirectToAction("ChangePassword", "Profile");
                    }
                    return RedirectToAction("Index", "Dashboard");
                    //return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                ViewBag.Message = "Specified User doesn't exists";
            }
            return View();
            
        }



        public ActionResult LogOut()
        {
            //Session.RemoveAll();
            //ViewBag.Message = "";
            //ViewBag.SuccessMessage = "You have logged out successfully";
            HttpCookie cookie = new HttpCookie("CookieUNITED1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        // GET: Login
        //      public ActionResult Index()
        //      {
        //          ViewBag.OrganizationId = new SelectList(ddl.GetOrganizationList(),"Id","Name");
        //          return View();
        //      }
        //[HttpPost]
        //public ActionResult Index(FormCollection frm)
        //{
        //          var usr=db.CheckUser(frm["OrganizationId"],frm["Email"],frm["Password"]);
        //          if (usr == null)
        //              return RedirectToAction("Index");
        //          Session["FullName"] = usr.FullName;
        //          Session["UserId"] = usr.UserId;
        //          Session["Email"] = usr.Email;
        //          Session["OrganizationId"] = usr.OrganizationId;
        //          Session.Timeout = 120;
        //	return RedirectToAction("Index", "Dashboard");
        //}
    }
}