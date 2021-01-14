namespace ServiceAuto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfileModifications : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Make", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Cars", "Model", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "Model", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Cars", "Make", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
