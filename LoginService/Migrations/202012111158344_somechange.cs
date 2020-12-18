namespace LoginService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "InvoiceDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "InvoiceDate", c => c.DateTime(nullable: false));
        }
    }
}
