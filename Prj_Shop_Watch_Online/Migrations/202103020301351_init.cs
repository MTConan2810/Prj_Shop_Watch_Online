namespace Prj_Shop_Watch_Online.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppFunction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppCode = c.String(),
                        FunctionCode = c.String(),
                        FunctionName = c.String(),
                        IsDirect = c.Boolean(nullable: false),
                        Url = c.String(),
                        HasUpdateRole = c.Boolean(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppFunctionGroupRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppCode = c.String(),
                        FunctionCode = c.String(),
                        GroupCode = c.String(),
                        ViewRole = c.Boolean(nullable: false),
                        UpdateRole = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppGroupRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppCode = c.String(),
                        GroupCode = c.String(),
                        GroupName = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppCode = c.String(),
                        AppName = c.String(),
                        ApiDomain = c.String(),
                        WebDomain = c.String(),
                        AutoLoginUrl = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenTH = c.String(),
                        AnhTH = c.String(),
                        Active = c.Boolean(nullable: false),
                        MoTa = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaSp = c.String(),
                        TenSp = c.String(),
                        Gia = c.Decimal(precision: 18, scale: 2),
                        GioiTinh = c.String(),
                        LoaiDH = c.String(),
                        KieuDH = c.String(),
                        DoChiuNuoc = c.Double(),
                        ChucNang = c.String(),
                        Vo = c.String(),
                        LoaiDay = c.String(),
                        DuongKinh = c.Int(),
                        LoaiMay = c.String(),
                        MauMat = c.String(),
                        MatKinh = c.String(),
                        MoTa = c.String(),
                        BrandId = c.Int(nullable: false),
                        SoLuong = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tieude = c.String(),
                        NoiDung = c.String(),
                        ThoiGian = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        Note = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(),
                        UserId = c.Int(),
                        ShipName = c.String(),
                        ShipAddress = c.String(),
                        ShipEmail = c.String(),
                        ShipPhoneNumber = c.String(),
                        Status = c.Boolean(nullable: false),
                        PaymentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payment", t => t.PaymentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PaymentId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayName = c.String(),
                        MoTa = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImagePath = c.String(),
                        Caption = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(),
                        SortOrder = c.Int(),
                        FileSize = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        ApplyForAll = c.Boolean(nullable: false),
                        DiscountPercent = c.Int(),
                        DiscountAmount = c.Decimal(precision: 18, scale: 2),
                        ProductId = c.Int(),
                        BrandId = c.Int(),
                        Status = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        ChiNhanh = c.String(),
                        DienThoai = c.String(),
                        Mail = c.String(),
                        ImageShop = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroupRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        AppCode = c.String(),
                        GroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.ProductImage", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetail", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropIndex("dbo.ProductImage", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "PaymentId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ProductId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropTable("dbo.UserGroupRole");
            DropTable("dbo.Shops");
            DropTable("dbo.Promotions");
            DropTable("dbo.ProductImage");
            DropTable("dbo.Payment");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Orders");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
            DropTable("dbo.AppList");
            DropTable("dbo.AppGroupRole");
            DropTable("dbo.AppFunctionGroupRole");
            DropTable("dbo.AppFunction");
        }
    }
}
