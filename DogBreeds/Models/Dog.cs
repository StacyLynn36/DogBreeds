using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DogBreeds.Models
{
    public class Dog
    {
        public int Id { get; set; }

        //[Required]
        //[MinLength(5, ErrorMessage = "Dog breed names must be at least 5 characters.")]
        public string PetName { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public GroupType Type { get; set; }

        [Required]
        [Display(Name = "Breed Name")]
        public string BreedName { get; set; }

        

    }
}