namespace DogBreeds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BreedName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Type");
            DropColumn("dbo.AspNetUsers", "BreedName");
        }
    }
}
