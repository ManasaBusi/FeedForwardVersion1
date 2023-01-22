using FeedForwardBusinessEntities.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.ViewModels
{
    public class FeedbackSchedulingViewModel
    {

       

        public List<LevelDetail> lstLevel { get; set; }

        public List<DesignationLevel> lstDesignation { get; set; }

        public List<FeedbackCategoryLevel> lstFeedBackCategoryLevel { get; set; }

        public int levelID;

        public int designationID;

        public int FeedBackCategoryLevel;





    }
}
