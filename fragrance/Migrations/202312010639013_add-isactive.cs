namespace fragrance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisactive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.collection", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.product_type", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.menus", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.menus", "IsActive");
            DropColumn("dbo.product_type", "IsActive");
            DropColumn("dbo.collection", "IsActive");
        }
    }
}
