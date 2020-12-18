namespace LoginService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chagedatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stores", "StoreCreatedBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stores", "StoreCreatedBy", c => c.String());
        }
    }
}
