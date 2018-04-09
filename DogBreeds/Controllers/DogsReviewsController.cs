using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DogBreeds.Models;

namespace DogBreeds.Controllers
{
    public class DogsReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DogsReviews
        public ActionResult Index()
        {
            return View(db.dogs.ToList());
        }

        // GET: DogsReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // GET: DogsReviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogsReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PetName,Type,BreedName")] Dog dog)
        {
            if (ModelState.IsValid)
            {
                db.dogs.Add(dog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dog);
        }

        // GET: DogsReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // POST: DogsReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PetName,Type,BreedName")] Dog dog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dog);
        }

        // GET: DogsReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // POST: DogsReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dog dog = db.dogs.Find(id);
            db.dogs.Remove(dog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [NonAction]
        private DogsReviewViewModel DogsReviewViewModel(DogReviews dogReviews)
        {
            List<DogsReviewViewModel> dogReviewsViewModel = new List<Models.DogsReviewViewModel>();

            //generate a dictionary with dog ids and names for lookup
            var dogNames = db.dogs.ToDictionary(d => d.Id, => d.Name);

            foreach (var dogsReviews in dogReviews)
            {
                dogReviewsViewModel.Add(new DogsReviewViewModel
                {
                    Id = dogReviews.Id,
                    DateCreated = dogReviews.DateCreated,
                    Content = dogReviews.Content,
                    DogId = dogReviews.DogId,
                    DogName = dogNames[dogReviews.DogId]
                });

                return dogReviewsViewModel;
            }           
            
          
        }

        //list of reviews for the dogs
        public ActionResult ListOfReviewsByDog(int Id)
        {
            var dogReviews = db.DogReviews
                .Where(ref => r.DogId == Id)
                .ToList();

            //get dog to pass
            var dogs = db.dogs.FirstOrDefault(d => d.Id == Id);
            ViewBag.Dogs = dogs;

            if (Dog != null)
            {
                return View(dogReviews);
            }
            else
            {
                //redirect to error page with error message
                ViewBag.Errormessage = "Dog not found.";
                return View("Error");
            }
        }
    }
}
