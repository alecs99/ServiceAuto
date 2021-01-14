namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfile : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "userCar_IdCar" });
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            CreateIndex("dbo.AspNetUsers", "UserCar_IdCar");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "UserCar_IdCar" });
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            CreateIndex("dbo.AspNetUsers", "userCar_IdCar");
        }
    }
}
