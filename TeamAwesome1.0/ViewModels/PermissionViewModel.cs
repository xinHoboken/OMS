using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamAwesome1._0.ViewModels
{
    public class PermissionViewModel
    {
        public int UserTypeNo { get; set; }
        public string UserTypeName { get; set; }
        public Nullable<bool> AR { get; set; }
        public Nullable<bool> PR { get; set; }
    }
}