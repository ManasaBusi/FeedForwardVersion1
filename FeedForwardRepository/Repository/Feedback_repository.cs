using FeedForwardBusinessEntities.EntityContext;
using FeedForwardBusinessEntities.EntityModels;
using FeedForwardBusinessEntities.ViewModels;
using FeedForwardRepository.Abstract;
using FeedForwardUtilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardRepository.Repository
{


    public class Feedback_repository : IFeedback_Repository
    {
        Context _context = new Context();

        List<int> levelIDs;

        FeedbackSchedulingViewModel fsVM = new FeedbackSchedulingViewModel();

        FeedbackSchedulingDetail fsDetail = new FeedbackSchedulingDetail();

        FeedbackSchedulingResultsViewModel fsDetailVM = new FeedbackSchedulingResultsViewModel();

        FeedbackSubmissionViewModel fsubmissionVM = new FeedbackSubmissionViewModel();

        List<FeedbackSession> lstFeedbackSSessions = new List<FeedbackSession>();

        List<FeedbackCaption> lstFeedbackCaptions = new List<FeedbackCaption>();

        //FeedbackSession fSession = new FeedbackSession();

        public FeedbackSchedulingViewModel GetUniqueLevelIDs()
        {

            fsVM.lstLevel = _context.LevelDetailInfo.ToList();
            fsVM.lstDesignation = _context.DesignationLevelInfo.ToList();
            fsVM.lstFeedBackCategoryLevel = _context.FeedbackCategoryLevelInfo.ToList();

            return fsVM;
        }

        public List<SelectListItem> GetDesignationBasedonLevelID(int levelID)
        {


            List<SelectListItem> lstDesignations = new List<SelectListItem>();

            //List<DesignationLevel> designationlist = new List<DesignationLevel>();

            fsVM.lstDesignation = (_context.DesignationLevelInfo.Where(x => x.LevelID == levelID)).ToList();


            foreach (DesignationLevel designation in fsVM.lstDesignation)
            {

                lstDesignations.Add(new SelectListItem() { Text = designation.Designation, Value = designation.DesignationID.ToString() });
            }

            lstDesignations.Insert(0, new SelectListItem() { Text = "Please Select Designation", Value = "0" });

            return lstDesignations;
        }

        public List<SelectListItem> GetFeedBackCategoryLevels(int levelID)
        {
            List<SelectListItem> categoryLevels = new List<SelectListItem>();


            //levelIDs = _context.LevelDetailInfo.Select(x => x.levelID).ToList();

            //fsVM.lstFeedBackCategoryLevel = _context.FeedbackCategoryLevelInfo.ToList();

            levelIDs = _context.FeedbackCategoryLevelInfo.Select(x => x.FCLID).ToList();

            //categoryLevels.Insert(0, new SelectListItem() { Text = "Please Select FeedBackCategoryLevel", Value = "0" });

            if (levelID >= levelIDs.Min())
            {
                FeedbackCategoryLevel fc = new FeedbackCategoryLevel();
                fsVM.lstFeedBackCategoryLevel = _context.FeedbackCategoryLevelInfo.Where(x => x.FCLID <= levelID).ToList();
                //fsVM.lstFeedBackCategoryLevel.Add(fc);
            }

            //if (levelID == levelIDs.Min() + 1)
            //{
            //    //categoryLevels.Add(new SelectListItem() { Text = "Senior Level", Value = "02" });
            //}

            //if (levelID >= levelIDs.Min() + 2)
            //{
            //    //categoryLevels.Add(new SelectListItem() { Text = "Super Senior Level", Value = "03" });
            //}

            //if (levelID == levelIDs.Min())
            //{
            //    categoryLevels.Add(new SelectListItem() { Text = "Peer Level", Value = "01" });

            //    return categoryLevels;
            //}

            //if (levelID == levelIDs.Min() + 1)
            //{
            //    categoryLevels.Add(new SelectListItem() { Text = "Peer Level", Value = "01" });
            //    categoryLevels.Add(new SelectListItem() { Text = "Senior Level", Value = "02" });
            //    return categoryLevels;
            //}

            //else if (levelID >= levelIDs.Min() + 2)
            //{
            //    categoryLevels.Add(new SelectListItem() { Text = "Peer Level", Value = "01" });
            //    categoryLevels.Add(new SelectListItem() { Text = "Senior Level", Value = "02" });
            //    categoryLevels.Add(new SelectListItem() { Text = "Super Senior Level", Value = "03" });

            //    return categoryLevels;
            //}

            foreach (FeedbackCategoryLevel fclevel in fsVM.lstFeedBackCategoryLevel)
            {

                categoryLevels.Add(new SelectListItem() { Text = fclevel.FCLDescription, Value = fclevel.FCLID.ToString() });
            }


            return categoryLevels;
        }

        public List<FeedbackSession> LoadFeedbackSessions()
        {

            lstFeedbackSSessions = _context.FeedbackSessionInfo.ToList();

            return lstFeedbackSSessions;
        }

        public List<FeedbackCaptionsViewModel> LoadFeedbackCaptions()
        {

            List<FeedbackCaptionsViewModel> lstFeedbackCaptionsVM = new List<FeedbackCaptionsViewModel>();

            lstFeedbackCaptions = _context.FeedbackCaptionInfo.ToList();

            foreach (FeedbackCaption feedbackCaption in lstFeedbackCaptions)
            {
                FeedbackCaptionsViewModel fcaptionvm = new FeedbackCaptionsViewModel();
                fcaptionvm.FCID = feedbackCaption.FCID;
                fcaptionvm.FCDescription = feedbackCaption.FCDescription;

                lstFeedbackCaptionsVM.Add(fcaptionvm);
            }

            return lstFeedbackCaptionsVM;
        }

        public List<FeedbackSchedulingDetail> LoadFeedbackSchedulingResults()
        {
            List<FeedbackSchedulingDetail> lstfsDetails = new List<FeedbackSchedulingDetail>();
            lstfsDetails = _context.FeedbackSchedulingDetailInfo.ToList();

            return lstfsDetails;
        }

        public int GetLevelIDBasedonDesignation(int designationID)
        {
            int levelID = 0;

            levelID = (_context.DesignationLevelInfo.Where(x => x.DesignationID == designationID)).Select(m => m.LevelID).FirstOrDefault();

            return levelID;

        }

        public List<QuestionDetail> LoadQuestionsOfUser(string userID)
        {
            List<QuestionDetail> lstQuestionsOfUser = new List<QuestionDetail>();
           
            int designationID = _context.UserDetailInfo.Where(x => x.UserID == userID).Select(m => m.DesignationID).FirstOrDefault();

            int levelID = GetLevelIDBasedonDesignation(designationID);

            lstQuestionsOfUser = _context.QuestionDetailInfo.Where(x => x.LevelID == levelID).ToList();

            return lstQuestionsOfUser;
        }
        public List<FeedbackSchedulingResultsViewModel> LoadFeedbackSchedulingListOfUser(string userID)
        {
            List<FeedbackSchedulingResultsViewModel> lstfsList = new List<FeedbackSchedulingResultsViewModel>();

            //FeedbackSchedulingResultsViewModel fsvm = new FeedbackSchedulingResultsViewModel();

            List<FeedbackSchedulingDetail> lstfsDetails = _context.FeedbackSchedulingDetailInfo.ToList();

            List<FeedbackSchedulingDetail> lstfsDetailsUser = new List<FeedbackSchedulingDetail>();

            lstfsDetailsUser = lstfsDetails.Where(x => x.feedbackFromUserID == userID).ToList();

            lstFeedbackSSessions = _context.FeedbackSessionInfo.ToList();

            foreach (FeedbackSchedulingDetail user in lstfsDetailsUser)
            {
                FeedbackSchedulingResultsViewModel fsResultsVM = new FeedbackSchedulingResultsViewModel();

                fsResultsVM.FeedbackSchID = user.FeedbackSchID;
                fsResultsVM.feedbackToUserID = user.feedbackToUserID;
                fsResultsVM.feedbackToName = user.feedbackToName;
                fsResultsVM.feedbackFromUserID = user.feedbackFromUserID;
                fsResultsVM.feedbackFromName = user.feedbackFromName;
                fsResultsVM.IsCompleted = user.IsCompleted;
                fsResultsVM.FeedbackSSessionID = user.FeedbackSessionSID;
                fsResultsVM.FeedbackSessionDesc = lstFeedbackSSessions.Where(x => x.FSID == user.FeedbackSessionSID).Select(m => m.FSDescription).FirstOrDefault();
                lstfsList.Add(fsResultsVM);
            }

            return lstfsList;
        }

        public FeedbackSchedulingResultsViewModel PrepareFeedBackLists(int levelID, int Designationid, int FeedBackCategoryLevel)
        {
            //fsResults.lstFeedbackSSessions = _context.FeedbackSessionInfo.ToList();

            List<FeedbackSchedulingDetail> lstfsDetails = _context.FeedbackSchedulingDetailInfo.ToList();

            List<FeedbackSchedulingDetail> lstfsDetailsOutput = new List<FeedbackSchedulingDetail>();

            List<string> lstfsDetailsAvailable = lstfsDetails.Where(x => x.IsCompleted == true).Select(x => x.feedbackFromUserID).ToList();

            int feedbackByLevelID = 0;

            List<int> designationIDlist = new List<int>();

            List<UserDetail> lstAllUsers = _context.UserDetailInfo.ToList();

            List<UserDetail> selectedFeedBackToUsers = lstAllUsers.Where(x => x.DesignationID == Designationid).ToList();


            fsDetailVM.feedbackToUserID = selectedFeedBackToUsers.Select(m => m.UserID).FirstOrDefault().ToString();

            fsDetailVM.feedbackToName = selectedFeedBackToUsers.Select(m => m.Name).FirstOrDefault().ToString();

            fsDetail.feedbackToUserID = selectedFeedBackToUsers.Select(m => m.UserID).FirstOrDefault().ToString();

            fsDetail.feedbackToName = selectedFeedBackToUsers.Select(m => m.Name).FirstOrDefault().ToString();

            if (FeedBackCategoryLevel == 1)
            {
                feedbackByLevelID = levelID;

            }

            else if (FeedBackCategoryLevel == 2)
            {
                feedbackByLevelID = levelID - 1;
            }

            else if (FeedBackCategoryLevel == 3)
            {
                feedbackByLevelID = levelID - 2;
            }

            designationIDlist = (_context.DesignationLevelInfo.Where(x => x.LevelID == feedbackByLevelID).Select(m => m.DesignationID)).ToList();

            List<UserDetail> selectedFeedBackByUsers = lstAllUsers.Where(x => (designationIDlist.Contains(x.DesignationID)) && (x.UserID != fsDetail.feedbackToUserID)).ToList();

            foreach (UserDetail usr in selectedFeedBackByUsers)
            {
                if (lstfsDetailsAvailable.Contains(usr.UserID))
                {
                    fsDetail.feedbackFromUserID = usr.UserID;
                    fsDetail.feedbackFromName = usr.Name;
                    fsDetailVM.feedbackFromUserID = usr.UserID;
                    fsDetailVM.feedbackFromName = usr.Name;
                    break;
                }
            }

            //lstfsDetailsOutput.Add(fsDetailVM);

            lstfsDetailsOutput.Add(fsDetail);

            fsDetailVM.lstFeedbackSchedulingDetails = lstfsDetailsOutput;

            return fsDetailVM;
        }


        public string SaveFeedbackSchedulingResults(FeedbackSchedulingResultsViewModel fsResultsVM)
        {
            string msg = string.Empty;

            List<FeedbackSchedulingDetail> lstfsDetails = _context.FeedbackSchedulingDetailInfo.ToList();

            List<FeedbackSchedulingDetail> lstfsDetailsExists = lstfsDetails.Where(x => (x.feedbackFromUserID == fsResultsVM.feedbackFromUserID) && (x.feedbackToUserID == fsResultsVM.feedbackToUserID) && (fsResultsVM.FeedbackSSessionID == fsResultsVM.FeedbackSSessionID)).ToList();

            if (lstfsDetailsExists != null && lstfsDetailsExists.Count > 0)
            {
                msg = "Record Exists";
                return msg;
            }


            try
            {
                fsDetail.feedbackToUserID = fsResultsVM.feedbackToUserID;
                fsDetail.feedbackToName = fsResultsVM.feedbackToName;
                fsDetail.feedbackFromUserID = fsResultsVM.feedbackFromUserID;
                fsDetail.feedbackFromName = fsResultsVM.feedbackFromName;
                fsDetail.FeedbackSessionSID = fsResultsVM.FeedbackSSessionID;
                fsDetail.CreatedBy = "SYSTEM";
                fsDetail.CreatedDate = DateTime.Now;

                //_context.Add(fsDetail);
                _context.FeedbackSchedulingDetailInfo.Add(fsDetail);
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

    }
}
