using FeedForwardBusinessEntities.EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.ViewModels
{
    public class FeedbackSubmissionViewModel
    {
        public List<QuestionDetail> lstQuestions;

        public List<FeedbackCaptionsViewModel> lstfeedbackCaptions;

    }
}
