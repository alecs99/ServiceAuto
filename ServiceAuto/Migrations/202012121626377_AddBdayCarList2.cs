namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBdayCarList2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Cars", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "userCar_idCar", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "userCar_idCar");
            AddForeignKey("dbo.AspNetUsers", "userCar_idCar", "dbo.Cars", "idCar");
            DropColumn("dbo.Cars", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "userCar_idCar", "dbo.Cars");
            DropIndex("dbo.AspNetUsers", new[] { "userCar_idCar" });
            DropColumn("dbo.AspNetUsers", "userCar_idCar");
            CreateIndex("dbo.Cars", "ApplicationUser_Id");
            AddForeignKey("dbo.Cars", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
