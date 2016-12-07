using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamAwesome1._0.ViewModels
{
    public class LaborViewModel
    {
        public int LaborNo { get; set; }
        public string EmpID { get; set; }
        public string PartNo { get; set; }
        public string OrderNo { get; set; }
        public int Hours { get; set; }
        public int Mins { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
    }
}