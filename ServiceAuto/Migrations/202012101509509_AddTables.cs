namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        idCar = c.Int(nullable: false, identity: true),
                        registrationNumber = c.String(nullable: false),
                        make = c.String(nullable: false, maxLength: 255),
                        model = c.String(nullable: false, maxLength: 255),
                        fuel = c.String(nullable: false, maxLength: 10),
                        user_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.idCar)
                .ForeignKey("dbo.IdentityUsers", t => t.user_Id, cascadeDelete: true)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        idReservation = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        car_idCar = c.Int(nullable: false),
                        service_idService = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idReservation)
                .ForeignKey("dbo.Cars", t => t.car_idCar, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.service_idService, cascadeDelete: true)
                .Index(t => t.car_idCar)
                .Index(t => t.service_idService);
            
            CreateTable(
                "dbo.Mechanics",
                c => new
                    {
                        idMechanic = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 255),
                        lastName = c.String(nullable: false, maxLength: 255),
                        salary = c.Single(nullable: false),
                        bonus = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.idMechanic);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        idService = c.Int(nullable: false, identity: true),
                        serviceName = c.String(nullable: false, maxLength: 255),
                        servicePrice = c.Single(nullable: false),
                        executionTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idService);
            
            CreateTable(
                "dbo.IdentityUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.MechanicsReservations",
                c => new
                    {
                        Mechanics_idMechanic = c.Int(nullable: false),
                        Reservations_idReservation = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Mechanics_idMechanic, t.Reservations_idReservation })
                .ForeignKey("dbo.Mechanics", t => t.Mechanics_idMechanic, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.Reservations_idReservation, cascadeDelete: true)
                .Index(t => t.Mechanics_idMechanic)
                .Index(t => t.Reservations_idReservation);
            
            AddColumn("dbo.AspNetUserRoles", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserClaims", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserLogins", "IdentityUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String());
            CreateIndex("dbo.AspNetUserClaims", "IdentityUser_Id");
            CreateIndex("dbo.AspNetUserLogins", "IdentityUser_Id");
            CreateIndex("dbo.AspNetUserRoles", "IdentityUser_Id");
            CreateIndex("dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Id", "dbo.IdentityUsers", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUsers", "Id");
            DropColumn("dbo.AspNetUsers", "Email");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            DropForeignKey("dbo.AspNetUserRoles", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserLogins", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUserClaims", "IdentityUser_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Cars", "user_Id", "dbo.IdentityUsers");
            DropForeignKey("dbo.Reservations", "service_idService", "dbo.Services");
            DropForeignKey("dbo.MechanicsReservations", "Reservations_idReservation", "dbo.Reservations");
            DropForeignKey("dbo.MechanicsReservations", "Mechanics_idMechanic", "dbo.Mechanics");
            DropForeignKey("dbo.Reservations", "car_idCar", "dbo.Cars");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            DropIndex("dbo.MechanicsReservations", new[] { "Reservations_idReservation" });
            DropIndex("dbo.MechanicsReservations", new[] { "Mechanics_idMechanic" });
            DropIndex("dbo.AspNetUserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.IdentityUsers", "UserNameIndex");
            DropIndex("dbo.Reservations", new[] { "service_idService" });
            DropIndex("dbo.Reservations", new[] { "car_idCar" });
            DropIndex("dbo.Cars", new[] { "user_Id" });
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUserLogins", "IdentityUser_Id");
            DropColumn("dbo.AspNetUserClaims", "IdentityUser_Id");
            DropColumn("dbo.AspNetUserRoles", "IdentityUser_Id");
            DropTable("dbo.MechanicsReservations");
            DropTable("dbo.IdentityUsers");
            DropTable("dbo.Services");
            DropTable("dbo.Mechanics");
            DropTable("dbo.Reservations");
            DropTable("dbo.Cars");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
