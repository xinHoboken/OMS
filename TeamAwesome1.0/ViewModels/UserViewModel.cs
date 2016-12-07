using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamAwesome1._0.ViewModels
{
    public class UserViewModel
    {
        public string EmpID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Desc { get; set; }
        public int DeptNo { get; set; }
        public string LoginID { get; set; }
        public Nullable<int> UserTypeNo { get; set; }

        public string Password { get; set; }
        public string DeptName { get; set; }
        public string UserTypeName { get; set; }
        public Nullable<bool> AR { get; set; }
        public Nullable<bool> PR { get; set; }

    }
}