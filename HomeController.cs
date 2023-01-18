using ProjectRevision1.DB_Connection;
using ProjectRevision1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectRevision1.Controllers
{
 [Authorize]  
    public class HomeController : Controller
    {

        //----------------Read Data from table------------------
        public ActionResult Index()
        {
            REVEntities2 DB = new REVEntities2();
            var Manish = DB.Employees.ToList();
            List<EmpModel> obj1 = new List<EmpModel>();
            foreach (var item in Manish)
            {
                obj1.Add(new EmpModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Adress = item.Adress,
                    Company = item.Company,
                    Salary = item.Salary,
                    Email = item.Email,
                    Phone = item.Phone,
                    Addhar = item.Addhar
                }); 

            }
            return View(obj1);
        }
    
//-------------Delete Operation--------------------
        public ActionResult Delete(int Id)
        {
            REVEntities2 DB = new REVEntities2();
            var deletitem = DB.Employees.Where(a => a.Id == Id).First();
            DB.Employees.Remove(deletitem);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }
//--------------Add New Data in Table----------------
        [HttpGet]
        public ActionResult Emp()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Emp(EmpModel obj)
          {
            REVEntities2 DB = new REVEntities2();
            Employee obje = new Employee();
            obje.Id = obj.Id;
            obje.Name = obj.Name;
            obje.Age = obj.Age;
            obje.Adress = obj.Adress;
            obje.Company = obj.Company;
            obje.Salary = obj.Salary;
            obje.Email = obj.Email;
            obje.Phone = obj.Phone;
            obje.Addhar = obj.Addhar;

            if (obj.Id == 0)
            {
                DB.Employees.Add(obje);
                DB.SaveChanges();
            }
            else
            {
                DB.Entry(obje).State = System.Data.Entity.EntityState.Modified;
                DB.SaveChanges();
            }
            return RedirectToAction("Index");
        }
//-------------------Edit Operation------------------------
        public ActionResult Edit(int Id)
        {
            REVEntities2 DB = new REVEntities2();
            var edit = DB.Employees.Where(a => a.Id == Id).First();
            EmpModel obj = new EmpModel();

            obj.Id = edit.Id;
            obj.Name = edit.Name;
            obj.Age = edit.Age;
            obj.Adress = edit.Adress;
            obj.Company = edit.Company;
            obj.Salary = edit.Salary;
            obj.Email = edit.Email;
            obj.Phone= edit.Phone;
            obj.Addhar = edit.Addhar;
            
            return View("Emp",obj);
        }
        //---------------Logien Page-------------------------
        [AllowAnonymous]
        [HttpGet] 
        public ActionResult Logien()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        
        public ActionResult Logien(LogienModel obj)
        {
            REVEntities2 DB = new REVEntities2();
            var res = DB.emplogiens.Where(a => a.Email == obj.Email).FirstOrDefault();
            if (res == null)
            {
                TempData["Invalid"] = "Email is Not found..!";
            }
            else
            {
                if(res.Email==obj.Email && res.password == obj.password)
                {
                    Session["UserEmail"] = res.Email;
                    Session["UserName"] = res.Name; 

                    FormsAuthentication.SetAuthCookie(res.Email, false);

                    return RedirectToAction("Index");
                }

                else
                {
                    TempData["InvalidPassword"] = "Invalid Password......!";
                }
            }
            return View();
        }

        //---------------Logout Page-------------------------
        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Logien");
        }

        //---------------About Page-------------------------
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        //---------------Contact Page-------------------------
        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}