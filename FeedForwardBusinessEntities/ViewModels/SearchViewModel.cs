using FeedForwardBusinessEntities.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.ViewModels
{
    public class SearchViewModel
    {

        public List<UserDetail>? lstUsrDetails;

        public string? searchQuery { get; set; }
    }
}
