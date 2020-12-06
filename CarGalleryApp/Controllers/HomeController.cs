using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YipRestaurantApp.Models;
using YipRestaurantApp.Services;

namespace YipRestaurantApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReviewService carService;

        public HomeController(ReviewService carService)
        {
            this.carService = carService;
        }

        public IActionResult Index()//Views/Home/view
        {
            return View();
        }

        //Post Method, passing a userModel to Login
        public IActionResult LogIn(UserModel userModel) //Requires a userModel, firstName and password
        {
            UserModel um = new UserModel();
            um.FirstName = userModel.FirstName;
            um.Password = userModel.Password;

            bool myVar = carService.Get(um.FirstName, um.Password); //Calls a Method in CarService to start connection to Mongo, returns a Bool, True if in DB

            if (!myVar) //Failed Authentication, send back to Login, clear fields
            {
                ModelState.Clear(); 
                UserModel ObjContact = new UserModel() //Create a new userModel, set to an empty string
                {
                    FirstName = string.Empty,
                    Password = string.Empty,
                };
                ViewBag.LoginMessage = "Not a valid email or password. Try agin please. ";
                return View("Index", ObjContact);
            }
            HttpContext.Session.SetString("personFirst", um.FirstName); //Succsessfull Authentication, save to Session so we can use elsewhere in Application
            HttpContext.Session.SetString("personLast", um.Password);

            Console.WriteLine($"Login Successful! {um.FirstName}");
            ViewBag.firstName = um.FirstName;
            return View("Landing"); //Send user to Landing Page
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
