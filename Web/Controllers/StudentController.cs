using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entities;
using Data.Enum;
using Microsoft.Practices.Unity;
using Service.Interface;

namespace Web.Controllers
{
    /// <summary>
    /// 学生控制器
    /// </summary>
    public class StudentController : Controller
    {
        [Dependency]
        public IStudentService StudentService { get; set; }


        public ActionResult Index()
        {
            var list = StudentService.FindAll(null);

            for (int i = 0; i < 10; i++)
            {
                Student student = new Student()
                {
                    Name = "小明",
                    Age = 22,
                    Sex = (byte)Sex.男
                };
                StudentService.Add(student); 
            }
            return View();
        }
    }
}