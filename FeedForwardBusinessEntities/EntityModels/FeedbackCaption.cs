using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.EntityModels
{
    [Table("FeedbackCaptions")]
    public class FeedbackCaption
    {
        [Key]
        public int FCID { get; set; }
        public string FCDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
