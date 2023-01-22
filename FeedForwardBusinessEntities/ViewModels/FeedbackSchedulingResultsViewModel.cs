﻿using FeedForwardBusinessEntities.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.ViewModels
{
    public class FeedbackSchedulingResultsViewModel
    {
        public List<FeedbackSchedulingDetail> lstFeedbackSchedulingDetails { get; set; }

        public List<FeedbackSession> lstFeedbackSSessions { get; set; }

        public string feedbackToUserID { get; set; }

        public string feedbackToName { get; set; }

        public string feedbackFromUserID { get; set; }

        public string feedbackFromName { get; set; }

        public int FeedbackSSessionID { get; set; }

        public bool? IsCompleted { get; set; }
    }
}