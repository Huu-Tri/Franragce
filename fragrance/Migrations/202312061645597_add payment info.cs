namespace fragrance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpaymentinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.user_order", "code", c => c.String());
            AddColumn("dbo.user_order", "payment_method", c => c.String());
            AddColumn("dbo.user_order", "payment_status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.user_order", "payment_status");
            DropColumn("dbo.user_order", "payment_method");
            DropColumn("dbo.user_order", "code");
        }
    }
}
