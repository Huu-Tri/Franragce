namespace fragrance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addoriginprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.product", "price_origin", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.product", "price_origin");
        }
    }
}
