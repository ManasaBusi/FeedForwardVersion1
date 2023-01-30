using FeedForwardBusinessEntities.EntityModels;
using FeedForwardBusinessEntities.ViewModels;
using FeedForwardRepository.Abstract;
using FeedForwardRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.WebRequestMethods;

namespace FeedForward.Controllers
{

    public class FeedbackSchedulingController : Controller
    {

        FeedbackSchedulingViewModel fsVM = new FeedbackSchedulingViewModel();

        List<FeedbackSchedulingResultsViewModel> lstfsVM = new List<FeedbackSchedulingResultsViewModel>();

        FeedbackSubmissionViewModel fsubmissionVM = new FeedbackSubmissionViewModel();

        List<FeedbackCaption> lstfCaption = new List<FeedbackCaption>();


        IFeedback_Repository _repoFeedback;

        public FeedbackSchedulingController(IFeedback_Repository repoFeedback)
        {
            _repoFeedback = repoFeedback;
        }

        //FeedbackSchedulingResultsViewModel fsDetails = new FeedbackSchedulingResultsViewModel();

        FeedbackSchedulingResultsViewModel fsResultsVM = new FeedbackSchedulingResultsViewModel();


        [HttpGet]
        public ActionResult FeedbackScheduleList()
        {
            string userID = HttpContext.Session.GetString("UserID");
            //string userID = "manasa";
            lstfsVM = _repoFeedback.LoadFeedbackSchedulingListOfUser(userID);

            return View(lstfsVM);
        }

        [HttpGet]

        public ActionResult FeedbackSubmission(int FeedbackSchID, string feedbackFromName)
        {
            fsubmissionVM.lstfeedbackCaptions = _repoFeedback.LoadFeedbackCaptions();
            fsubmissionVM.lstQuestions = _repoFeedback.LoadQuestionsOfUser(feedbackFromName);

            return View(fsubmissionVM);
        }



        [HttpGet]
        public ActionResult FeedbackScheduling()
        {
            //List<int> lstLevelID = new List<int>();


            fsVM = _repoFeedback.GetUniqueLevelIDs();

            //List<SelectListItem> levelList = new List<SelectListItem>();

            //foreach (int level in lstLevelID)
            //{
            //    SelectListItem obj = new SelectListItem();
            //    obj.Text = level.ToString();
            //    obj.Value = level.ToString();
            //    levelList.Add(obj);
            //}

            //SelectListItem objStart = new SelectListItem();
            //objStart.Text = "Select a Level";
            //objStart.Value = "0";
            //levelList.Insert(0, objStart);

            //ViewData["LevelIDList"] = levelList;

            //fsVM.lstFeedBackCategoryLevel = _repoFeedback.GetFeedBackCategoryLevels(levelID);
            //fsVM.lstDesignations = _repoFeedback.GetDesignationBasedonLevelID(levelID);

            return View(fsVM);
        }


        [HttpGet]
        public JsonResult GetDesignations(int levelID)
        {
            //fsVM.LevelID = id;

            //HttpContext.Session.SetString("LevelID", id.ToString());

            //fsVM = _repoFeedback.GetDesignationBasedonLevelID(id);

            List<SelectListItem> lstDesignations = _repoFeedback.GetDesignationBasedonLevelID(levelID);

            return Json(lstDesignations);

        }

        [HttpGet]
        public JsonResult GetCategoryLevels(int levelID, int designationID)
        {
            //int LevelID = Convert.ToInt32(HttpContext.Session.GetString("LevelID").ToString());

            //fsVM = _repoFeedback.GetFeedBackCategoryLevels(LevelID);

            //HttpContext.Session.SetString("Designationid", Designationid.ToString());

            List<SelectListItem> lstCategoryLevels = _repoFeedback.GetFeedBackCategoryLevels(levelID);

            lstCategoryLevels.Add(new SelectListItem() { Text = "Please Select FeedBackCategoryLevel", Value = "0" });
            return Json(lstCategoryLevels);

        }


        [HttpPost]
        public IActionResult FeedbackScheduling(int levelID, int designationID, int FeedBackCategoryLevel)
        {
            HttpContext.Session.SetString("levelID", levelID.ToString());
            HttpContext.Session.SetString("designationID", designationID.ToString());
            HttpContext.Session.SetString("FeedBackCategoryLevel", FeedBackCategoryLevel.ToString());

            //fsDetails.lstFeedbackSchedulingDetails = _repoFeedback.PrepareFeedBackLists(levelID, designationID, FeedBackCategoryLevel);

            return RedirectToAction("FeedbackSchedulingResults");

            //return View();
        }

        [HttpGet]
        public IActionResult FeedbackSchedulingResults()
        {

            int LevelID = Convert.ToInt32(HttpContext.Session.GetString("levelID").ToString());
            int Designationid = Convert.ToInt32(HttpContext.Session.GetString("designationID").ToString());
            int FeedBackCategoryLevel = Convert.ToInt32(HttpContext.Session.GetString("FeedBackCategoryLevel").ToString());


            //List<FeedbackSchedulingDetail> lstfsDetail = new List<FeedbackSchedulingDetail>();

            //fsVM.lstFeedbackSchedulingDetails = _repoFeedback.PrepareFeedBackLists(LevelID, Designationid, FeedBackCategoryLevel);

            //return this.View("/FeedbackScheduling/FeedbackSchedulingResults");


            fsResultsVM = _repoFeedback.PrepareFeedBackLists(LevelID, Designationid, FeedBackCategoryLevel);

            fsResultsVM.lstFeedbackSSessions = _repoFeedback.LoadFeedbackSessions();


            //fsResultsVM.lstFeedbackSchedulingDetails = _repoFeedback.PrepareFeedBackLists(LevelID, Designationid, FeedBackCategoryLevel);

            return View(fsResultsVM);

        }

        [HttpPost]
        public IActionResult FeedbackSchedulingResults(FeedbackSchedulingResultsViewModel fsResultsVM)
        {
            string msg = string.Empty;

            //List<FeedbackSchedulingDetail> lstfsDetail = new List<FeedbackSchedulingDetail>();

            //fsResultsVM.lstFeedbackSchedulingDetails = lstfsDetail;

            msg = _repoFeedback.SaveFeedbackSchedulingResults(fsResultsVM);

            if (msg == "Success")
            {
                ViewBag.Info = "Details Saved Successfully";
                fsResultsVM.lstFeedbackSchedulingDetails = _repoFeedback.LoadFeedbackSchedulingResults();
                fsResultsVM.lstFeedbackSSessions = _repoFeedback.LoadFeedbackSessions();
            }

            if (msg == "Record Exists")
            {
                ViewBag.Info = "This schedule already exists";
            }


            else
            {
                ViewBag.Info = "Details not saved, please try again";
            }

            return View(fsResultsVM);
        }


    }
}
