using FeedForwardBusinessEntities.EntityModels;
using FeedForwardBusinessEntities.ViewModels;
using FeedForwardRepository.Abstract;
using FeedForwardRepository.Repository;
using FeedForwardUtilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using FeedForwardBusinessEntities.EntityContext;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FeedForward.Controllers
{
    public class UserDetailController : Controller
    {
        IAuth_Repository _repo;

        public UserDetailController(IAuth_Repository repo)
        {
            _repo = repo;
        }

        UserDetailViewModel usrDetail = new UserDetailViewModel();

        string apiBaseUrl = "https://localhost:7291/";


        UserDetail dt;

        QuestionLevelViewModel qvm = new QuestionLevelViewModel();

        RoleDetail rd = new RoleDetail() { RoleID = 0, RoleDescription = "Select Role" };

        DesignationLevel ds = new DesignationLevel() { DesignationID = 0, Designation = "Select Designation" };

        SearchViewModel srch = new SearchViewModel();

        [HttpGet]
        public IActionResult UserRegistration()
        {
            

            usrDetail = _repo.GetUserDataWithAllRolesAndDesignations();

            usrDetail.RoleList.Insert(0, rd);
            usrDetail.DesignationList.Insert(0, ds);

            return View(usrDetail);
        }


        [HttpPost]
        public async Task<IActionResult> UserRegistration(UserDetailViewModel usrDetailVM, string btnRegister)
        {
            string msg = string.Empty;

            Random rnd = new Random();
            int pwd = rnd.Next(10000, 99999);
            usrDetailVM.Password = pwd.ToString();

            //if (ModelState.IsValid == false)
            //{
            //    foreach (var eachState in ModelState.Values)
            //    {
            //        //eachState.


            //        foreach (var eachError in eachState.Errors)
            //        {
            //            if (string.IsNullOrEmpty(msg))
            //            {
            //                msg = eachError.ErrorMessage;
            //            }
            //            else
            //            {
            //                msg = msg + "," + eachError.ErrorMessage;
            //            }
            //        }
            //    }
            //}

            //if (!string.IsNullOrEmpty(msg))
            //{
            //    ViewBag.InvalidMessages = msg;
            //    usrDetail = _repo.GetUserDataWithAllRolesAndDesignations();
            //    return View(usrDetail);
            //}

            //else
            //{

            //Random rnd = new Random();
            //int pwd = rnd.Next(10000, 99999);
            //usrDetailVM.Password = pwd.ToString();

            if (btnRegister == "Register")
            {


                string msgResponse = _repo.RegisterCurrentUser(usrDetailVM);

                //string msg = "success";

                if (msgResponse == "success")
                {

                    string emailMsg = $"Please use UserID: {usrDetailVM.UserID} and Password: {usrDetailVM.Password} to login in Login Page, please change once you login";

                    UtilityForSendingEmail.SendEmail("manasaekh@gmail.com", "m.deepthi1@gmail.com", "Registration Successful", emailMsg);

                    return RedirectToAction("LoginPage");


                }
            }

            else if (btnRegister == "Register through API")
            {
                string endpoint = string.Empty;

                using (HttpClient client = new HttpClient())
                {
                    endpoint = apiBaseUrl + "api/Authentication/RegisterUser";
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usrDetailVM), Encoding.UTF8, "application/json");
                    var resp = await client.PostAsync(endpoint, content);

                    if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.Info = "User Registered successfully";
                        return RedirectToAction("LoginPage");
                    }
                    else
                    {
                        ViewBag.Info = "User not Registered, try again";
                    }

                    return View();
                }

            }

            else if (btnRegister == "Search")
            {
               

                //usrDetail = _repo.GetUserDataWithAllRolesAndDesignations();
                //usrDetail.RoleList.Insert(0, rd);
                //usrDetail.DesignationList.Insert(0, ds);

                //usrDetailVM.RoleList = usrDetail.RoleList;
                //usrDetailVM.DesignationList = usrDetail.DesignationList;

                List<UserDetail> matchedusrDetails = _repo.Search(usrDetailVM.searchQuery);

                if (matchedusrDetails != null && matchedusrDetails.Count > 0)
                {
                    srch.lstUsrDetails = matchedusrDetails;
                }

                //return View(srch);

                return PartialView("_SearchFormPartial", srch);

                //return PartialView(usrDetailVM);
            }

            //}

            return View();
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MainPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            return View();
        }



        [HttpPost]
        public IActionResult ForgotPassword(UserLoginViewModel usrdata, string btn)
        {
            if (usrdata != null && usrdata.UserID != null)
            {

                HttpContext.Session.SetString("UserID", usrdata.UserID);

                string OTP;

                string userID;

                string userValidated;

                if (btn == "Next")
                {
                    OTP = _repo.SendOTPForForgotPassword(usrdata.UserID);

                    if (OTP != null)
                    {
                        HttpContext.Session.SetString("OTP", OTP);
                        ViewBag.OTPSent = "Please check your email and type your OTP below:";

                    }

                    else { ViewBag.OTPSent = "UserID not found, please enter a valid UserID"; }
                }

                else if (btn == "Submit")
                {
                    OTP = HttpContext.Session.GetString("OTP").ToString();

                    userID = HttpContext.Session.GetString("UserID").ToString();

                    if (usrdata.UserID == userID && usrdata.Password == OTP)
                    {
                        ViewBag.OTPValidated = "OTP validated, please proceed with setting up Password";

                        HttpContext.Session.SetString("userValidated", "true");

                    }

                }

                else if (btn == "Reset Password")
                {
                    userValidated = HttpContext.Session.GetString("userValidated").ToString();

                    if (userValidated == "true")
                    {
                        if (usrdata.NewPassword.Trim() == usrdata.ConfirmPassword.Trim())
                        {
                            string msg = _repo.ChangePassword(usrdata.UserID, usrdata.NewPassword);

                            if (msg == "success")
                            {
                                return RedirectToAction("MainPage", "UserDetail");
                            }

                            else ViewBag.Message = "Password not updated, please try again";


                        }

                        else ViewBag.Message = "New Pasword and Confirm Password dont match";
                    }


                    else ViewBag.Message = "User is not Validated, please go through OTP process first;";

                }
            }

            else ViewBag.Message = "Please enter UserID and follow OTP process";

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginPage(UserLoginViewModel usrdata, string btn)
        {
            if (btn == "Login")
            {
                // Authentication
                dt = _repo.AuthenticateCurrentUser(usrdata);

                if (dt != null)
                {
                    HttpContext.Session.SetString("UserID", usrdata.UserID);
                    //Change password page, passowrd change date null or passowrd change date > 30

                    if (dt.PasswordChangeDate == null || dt.PasswordChangeDate < DateTime.Now.AddDays(-30))
                    {
                        //HttpContext.Session.SetString("UserID", usrdata.UserID);
                        //return RedirectToAction("ChangePassword");

                        return RedirectToAction("ChangePassword", "UserDetail");



                    }

                    else
                    {

                        string[] userRoles = new string[] { dt.RoleID.ToString() };

                        var claims = new List<Claim>();
                        claims.Add(new Claim("username", dt.UserID));   // u00001
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, dt.UserID));      // u000001
                        foreach (var eachRole in userRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, eachRole));
                        }

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);
                    }

                    return RedirectToAction("MainPage", "UserDetail");



                }
                else
                {
                    ViewBag.Info = "Invalid user ID or password";
                }
            }

            else if (btn == "Login with API")
            {

                string endpoint = string.Empty;

                using (HttpClient client = new HttpClient())
                {
                    endpoint = apiBaseUrl + "api/Authentication/AuthenticateUser";
                    StringContent content = new StringContent(JsonConvert.SerializeObject(usrdata), Encoding.UTF8, "application/json");
                    var resp = await client.PostAsync(endpoint, content);

                    if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.Info = "User Authenticated successfully";
                        return RedirectToAction("MainPage", "UserDetail");
                    }
                    else
                    {
                        ViewBag.Info = "User not Authenticated, try again";
                    }

                    return View();
                }

            }

            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {

            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.OTPSent = "Please enter an OTP after entering UserID and Clicking on Next";
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(UserLoginViewModel user)
        {
            string userID = string.Empty;

            if (user.NewPassword.Trim() == user.ConfirmPassword.Trim())
            {

                if (HttpContext.Session != null)
                {
                    userID = HttpContext.Session.GetString("UserID");
                }

                string msg = _repo.ChangePassword(userID, user.NewPassword);

                if (msg == "success")
                {
                    return RedirectToAction("MainPage", "UserDetail");
                }


            }

            else
            {
                ViewBag.Info = "Password and Confirm Password has to be same!!";
            }

            return View();
        }

        [HttpGet]
        public IActionResult QuestionDetails()
        {

            qvm.LevelIDList = _repo.GetLevelIDList();
            qvm.QuestionDetailsList = _repo.GetQuestionDetailsList();
            return View(qvm);
        }


        [HttpPost]
        public IActionResult QuestionDetails(QuestionLevelViewModel qvm)
        {

            string msg = _repo.SaveQuestionDetails(qvm);

            if (msg == "Success")
            {
                qvm.LevelIDList = _repo.GetLevelIDList();
                qvm.QuestionDetailsList = _repo.GetQuestionDetailsList();

                ViewBag.Info = "Question Details Added Successfully!!";
            }

            else
            {
                ViewBag.Info = "Please try again, there is an issue;";
            }



            return View(qvm);
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        //[HttpPost]

        //public async Task<IActionResult> _SearchFormPartial(string searchUser)
        //{
        //    IQueryable<UserDetail> userQuery = _repo.Search(searchUser);


        //    return PartialView("_SearchFormPartial", userQuery);

        //}

        [HttpGet]
        public async Task<IActionResult> _SearchFormPartial()
        {

            //return PartialView(srch);

            return PartialView("_SearchFormPartial", srch);

        }

    }
}
