namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCarsModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reservations", new[] { "car_idCar" });
            DropIndex("dbo.AspNetUsers", new[] { "userCar_idCar" });
            RenameColumn(table: "dbo.Cars", name: "user_Id", newName: "UserId_Id");
            RenameIndex(table: "dbo.Cars", name: "IX_user_Id", newName: "IX_UserId_Id");
            AlterColumn("dbo.Cars", "Make", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Cars", "Model", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Reservations", "car_IdCar");
            CreateIndex("dbo.AspNetUsers", "userCar_IdCar");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "userCar_IdCar" });
            DropIndex("dbo.Reservations", new[] { "car_IdCar" });
            AlterColumn("dbo.Cars", "Model", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Cars", "Make", c => c.String(nullable: false, maxLength: 255));
            RenameIndex(table: "dbo.Cars", name: "IX_UserId_Id", newName: "IX_user_Id");
            RenameColumn(table: "dbo.Cars", name: "UserId_Id", newName: "user_Id");
            CreateIndex("dbo.AspNetUsers", "userCar_idCar");
            CreateIndex("dbo.Reservations", "car_idCar");
        }
    }
}
