using FeedForwardBusinessEntities.EntityContext;
using FeedForwardBusinessEntities.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedForwardBusinessEntities.ViewModels
{

    public class UserDetailViewModel
    {

        public int ID { get; set; }

        //[Required(ErrorMessage = "UserID is mandatory")]
        //[MinLength(6, ErrorMessage = "UserID must be at least 6 characters long")]
        //[StringLength(50, ErrorMessage = "UserID should be only 50 characters long")]
        public string UserID { get; set; }

        public string? Password { get; set; }

        //[MinLength(6, ErrorMessage = "Name must be at least 5 characters long")]
        //[StringLength(50, ErrorMessage = "Name should be only 100 characters long")]
        //[RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Email is mandatory")]
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,3})$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "EmpID is mandatory")]
        //[RegularExpression(@"[0-9]+", ErrorMessage = "EmpID is invalid")]
        public string EmpID { get; set; }

        //[Required(ErrorMessage = "Mobile is mandatory")]
        //[RegularExpression(@"[0-9]+", ErrorMessage = "Mobile is invalid")]

        public string DOB { get; set; }
        public string Mobile { get; set; }

        public DateTime? PasswordChangeDate { get; set; }

        //[Required(ErrorMessage = "Role is mandatory")]
        ////[RegularExpression(@"^(Select Role)", ErrorMessage = "Role is invalid")]
        public int RoleID { get; set; }
        public List<RoleDetail>? RoleList { get; set; }

        //[Required(ErrorMessage = "Designation is mandatory")]
        
        public int DesignationID { get; set; }

        public List<DesignationLevel>? DesignationList { get; set; }

        public List<UserDetail>? lstUsrDetails;

        public string? searchQuery { get; set; }

    }
}
