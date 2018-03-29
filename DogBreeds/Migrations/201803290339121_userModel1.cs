namespace DogBreeds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            DropColumn("dbo.AspNetUsers", "BreedName");
            DropColumn("dbo.AspNetUsers", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "BreedName", c => c.String());
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
