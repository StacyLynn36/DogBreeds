using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DogBreeds.Models
{
    public class DogReview
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string PetName { get; set; }
        public int DogId { get; set; }
        public DateTime DateCreated { get; set; }
    }
    
}
