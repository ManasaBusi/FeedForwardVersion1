using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.EntityModels
{
    [Table("FeedbackSchedulingDetails")]
    public class FeedbackSchedulingDetail
    {
        [Key]
        public int FeedbackSchID { get; set; }
        public string feedbackToUserID { get; set; }

        public string feedbackToName { get; set; }

        public string feedbackFromUserID { get; set; }

        public string feedbackFromName { get; set; }

        public bool? IsCompleted { get; set; }

        public int FeedbackSessionSID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
