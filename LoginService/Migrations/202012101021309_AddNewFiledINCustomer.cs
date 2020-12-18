namespace LoginService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFiledINCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CustomerEmail", c => c.String());
            AlterColumn("dbo.Stores", "StoreEditedBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stores", "StoreEditedBy", c => c.String());
            DropColumn("dbo.Customers", "CustomerEmail");
        }
    }
}
