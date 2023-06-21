using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Crud.Models
{
    public class StudentViewModel
    {
        public int Studentld { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}