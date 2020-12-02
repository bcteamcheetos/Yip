using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YipRestaurantApp.Models;

namespace YipRestaurantApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()//Views/Home/view
        {
            return View();
        }

        public IActionResult LogIn(UserModel userModel)
        {
            //    //Create a DALPerson Object
            //    //use the DALPerson.CheckLogInCred method to check if the credentials are valid
            //    //if statement
            //    //personModel==null --login fail
            //    //else save the personID, user's first name on the session
            //    //DALPerson dp = new DALPerson(_configuration);
            UserModel um = new UserModel();
            um.FirstName = userModel.FirstName;
            um.LastName = userModel.LastName;
            HttpContext.Session.SetString("personFirst", um.FirstName);
            HttpContext.Session.SetString("personLast", um.LastName);
            //    //pm = dp.CheckLogInCredentials(logInCredentialsModel);
            //    //if (pm == null)
            //    //{
            //    //    ViewBag.LoginMessage = "Login Failed!";
            //    //}
            //    //else
            //    //{
            //    //    HttpContext.Session.SetString("personID", pm.PersonID);
            //    //    HttpContext.Session.SetString("userName", pm.UserName);
            //    //    HttpContext.Session.SetString("firstName", pm.FName);
            //    //    ViewBag.userFirstName = pm.FName;
            //    //    ViewBag.Display = false;
            //    //}
            Console.WriteLine($"Login Successful! {um.FirstName}");
            ViewBag.firstName = um.FirstName;
            return View("Landing");
        }
        public IActionResult Landing()
        {
            //This method ensures that the user's first name is pulled when clicking on the "Home" button
            string fName = HttpContext.Session.GetString("personFirst");
            string lName = HttpContext.Session.GetString("personLast");

            if (fName != null)
            {
                ViewBag.firstName = fName;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
