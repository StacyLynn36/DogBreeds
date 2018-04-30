namespace DogBreeds.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DogBreeds.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DogBreeds.Models.ApplicationDbContext context)
        {
            context.dogs.AddOrUpdate(b => b.PetName,
                 new Models.Dog()
                 {
                     PetName = "Chihuahua",
                     Type = Models.GroupType.Toy,
                     BreedName = "dog"
                 },

                  new Models.Dog()
                  {
                      PetName = "Irish Wolfhound",
                      Type = Models.GroupType.Hounds,
                      BreedName = "dog"
                  }

               );
        }
    }
}
