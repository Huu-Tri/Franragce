namespace fragrance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.acc_user",
                c => new
                    {
                        id_user = c.Int(nullable: false, identity: true),
                        name_user = c.String(nullable: false, maxLength: 255, unicode: false),
                        email_user = c.String(nullable: false, maxLength: 255, unicode: false),
                        phone_user = c.String(nullable: false, maxLength: 20, unicode: false),
                        password_user = c.String(nullable: false, maxLength: 255, unicode: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_user);
            
            CreateTable(
                "dbo.user_order",
                c => new
                    {
                        id_order = c.Int(nullable: false, identity: true),
                        receiver_oder = c.String(nullable: false, maxLength: 100, unicode: false),
                        status_order = c.String(nullable: false, maxLength: 100, unicode: false),
                        address_order = c.String(nullable: false, maxLength: 500, unicode: false),
                        date_order = c.DateTime(storeType: "date"),
                        phone_order = c.String(nullable: false, maxLength: 100, unicode: false),
                        action_order = c.Int(nullable: false),
                        id_order_user = c.Int(),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_order)
                .ForeignKey("dbo.acc_user", t => t.id_order_user)
                .Index(t => t.id_order_user);
            
            CreateTable(
                "dbo.order_details",
                c => new
                    {
                        id_order_dt = c.Int(nullable: false),
                        id_pro = c.Int(nullable: false),
                        amount_order_dt = c.Int(),
                        order_details_total = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.id_order_dt, t.id_pro })
                .ForeignKey("dbo.product", t => t.id_pro)
                .ForeignKey("dbo.user_order", t => t.id_order_dt)
                .Index(t => t.id_order_dt)
                .Index(t => t.id_pro);
            
            CreateTable(
                "dbo.product",
                c => new
                    {
                        id_pr = c.Int(nullable: false, identity: true),
                        name_pr = c.String(nullable: false, maxLength: 255, unicode: false),
                        image_pr = c.String(nullable: false, maxLength: 500, unicode: false),
                        volume_pr = c.Int(nullable: false),
                        price_pr = c.Decimal(nullable: false, precision: 10, scale: 4),
                        amount_pr = c.Int(nullable: false),
                        desc_pr = c.String(storeType: "ntext"),
                        notes_pr = c.String(storeType: "ntext"),
                        tips_pr = c.String(storeType: "ntext"),
                        status_pr = c.Int(nullable: false),
                        id_pro_typeof = c.Int(),
                        id_pro_coll = c.Int(),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_pr)
                .ForeignKey("dbo.collection_child", t => t.id_pro_coll)
                .ForeignKey("dbo.product_type", t => t.id_pro_typeof)
                .Index(t => t.id_pro_typeof)
                .Index(t => t.id_pro_coll);
            
            CreateTable(
                "dbo.collection_child",
                c => new
                    {
                        id_c_child = c.Int(nullable: false, identity: true),
                        name_c_child = c.String(nullable: false, maxLength: 250, unicode: false),
                        desc_c_child = c.String(storeType: "ntext"),
                        id_c_collect = c.Int(),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_c_child)
                .ForeignKey("dbo.collection", t => t.id_c_collect)
                .Index(t => t.id_c_collect);
            
            CreateTable(
                "dbo.collection",
                c => new
                    {
                        id_collect = c.Int(nullable: false, identity: true),
                        name_collect = c.String(nullable: false, maxLength: 250, unicode: false),
                        desc_collect = c.String(storeType: "ntext"),
                        image_collect = c.String(maxLength: 500, unicode: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_collect);
            
            CreateTable(
                "dbo.product_type",
                c => new
                    {
                        id_prt = c.Int(nullable: false, identity: true),
                        name_prt = c.String(nullable: false, maxLength: 100, unicode: false),
                        image_prt = c.String(maxLength: 500, unicode: false),
                        desc_prt = c.String(storeType: "ntext"),
                        forgender_prt = c.String(maxLength: 100, unicode: false),
                        created_at = c.DateTime(),
                        id_menu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_prt)
                .ForeignKey("dbo.menus", t => t.id_menu, cascadeDelete: true)
                .Index(t => t.id_menu);
            
            CreateTable(
                "dbo.menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.admin",
                c => new
                    {
                        id_ad = c.Int(nullable: false, identity: true),
                        name_ad = c.String(nullable: false, maxLength: 255, unicode: false),
                        email_ad = c.String(nullable: false, maxLength: 255, unicode: false),
                        password_ad = c.String(nullable: false, maxLength: 100, unicode: false),
                        created_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_ad)
                .Index(t => t.name_ad, unique: true)
                .Index(t => t.email_ad, unique: true);
            
            CreateTable(
                "dbo.contact",
                c => new
                    {
                        id_contact = c.Int(nullable: false, identity: true),
                        info_contact = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.id_contact);
            
            CreateTable(
                "dbo.message",
                c => new
                    {
                        email_mes = c.String(maxLength: 200, unicode: false),
                        content_mes = c.String(storeType: "ntext"),
                        id_mes = c.Int(nullable: false, identity: true),
                        fullname_mes = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id_mes);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.user_order", "id_order_user", "dbo.acc_user");
            DropForeignKey("dbo.order_details", "id_order_dt", "dbo.user_order");
            DropForeignKey("dbo.product", "id_pro_typeof", "dbo.product_type");
            DropForeignKey("dbo.product_type", "id_menu", "dbo.menus");
            DropForeignKey("dbo.order_details", "id_pro", "dbo.product");
            DropForeignKey("dbo.product", "id_pro_coll", "dbo.collection_child");
            DropForeignKey("dbo.collection_child", "id_c_collect", "dbo.collection");
            DropIndex("dbo.admin", new[] { "email_ad" });
            DropIndex("dbo.admin", new[] { "name_ad" });
            DropIndex("dbo.product_type", new[] { "id_menu" });
            DropIndex("dbo.collection_child", new[] { "id_c_collect" });
            DropIndex("dbo.product", new[] { "id_pro_coll" });
            DropIndex("dbo.product", new[] { "id_pro_typeof" });
            DropIndex("dbo.order_details", new[] { "id_pro" });
            DropIndex("dbo.order_details", new[] { "id_order_dt" });
            DropIndex("dbo.user_order", new[] { "id_order_user" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.message");
            DropTable("dbo.contact");
            DropTable("dbo.admin");
            DropTable("dbo.menus");
            DropTable("dbo.product_type");
            DropTable("dbo.collection");
            DropTable("dbo.collection_child");
            DropTable("dbo.product");
            DropTable("dbo.order_details");
            DropTable("dbo.user_order");
            DropTable("dbo.acc_user");
        }
    }
}
