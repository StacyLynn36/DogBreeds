namespace DogBreeds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class velis1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dogs", "PetName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dogs", "PetName", c => c.String(nullable: false));
        }
    }
}
