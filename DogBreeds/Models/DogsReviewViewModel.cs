using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DogBreeds.Models
{
    public class DogsReviewViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "0:d", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Content { get; set; }

        public int DogId { get; set; }

        [Display(Name = "Dog")]
        public string DogName { get; set; }
    }
}