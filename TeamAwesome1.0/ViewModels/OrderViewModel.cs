using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamAwesome1._0.ViewModels
{
    public class OrderViewModel
    {
        public int AutoNumber { get; set; }
        public string OrderNo { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Die { get; set; }
        public int MachNo { get; set; }
        public int Std { get; set; }
        public string Act { get; set; }
        public Nullable<int> OnHrs { get; set; }
        public Nullable<int> Shift { get; set; }
        public string PartNo { get; set; }
        public string BoxCode { get; set; }
        public int PackerNo { get; set; }

        public string BoxSize { get; set; }
        public Nullable<int> PackerQty { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<int> BoxCount { get; set; }
        public int Adj { get; set; }
        public string PartDesc { get; set; }
}
}