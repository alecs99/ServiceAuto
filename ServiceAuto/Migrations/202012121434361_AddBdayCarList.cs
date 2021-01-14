namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBdayCarList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "BirthDay", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "ApplicationUser_Id");
            AddForeignKey("dbo.Cars", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Cars", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "BirthDay");
            DropColumn("dbo.Cars", "ApplicationUser_Id");
        }
    }
}
