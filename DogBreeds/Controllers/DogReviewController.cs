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
    public class DogReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DogReview
        public ActionResult Index()
        {
            return View(BuildDogsReviewViewModel(db.DogReviews.ToList()));
        }

        // GET: DogReview/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogReview dogReview = db.DogReviews.Find(id);
            DogsReviewViewModel dogsReviewViewModel = DogsReviewViewModel(dogReview);
            if (dogReview == null)
            {
                return HttpNotFound();
            }
            return View(dogsReviewViewModel);
        }

        // GET: DogReview/Create
        public ActionResult Create()
        {
            //generate select list with ids for dogs dropdown
            var dogList = db.dogs.Select(d => d);
            ViewBag.SelectDogList = new SelectList(dogList, "Id", "PetName");
            return View();
        }

        // POST: DogReview/Create
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
         //GET: user create dog review
         public ActionResult UserCreate()
        {
            return View();
        }

        //POST: user create dog review
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind(Include ="Id,DateCreated,Content,DogID")] DogReview dogReview)
        {
            if (ModelState.IsValid)
            {
                db.DogReviews.Add(dogReview);
                db.SaveChanges();
                return RedirectToAction("ListOfReviewsByDog", new { id = dogReview.DogId });
            }

            return View(dogReview);
        }

        // GET: DogReview/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogReview dogReview = db.DogReviews.Find(id);
            DogsReviewViewModel dogsReviewViewModel = DogsReviewViewModel(dogReview);
            if (dogReview == null)
            {
                return HttpNotFound();
            }
            return View(dogReview);
        }

        // POST: DogReview/Edit/5
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

        // GET: DogReview/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DogReview dogReview = db.DogReviews.Find(id);
            DogsReviewViewModel dogsReviewViewModel = DogsReviewViewModel(dogReview);
            if (dogReview == null)
            {
                return HttpNotFound();
            }
            return View(dogsReviewViewModel);
        }

        // POST: DogReview/Delete/5
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
        private DogsReviewViewModel DogsReviewViewModel(DogReview dogReview)
        {
            
            //generate a dictionary with brewery ids and names for lookup
            var dogNames = db.dogs.ToDictionary(d => d.Id, d => d.PetName);

            return new DogsReviewViewModel()
            {
                Id = dogReview.Id,
                DateCreated = dogReview.DateCreated,
                Content = dogReview.Content,
                DogId = dogReview.DogId,
                DogName = dogNames[dogReview.DogId]
            };
        }

        [NonAction]
        private List<DogsReviewViewModel> BuildDogsReviewViewModel(List<DogReview> dogReviews)
        {
            List<DogsReviewViewModel> dogsReviewViewModel = new List<DogsReviewViewModel>();
            //generate a dictionary with dog ids and names for lookup
            var dogNames = db.dogs.ToDictionary(d => d.Id, d => d.PetName);

            foreach (var dogReview in dogReviews)
            {
                dogsReviewViewModel.Add(new DogsReviewViewModel
                {
                    Id = dogReview.Id,
                    DateCreated = dogReview.DateCreated,
                    Content = dogReview.Content,
                    DogId = dogReview.DogId,
                    DogName = dogNames[dogReview.DogId]
                });
                
            }
            return dogsReviewViewModel;
        }

        //list of reviews for a given dog
        public ActionResult ListOfReviewsByDogs(int id)
        {
            var dogReviews = db.DogReviews
                .Where(r => r.DogId == id)
                .ToList();

            int Id = 0;
            //get dog to pass
            var dogs = db.dogs.FirstOrDefault(d => d.Id == Id);
            ViewBag.dogs = dogs;

            if (dogs != null)
            {
                return View(dogReviews);
            }
            else
            {
                //redirect to error page with error message
                ViewBag.ErrorMessage = "Dog Not Found.";
                return View("Error");
            }
        }

    }
}
