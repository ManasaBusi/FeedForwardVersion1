using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.EntityModels
{
    public class SearchModel
    {
        public List<UserDetail>? lstUsrDetails;

        public string? searchQuery { get; set; }
    }
}
