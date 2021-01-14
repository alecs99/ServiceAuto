namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carModifier : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "UserId_Id", "dbo.IdentityUsers");
            DropIndex("dbo.Cars", new[] { "UserId_Id" });
            AlterColumn("dbo.Cars", "UserId_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Cars", "UserId_Id");
            AddForeignKey("dbo.Cars", "UserId_Id", "dbo.IdentityUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "UserId_Id", "dbo.IdentityUsers");
            DropIndex("dbo.Cars", new[] { "UserId_Id" });
            AlterColumn("dbo.Cars", "UserId_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Cars", "UserId_Id");
            AddForeignKey("dbo.Cars", "UserId_Id", "dbo.IdentityUsers", "Id", cascadeDelete: true);
        }
    }
}
