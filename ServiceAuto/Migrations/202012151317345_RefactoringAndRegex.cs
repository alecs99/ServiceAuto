namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringAndRegex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reservations", new[] { "car_IdCar" });
            DropIndex("dbo.Reservations", new[] { "service_idService" });
            DropIndex("dbo.MechanicsReservations", new[] { "Mechanics_idMechanic" });
            DropIndex("dbo.MechanicsReservations", new[] { "Reservations_idReservation" });
            AddColumn("dbo.Mechanics", "FirstName", c => c.String(nullable: false, maxLength: 21));
            AlterColumn("dbo.Cars", "Make", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Mechanics", "LastName", c => c.String(nullable: false, maxLength: 21));
            AlterColumn("dbo.Services", "ServiceName", c => c.String(nullable: false, maxLength: 21));
            CreateIndex("dbo.Reservations", "Car_IdCar");
            CreateIndex("dbo.Reservations", "Service_IdService");
            CreateIndex("dbo.MechanicsReservations", "Mechanics_IdMechanic");
            CreateIndex("dbo.MechanicsReservations", "Reservations_IdReservation");
            DropColumn("dbo.Mechanics", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mechanics", "name", c => c.String(nullable: false, maxLength: 255));
            DropIndex("dbo.MechanicsReservations", new[] { "Reservations_IdReservation" });
            DropIndex("dbo.MechanicsReservations", new[] { "Mechanics_IdMechanic" });
            DropIndex("dbo.Reservations", new[] { "Service_IdService" });
            DropIndex("dbo.Reservations", new[] { "Car_IdCar" });
            AlterColumn("dbo.Services", "ServiceName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Mechanics", "LastName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Cars", "Make", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Mechanics", "FirstName");
            CreateIndex("dbo.MechanicsReservations", "Reservations_idReservation");
            CreateIndex("dbo.MechanicsReservations", "Mechanics_idMechanic");
            CreateIndex("dbo.Reservations", "service_idService");
            CreateIndex("dbo.Reservations", "car_IdCar");
        }
    }
}
