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
        private readonly CarService carService;
        public ReviewController(CarService carService)
        {
            this.carService = carService;
        }
        // GET: ReviewController
        public ActionResult Index()
        {
            return View(carService.Get());
        }

        // GET: ReviewController/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = carService.Get(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // GET: ReviewController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review car)
        {
            if (ModelState.IsValid)
            {
                carService.Create(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: ReviewController/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = carService.Get(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: ReviewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Review car)
        {
            if(id != car.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                carService.Update(id, car);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(car);
            }
        }

        // GET: ReviewController/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = carService.Get(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: ReviewController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var car = carService.Get(id);

                if (car == null)
                {
                    return NotFound();
                }

                carService.Remove(car.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
