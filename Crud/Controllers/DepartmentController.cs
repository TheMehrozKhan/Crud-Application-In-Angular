using Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crud.Controllers
{
    public class DepartmentController : Controller
    {
        crud_with_angular_ajaxEntities db = new crud_with_angular_ajaxEntities();

        public ActionResult Index()
        {
            List<tbl_department> deplist = db.tbl_department.ToList();
            ViewBag.dep_list = new SelectList(deplist, "DepartmentId", "DepartmentName");
            return View();
        }

        public JsonResult GetStudentList()
        {
            List<StudentViewModel> Stulist = db.tbl_Student
                .Where(x => x.isDeleted == false)
                .Select(x => new StudentViewModel
                {
                    Studentld = x.StudentId,
                    StudentName = x.StudentName,
                    StudentEmail = x.StudentEmail,
                    DepartmentName = x.tbl_department.DepartmentName
                })
                .ToList();

            return Json(Stulist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(StudentViewModel model)
        {
            var result = false;
            try
            {
                if (model.Studentld > 0)
                {
                    tbl_Student Stu = db.tbl_Student.SingleOrDefault(x => x.isDeleted == false && x.StudentId == model.Studentld);
                    Stu.StudentName = model.StudentName;
                    Stu.StudentEmail = model.StudentEmail;
                    Stu.DepartmentId = model.DepartmentId; 
                    db.SaveChanges();
                }
                else
                {
                    result = true;
                    tbl_Student Stu = new tbl_Student(); 
                    Stu.StudentName = model.StudentName; 
                    Stu.StudentEmail = model.StudentEmail;
                    Stu.DepartmentId = model.DepartmentId; 
                    Stu.isDeleted = false;
                    db.tbl_Student.Add(Stu);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch ( Exception ex)
            {
                throw;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}