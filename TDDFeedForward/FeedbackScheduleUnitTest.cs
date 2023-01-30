using FeedForward.Controllers;
using FeedForwardBusinessEntities.ViewModels;
using FeedForwardRepository.Abstract;
using FeedForwardRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TDDFeedForward
{
    [TestClass]
    public class FeedbackScheduleUnitTest
    {
        [TestMethod]
        public void TestFeedbackScheduleList()
        {
            IFeedback_Repository objrepoFeedback = new Feedback_repository();

            FeedbackSchedulingController objfeedbackScheduling = new FeedbackSchedulingController(objrepoFeedback);

            ViewResult result = objfeedbackScheduling.FeedbackScheduleList() as ViewResult;

            List<FeedbackSchedulingResultsViewModel> lstfsVM = result.Model as List<FeedbackSchedulingResultsViewModel>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(lstfsVM);
            Assert.IsTrue(lstfsVM.Count > 0);
            //Assert.IsTrue(lstInvalidNames.Count == 0);


        }
    }
}