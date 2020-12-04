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
        private readonly CarService carService;

        public HomeController(CarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Index()//Views/Home/view
        {
            return View();
        }

        public IActionResult LogIn(UserModel userModel)
        {
            UserModel um = new UserModel();
            um.FirstName = userModel.FirstName;
            um.Password = userModel.Password;
            bool myVar = carService.Get(um.FirstName, um.Password);
            if (!myVar)
            {
                ViewBag.LoginMessage = "Not a valid email or password. Try agin please. ";
                return View("Index");
            }
            HttpContext.Session.SetString("personFirst", um.FirstName);
            HttpContext.Session.SetString("personLast", um.Password);

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
