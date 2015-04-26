namespace SpajamMadobenWebAPI.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Service
    {
        public Service() { }

        public string CardID { get; set; }
        public string CustomerID { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public Nullable<int> EmployeeID { get; set; }
    }
}
