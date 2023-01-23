using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplicationProject2.Models;
using System.Globalization;

namespace WebApplicationProject2.Controllers
{
    public class HomeController : Controller
    {
        public static string saveCompanyName;

        public ViewResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection form)
        {
            CompanyInformation3Entities db = new CompanyInformation3Entities();
            Complaint modell = new Complaint();
            modell.Email = form["email"];
            modell.Message = form["message"];

            db.Complaint.Add(modell);
            db.SaveChanges();

            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(FormCollection form)
        {
            CompanyInformation3Entities db = new CompanyInformation3Entities();
            Companies model = new Companies();
            model.CompanyName = form["CompanyName"].Trim();
            model.Password = form["Password"].Trim();
            string dateString = form["CompanyEstablishmentDate"];
            DateTime date;
            if (DateTime.TryParse(dateString, out date))
            {
                model.CompanyEstablishmentDate = date;
            }
            model.CompanyCountry = form["CompanyCountry"].Trim();
            model.CompanyCity = form["CompanyCity"].Trim();
            model.CompanyPhone = form["CompanyPhone"].Trim();
            model.CompanyEmail = form["CompanyEmail"].Trim();
            model.CompanyType = form["CompanyType"];
            db.Companies.Add(model);
            db.SaveChanges();
            if (model != null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Registration");
            }
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string CompanyName, string Password)
        {
            CompanyInformation3Entities db = new CompanyInformation3Entities();
            Companies model = new Companies();
            model.CompanyName = CompanyName.Trim();
            model.Password = Password.Trim();
            var user = db.Companies.Where(x => x.CompanyName == model.CompanyName && x.Password == model.Password).FirstOrDefault();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.CompanyName, false);
                Session["user"] = user.CompanyName;
                saveCompanyName = user.CompanyName;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
        }

        public static string successMessage;
        [Authorize]
        public ActionResult EditProfile()
        {

            if (saveCompanyName == null)
            {
                return RedirectToAction("Login");
            }

            Companies companies = new Companies();
            return View(companies);
        }

        [HttpPost]
        public ActionResult EditProfile(Companies companies)
        {
            int result = 0;
            try
            {
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CompanyInformation3;Integrated Security=True"; ;

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("UPDATE Companies SET CompanyName = '" + companies.CompanyName + "',Password = '" + companies.Password + "'" +
                        ",CompanyPhone = '" + companies.CompanyPhone + "',CompanyEmail = '" + companies.CompanyEmail + "' WHERE CompanyName = '" + saveCompanyName + "'", connection);
                    result = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }

            if (result != 0)
            {
                saveCompanyName = companies.CompanyName;
                successMessage = "1";
            }
            else
            {
                successMessage = "0";
            }

            return RedirectToAction("EditProfile");
        }

    }
}