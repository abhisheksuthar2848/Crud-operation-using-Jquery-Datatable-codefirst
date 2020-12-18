namespace LoginService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stores", "StoreCreatedDate", c => c.DateTime());
            AlterColumn("dbo.Stores", "StoreEditTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stores", "StoreEditTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Stores", "StoreCreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
