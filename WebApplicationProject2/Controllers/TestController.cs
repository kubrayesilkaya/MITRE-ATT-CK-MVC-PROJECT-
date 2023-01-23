using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using WebApplicationProject2.Models;

namespace WebApplicationProject2.Controllers
{
    public class TestController : Controller
    {

        public static string T1047issuccessed;
        public static string T1518issuccessed;

        public ActionResult T1047()
        {
            if (HomeController.saveCompanyName == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public ActionResult DoT1047()
        {
            Process.Start(@"cmd.exe", "/c D: & wmic useraccount get /ALL /format:csv  >>result.txt");
            string result = TakeAttackResult();

            int myresult = 0;
            try
            {
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CompanyInformation3;Integrated Security=True";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO T1047(AttackResult,CompanyName) " +
                        "VALUES('" + result + "','" + HomeController.saveCompanyName + "')", connection);
                    myresult = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }

            if (myresult != 0)
            {
                T1047issuccessed = "1";
            }
            else
            {
                T1047issuccessed = "0";
            }

            return RedirectToAction("T1047");
        }

        public ActionResult T1518()
        {
            if (HomeController.saveCompanyName == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult DoT1518()
        {
            Process.Start(@"C:\Windows\System32\cmd.exe", "/c D: & reg query \"HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Internet Explorer\" /v svcVersion >>result.txt");
            string result = TakeAttackResult(); 

            int myresult = 0;
            try
            {
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=CompanyInformation3;Integrated Security=True";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    SqlCommand command = new SqlCommand("INSERT INTO T1518(AttackResult,CompanyName) " +
                        "VALUES('" + result + "','" + HomeController.saveCompanyName + "')", connection);
                    myresult = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { }

            if (myresult != 0)
            {
                T1518issuccessed = "1";
            }
            else
            {
                T1518issuccessed = "0";
            }

            return RedirectToAction("T1518");
        }

        public ActionResult TestResults()
        {
            if (HomeController.saveCompanyName == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult TestResultsT1047()
        {
            if (HomeController.saveCompanyName == null)
            {
                return RedirectToAction("Login", "Home");
            }
            CompanyInformation3Entities db = new CompanyInformation3Entities();

            List<T1047> t1047List = new List<T1047>();
            foreach (T1047 i in db.T1047)
            {
                if (i.CompanyName == HomeController.saveCompanyName)
                {
                    t1047List.Add(i);
                }
            }

            return View(t1047List);
        }
        public ActionResult TestResultsT1518()
        {
            if (HomeController.saveCompanyName == null)
            {
                return RedirectToAction("Login", "Home");
            }
            CompanyInformation3Entities db = new CompanyInformation3Entities();

            List<T1518> t1518List = new List<T1518>();
            foreach (T1518 i in db.T1518)
            {
                if (i.CompanyName == HomeController.saveCompanyName)
                {
                    t1518List.Add(i);
                }
            }

            return View(t1518List);
        }

        public string TakeAttackResult()
        {
            string myFileData = "";   
            string[] lines = null;    

            while (true) 
            {
                try
                {
                    lines = System.IO.File.ReadAllLines(@"D://result.txt"); 
                    System.IO.File.Delete(@"D://result.txt");
                    break;
                }
                catch { } 
            }

            foreach (string line in lines)  
            {
                myFileData += line; 
            }

            return myFileData; 
        }

    }
}