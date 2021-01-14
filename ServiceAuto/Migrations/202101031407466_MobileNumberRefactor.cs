namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MobileNumberRefactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MobileNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MobileNumber");
        }
    }
}
