using FeedForwardBusinessEntities.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.ViewModels
{
    public class QuestionLevelViewModel
    {
        public string Question { get; set; }
        public List<int> LevelIDList { get; set; }

        public int QuestionID { get; set; }

        public int LevelID { get; set; }

        public List<QuestionDetail> QuestionDetailsList { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
