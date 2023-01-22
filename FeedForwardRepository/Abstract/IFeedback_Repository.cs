using FeedForwardBusinessEntities.EntityModels;
using FeedForwardBusinessEntities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardRepository.Abstract
{
    public interface IFeedback_Repository
    {
        public FeedbackSchedulingViewModel GetUniqueLevelIDs();

        public List<SelectListItem> GetFeedBackCategoryLevels(int levelID);

        public List<SelectListItem> GetDesignationBasedonLevelID(int levelID);

        //public List<FeedbackSchedulingDetail> PrepareFeedBackLists(int levelID, int Designationid, int FeedBackCategoryLevel);

        public FeedbackSchedulingResultsViewModel PrepareFeedBackLists(int levelID, int Designationid, int FeedBackCategoryLevel);

        //public List<FeedbackSchedulingDetail> PrepareFeedBackLists(int levelID, int Designationid, int FeedBackCategoryLevel);

        public string SaveFeedbackSchedulingResults(FeedbackSchedulingResultsViewModel fsResultsVM);

        public List<FeedbackSession> LoadFeedbackSessions();

        public List<FeedbackSchedulingDetail> LoadFeedbackSchedulingResults();
    }
}
