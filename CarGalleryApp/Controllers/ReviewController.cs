using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YipRestaurantApp.Models;
using YipRestaurantApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YipRestaurantApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService reviewService;
        public ReviewController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        // GET: ReviewController
        public ActionResult Index()
        {
            return View(reviewService.Get());
        }

        // GET: ReviewController/Details/ connects to the delete view

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = reviewService.Get(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // GET: ReviewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewController/Create connects to the create view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            UserModel aPerson = GetUserFromSession();
            review.Person = aPerson;

            if (ModelState.IsValid)
            {
                reviewService.Create(review);
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: ReviewController/Edit/ connects to the edit view
        public ActionResult Edit(string id)
        {
            UserModel aPerson = GetUserFromSession();

            if (id == null)
            {
                return NotFound();
            }
            var review = reviewService.Get(id);
            if (review == null)
            {
                return NotFound();
            }
            if (aPerson.FirstName != review.Person.FirstName
            || aPerson.Password != review.Person.Password)
            {
                ViewBag.LoginMessage = $"Sorry {aPerson.FirstName}, you cannot edit other Yip User Reviews!";
                return View("Details", review);
            }
            return View(review);
        }

        // POST: ReviewController/Edit/ connects to the edit view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Review review)
        {
            

            if (id != review.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                reviewService.Update(id, review);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(review);
            }
        }

        // GET: ReviewController/Delete/  Connects to Delete view
        public ActionResult Delete(string id)
        {
            UserModel aPerson = GetUserFromSession();
            if (id == null)
            {
                return NotFound();
            }

            var review = reviewService.Get(id);
            if (review == null)
            {
                return NotFound();
            }
            if (aPerson.FirstName != review.Person.FirstName //Checks if current user is the person who submitted review, if not send back to Detail with message
            || aPerson.Password != review.Person.Password)
            {
                ViewBag.LoginMessage = $"Sorry {aPerson.FirstName}, you cannot delete other Yip User Reviews!";
                return View("Details", review);
            }
            return View(review);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var review = reviewService.Get(id);

                if (review == null)
                {
                    return NotFound();
                }

                reviewService.Remove(review.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public UserModel GetUserFromSession() //Helper Method to read a user from Session and return a userModel
        {
            string name = HttpContext.Session.GetString("personFirst");
            string password = HttpContext.Session.GetString("personLast");

            UserModel aPerson = new UserModel(name, password);
            return aPerson;
        }
    }
}
