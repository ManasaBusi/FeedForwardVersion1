using FeedForwardBusinessEntities.EntityContext;
using FeedForwardBusinessEntities.EntityModels;
using FeedForwardBusinessEntities.ViewModels;
using FeedForwardRepository.Abstract;
using FeedForwardUtilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeedForwardRepository.Repository
{
    public class Auth_repository : IAuth_Repository
    {
        Context _context = new Context();

        UserDetailViewModel usrDetails = new UserDetailViewModel();

        List<UserDetail> lstUsrDetails = new List<UserDetail>();

        UserDetail usrDetail = new UserDetail();

        QuestionDetail quesDetail = new QuestionDetail();

        //QuestionLevelViewModel qvm = new QuestionLevelViewModel();
        public UserDetail AuthenticateCurrentUser(UserLoginViewModel usr)
        {

            try
            {
                lstUsrDetails = _context.UserDetailInfo.ToList();

                usrDetail = lstUsrDetails.FirstOrDefault(x => (x.UserID == usr.UserID.Trim()) && (x.Password == usr.Password.Trim()));

            }
            catch (Exception ex)
            {
                var obj = UtilityForExceptions.GetInstance();
                obj.LogExceptions(ex.Message, ex.StackTrace);
            }

            return usrDetail;
        }

        public string RegisterCurrentUser(UserDetailViewModel usrDetailVM)
        {
            //UserDetail usrDetail = new UserDetail();
            string msg = string.Empty;

            try
            {
                usrDetail.ID = usrDetailVM.ID;
                usrDetail.UserID = usrDetailVM.UserID;
                usrDetail.Name = usrDetailVM.Name;
                usrDetail.Email = usrDetailVM.Email;
                usrDetail.RoleID = usrDetailVM.RoleID;
                usrDetail.Mobile = usrDetailVM.Mobile;
                usrDetail.DesignationID = usrDetailVM.DesignationID;
                usrDetail.CreatedDate = DateTime.Now;
                usrDetail.CreatedBy = "SYSTEM";

                //Random rnd = new Random();
                //int pwd = rnd.Next(10000, 99999);

                usrDetail.Password = usrDetailVM.Password;

                usrDetail.PasswordChangeDate = null;

                usrDetail.EmpID = usrDetailVM.EmpID;

                _context.Add(usrDetail);
                _context.SaveChanges();
                msg = "success";

            }
            catch (Exception ex)
            {
                usrDetail.Password = null;
                var obj = UtilityForExceptions.GetInstance();
                obj.LogExceptions(ex.Message, ex.StackTrace);
                msg = "failed";
            }

            return msg;
        }

        public List<UserDetail> GetAllUsers()
        {
            if (_context != null)
            {

                lstUsrDetails = _context.UserDetailInfo.ToList();

            }

            return lstUsrDetails;
        }

        public UserDetailViewModel GetUserDataWithAllRolesAndDesignations()
        {

            try
            {
                if (_context.RoleDetailInfo != null)
                {

                    usrDetails.RoleList = _context.RoleDetailInfo.ToList();

                }

                if (_context.DesignationLevelInfo != null)
                {
                    usrDetails.DesignationList = _context.DesignationLevelInfo.ToList();

                }

            }
            catch (Exception ex)
            {

                var obj = UtilityForExceptions.GetInstance();
                obj.LogExceptions(ex.Message, ex.StackTrace);

            }

            return usrDetails;
        }


        public string ChangePassword(string userID, string newPassword)
        {
            string msg = string.Empty;

            try
            {
                List<UserDetail> lstUserDetails = _context.UserDetailInfo.ToList();
                usrDetail = lstUserDetails.FirstOrDefault(x => x.UserID == userID.Trim());
                usrDetail.Password = newPassword;
                usrDetail.PasswordChangeDate = DateTime.Now;
                _context.SaveChanges();

                msg = "success";
            }

            catch (Exception ex)
            {
                var obj = UtilityForExceptions.GetInstance();
                obj.LogExceptions(ex.Message, ex.StackTrace);
                msg = "failed";
            }

            return msg;

        }

        public string SendOTPForForgotPassword(string userID)
        {
            string OTP = string.Empty;

            List<UserDetail> lstUserDetails = _context.UserDetailInfo.ToList();
            usrDetail = lstUserDetails.FirstOrDefault(x => x.UserID == userID.Trim());

            Random rnd = new Random();
            int pwd = rnd.Next(10000, 99999);
            //usrDetail.Password = pwd.ToString();
            OTP = pwd.ToString();

            string emailMsg = $"Here is your OTP: {OTP}, please change once you login";

            UtilityForSendingEmail.SendEmail("manasaekh@gmail.com", "m.deepthi1@gmail.com", "Forgot Password: OTP sent", emailMsg);


            return OTP;
        }

        public List<int> GetLevelIDList()
        {
            List<int> levelIDList = new List<int>();
            try
            {

                if (_context.DesignationLevelInfo != null && _context.DesignationLevelInfo.Count() > 0)
                {
                    levelIDList = _context.DesignationLevelInfo.Select(a => a.LevelID).ToList();

                }

            }
            catch (Exception ex)
            {

                var obj = UtilityForExceptions.GetInstance();
                obj.LogExceptions(ex.Message, ex.StackTrace);

            }

            return levelIDList;
        }

        public List<QuestionDetail> GetQuestionDetailsList()
        {
            string msg = string.Empty;

            List<QuestionDetail> lstQuestionDetails = new List<QuestionDetail>();

            try
            {

                if (_context.QuestionDetailInfo != null)
                {
                    lstQuestionDetails = _context.QuestionDetailInfo.ToList();

                }

            }
            catch (Exception ex)
            {

                var obj = UtilityForExceptions.GetInstance();
                obj.LogExceptions(ex.Message, ex.StackTrace);

            }

            return lstQuestionDetails;
        }

        public string SaveQuestionDetails(QuestionLevelViewModel qvm)
        {
            string msg = string.Empty;

            try
            {

                quesDetail.QuestionID = qvm.QuestionID;
                quesDetail.Question = qvm.Question;
                quesDetail.LevelID = qvm.LevelID;
                quesDetail.CreatedDate = DateTime.Now;
                quesDetail.CreatedBy = "SYSTEM";

                _context.Add(quesDetail);
                _context.SaveChanges();

                msg = "Success";
            }

            catch (Exception ex)
            {
                var obj = UtilityForExceptions.GetInstance();
                obj.LogExceptions(ex.Message, ex.StackTrace);
                msg = "failed";
            }

            return msg;

        }

        public List<UserDetail> Search(string searchUser)
        {
            List<UserDetail> matchedusrDetails = new List<UserDetail>();

            //var userQuery = from x in _context.UserDetailInfo select x;

            //if (!string.IsNullOrEmpty(searchUser))
            //{
            //    userQuery = userQuery.Where(x => x.UserID.Contains(searchUser) || x.Email.Contains(searchUser));
            //}

            //return userQuery;

            lstUsrDetails = _context.UserDetailInfo.ToList();

            matchedusrDetails = (lstUsrDetails.Where(x => x.UserID.Contains(searchUser) || x.Name.Contains(searchUser) || x.Email.Contains(searchUser))).ToList();

            return matchedusrDetails;

        }
    }
}

