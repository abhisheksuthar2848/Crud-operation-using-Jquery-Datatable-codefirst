namespace LoginService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestorecreatebystring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stores", "StoreCreatedBy", c => c.String());
            AlterColumn("dbo.Stores", "StoreEditedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stores", "StoreEditedBy", c => c.Int(nullable: false));
            AlterColumn("dbo.Stores", "StoreCreatedBy", c => c.Int(nullable: false));
        }
    }
}
