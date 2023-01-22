using FeedForwardBusinessEntities.EntityModels;
using FeedForwardBusinessEntities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardRepository.Abstract
{
    public interface IAuth_Repository
    {
        public UserDetail AuthenticateCurrentUser(UserLoginViewModel usr);

        public UserDetailViewModel GetUserDataWithAllRolesAndDesignations();

        public string RegisterCurrentUser(UserDetailViewModel usrDetailVM);

        public string ChangePassword(string userID, string newPassword);

        public string SendOTPForForgotPassword(string userID);

        public List<int> GetLevelIDList();

        public List<QuestionDetail> GetQuestionDetailsList();

        public string SaveQuestionDetails(QuestionLevelViewModel qvm);

        //public IQueryable<UserDetail> Search(string searchUser);

        public List<UserDetail> Search(string searchUser);

        public List<UserDetail> GetAllUsers();
    }
}
