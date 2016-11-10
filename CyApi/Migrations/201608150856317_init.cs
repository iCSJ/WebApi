namespace CyApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillNo = c.String(unicode: false),
                        OrderGroupNo = c.String(unicode: false),
                        PayTypeId = c.Int(nullable: false),
                        PayType = c.String(unicode: false),
                        PayDate = c.DateTime(nullable: false, precision: 0),
                        RealPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TypeOfPay = c.Int(nullable: false),
                        OperatorName = c.String(unicode: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                        OrderGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderGroup", t => t.OrderGroup_Id)
                .Index(t => t.OrderGroup_Id);
            
            CreateTable(
                "dbo.OrderGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupNo = c.String(maxLength: 255, storeType: "nvarchar"),
                        OrderCount = c.Int(),
                        OperatorName = c.String(maxLength: 255, storeType: "nvarchar"),
                        CheckDate = c.DateTime(nullable: false, precision: 0),
                        PersonNumber = c.Int(nullable: false),
                        ProductAmount = c.Double(nullable: false),
                        RoomFee = c.Double(nullable: false),
                        SrvFee = c.Double(nullable: false),
                        Amount = c.Double(nullable: false),
                        DiscountRate = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        FreeCharge = c.Double(nullable: false),
                        ShouldPay = c.Double(nullable: false),
                        YetPay = c.Double(nullable: false),
                        ChargeAmount = c.Double(nullable: false),
                        AllDiscount = c.Boolean(nullable: false),
                        DiscountType = c.Int(nullable: false),
                        InvoiceNo = c.String(unicode: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChargeAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderGroupNo = c.String(unicode: false),
                        ChargeDate = c.DateTime(nullable: false, precision: 0),
                        CustomerId = c.Int(nullable: false),
                        CustomerName = c.String(unicode: false),
                        ChargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Repayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                        OrderGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderGroup", t => t.OrderGroup_Id)
                .Index(t => t.OrderGroup_Id);
            
            CreateTable(
                "dbo.HisOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(maxLength: 255, storeType: "nvarchar"),
                        TableId = c.Int(nullable: false),
                        TableName = c.String(maxLength: 255, storeType: "nvarchar"),
                        CustomerId = c.Int(nullable: false),
                        CustomerName = c.String(unicode: false),
                        OperatorName = c.String(maxLength: 255, storeType: "nvarchar"),
                        CheckDate = c.DateTime(nullable: false, precision: 0),
                        PersonNumber = c.Int(nullable: false),
                        DishAmount = c.Double(nullable: false),
                        RoomFee = c.Double(nullable: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                        OrderGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderGroup", t => t.OrderGroup_Id)
                .Index(t => t.OrderGroup_Id);
            
            CreateTable(
                "dbo.HisOrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductTypeId = c.String(maxLength: 255, storeType: "nvarchar"),
                        ProductType = c.String(unicode: false),
                        ProductId = c.String(unicode: false),
                        ProductName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Unit = c.String(maxLength: 255, storeType: "nvarchar"),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Waiter = c.String(unicode: false),
                        OperatorName = c.String(unicode: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Memo = c.String(unicode: false),
                        Status = c.Int(nullable: false),
                        Batch = c.Int(nullable: false),
                        OrderType = c.Int(nullable: false),
                        MainProduct = c.Int(),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HisOrder", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.CurOrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductTypeId = c.String(maxLength: 255, storeType: "nvarchar"),
                        ProductType = c.String(unicode: false),
                        ProductId = c.String(unicode: false),
                        ProductName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Unit = c.String(maxLength: 255, storeType: "nvarchar"),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Waiter = c.String(unicode: false),
                        OperatorName = c.String(unicode: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Memo = c.String(unicode: false),
                        Status = c.Int(nullable: false),
                        Batch = c.Int(nullable: false),
                        OrderType = c.Int(nullable: false),
                        MainProduct = c.Int(),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CurOrder", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.CurOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(maxLength: 255, storeType: "nvarchar"),
                        TableId = c.Int(nullable: false),
                        TableName = c.String(maxLength: 255, storeType: "nvarchar"),
                        CustomerId = c.Int(nullable: false),
                        CustomerName = c.String(unicode: false),
                        OperatorName = c.String(maxLength: 255, storeType: "nvarchar"),
                        CheckDate = c.DateTime(nullable: false, precision: 0),
                        PersonNumber = c.Int(nullable: false),
                        DishAmount = c.Double(nullable: false),
                        RoomFee = c.Double(nullable: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        NickName = c.String(unicode: false),
                        Tel = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        QQ = c.String(unicode: false),
                        BirthDay = c.DateTime(nullable: false, precision: 0),
                        Sex = c.String(unicode: false),
                        IdNumber = c.String(unicode: false),
                        Password = c.String(unicode: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VipCard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNo = c.String(unicode: false),
                        CustomerId = c.Int(nullable: false),
                        Password = c.String(unicode: false),
                        IssueDate = c.DateTime(nullable: false, precision: 0),
                        ExpiredDate = c.DateTime(nullable: false, precision: 0),
                        NotValid = c.Boolean(),
                        FillAmount = c.Decimal(precision: 18, scale: 2),
                        PresentAmount = c.Decimal(precision: 18, scale: 2),
                        ExpendAmount = c.Decimal(precision: 18, scale: 2),
                        RemainAmount = c.Decimal(precision: 18, scale: 2),
                        FillTimes = c.Int(),
                        PresentTimes = c.Int(),
                        ExpendTimes = c.Int(),
                        RemainTimes = c.Int(),
                        IntegralAmount = c.Decimal(precision: 18, scale: 2),
                        integralRemain = c.Decimal(precision: 18, scale: 2),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.VipCardFillFlow",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VipCardNo = c.String(unicode: false),
                        FillNo = c.String(unicode: false),
                        Fill = c.Double(nullable: false),
                        Remain = c.Double(nullable: false),
                        TypeOfFill = c.Int(nullable: false),
                        FillDate = c.DateTime(nullable: false, precision: 0),
                        FillStatus = c.Int(nullable: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                        VipCard_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VipCard", t => t.VipCard_Id)
                .Index(t => t.VipCard_Id);
            
            CreateTable(
                "dbo.VipCardPayFlow",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VipCardNo = c.String(unicode: false),
                        OrderGroupNo = c.String(unicode: false),
                        BillNo = c.String(unicode: false),
                        LastRemain = c.Double(nullable: false),
                        Expended = c.Double(nullable: false),
                        TypeOfPay = c.Int(nullable: false),
                        PayDate = c.DateTime(nullable: false, precision: 0),
                        PayStatus = c.Int(nullable: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                        VipCard_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VipCard", t => t.VipCard_Id)
                .Index(t => t.VipCard_Id);
            
            CreateTable(
                "dbo.Dict",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Categroy = c.String(unicode: false),
                        Key = c.String(unicode: false),
                        Key1 = c.String(unicode: false),
                        Key2 = c.String(unicode: false),
                        Value = c.String(unicode: false),
                        Value1 = c.String(unicode: false),
                        Value2 = c.String(unicode: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Option",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 255, storeType: "nvarchar"),
                        Value = c.String(maxLength: 255, storeType: "nvarchar"),
                        Value1 = c.String(maxLength: 255, storeType: "nvarchar"),
                        Value2 = c.String(maxLength: 255, storeType: "nvarchar"),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PayType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        IsVip = c.Boolean(),
                        IsTimesVip = c.Boolean(),
                        IsIngegralVip = c.Boolean(),
                        CanCharge = c.Boolean(),
                        CanMerge = c.Boolean(),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        PosId = c.Int(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        RoomFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        PersonConsume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LowestConsume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pos", t => t.PosId, cascadeDelete: true)
                .Index(t => t.PosId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductNo = c.String(unicode: false),
                        ProductTypeId = c.String(unicode: false),
                        ProductTypeCode = c.String(unicode: false),
                        ProductTypeName = c.String(unicode: false),
                        ProductName = c.String(unicode: false),
                        Spec = c.String(unicode: false),
                        PieceUnit = c.String(unicode: false),
                        Unit = c.String(unicode: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discountable = c.Boolean(),
                        CanIntegral = c.Boolean(),
                        Py = c.String(unicode: false),
                        InputCode = c.String(unicode: false),
                        CanModify = c.Boolean(),
                        CanMatch = c.Boolean(),
                        MaxSelected = c.Int(),
                        Picture = c.String(unicode: false),
                        TypeOfProduct = c.Int(nullable: false),
                        Memo = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                        ProductType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductType", t => t.ProductType_Id)
                .Index(t => t.ProductType_Id);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Code = c.String(unicode: false),
                        ParentId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsSelected = c.Boolean(),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                        MainProduct_Id = c.Int(),
                        Product_Id = c.Int(),
                        Product_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.MainProduct_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id1)
                .Index(t => t.MainProduct_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Product_Id1);
            
            CreateTable(
                "dbo.ShopInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Tel = c.String(unicode: false),
                        Contact = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        QQ = c.String(unicode: false),
                        MacAddress = c.String(unicode: false),
                        Picture = c.String(unicode: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserShop",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ShopId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ShopId });
            
            CreateTable(
                "dbo.VipCardRule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfTime = c.Int(),
                        DiscountRate = c.Double(),
                        TypeOfIntegral = c.Int(),
                        IntegralBaseNum = c.Double(),
                        IntegralPoint = c.Double(),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                        Pos_Id = c.Int(),
                        VipCardType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pos", t => t.Pos_Id)
                .ForeignKey("dbo.VipCardType", t => t.VipCardType_Id)
                .Index(t => t.Pos_Id)
                .Index(t => t.VipCardType_Id);
            
            CreateTable(
                "dbo.VipCardType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Level = c.Int(nullable: false),
                        GrowValue = c.Int(nullable: false),
                        AutoGrowType = c.Int(nullable: false),
                        ShopId = c.Int(),
                        ShopName = c.String(maxLength: 255, storeType: "nvarchar"),
                        Modifier = c.String(maxLength: 60, storeType: "nvarchar"),
                        ModifyTime = c.DateTime(nullable: false, precision: 0, storeType: "timestamp"),
                        Creator = c.String(maxLength: 60, storeType: "nvarchar"),
                        CreateTime = c.DateTime(precision: 0, storeType: "timestamp"),
                        IsDeleted = c.Boolean(),
                        SortId = c.Int(),
                        IsSync = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VipCardRule", "VipCardType_Id", "dbo.VipCardType");
            DropForeignKey("dbo.VipCardRule", "Pos_Id", "dbo.Pos");
            DropForeignKey("dbo.SubProducts", "Product_Id1", "dbo.Product");
            DropForeignKey("dbo.SubProducts", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.SubProducts", "MainProduct_Id", "dbo.Product");
            DropForeignKey("dbo.Product", "ProductType_Id", "dbo.ProductType");
            DropForeignKey("dbo.Tables", "PosId", "dbo.Pos");
            DropForeignKey("dbo.VipCardPayFlow", "VipCard_Id", "dbo.VipCard");
            DropForeignKey("dbo.VipCardFillFlow", "VipCard_Id", "dbo.VipCard");
            DropForeignKey("dbo.VipCard", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.CurOrderDetail", "OrderId", "dbo.CurOrder");
            DropForeignKey("dbo.HisOrder", "OrderGroup_Id", "dbo.OrderGroup");
            DropForeignKey("dbo.HisOrderDetail", "OrderId", "dbo.HisOrder");
            DropForeignKey("dbo.ChargeAccounts", "OrderGroup_Id", "dbo.OrderGroup");
            DropForeignKey("dbo.Bill", "OrderGroup_Id", "dbo.OrderGroup");
            DropIndex("dbo.VipCardRule", new[] { "VipCardType_Id" });
            DropIndex("dbo.VipCardRule", new[] { "Pos_Id" });
            DropIndex("dbo.SubProducts", new[] { "Product_Id1" });
            DropIndex("dbo.SubProducts", new[] { "Product_Id" });
            DropIndex("dbo.SubProducts", new[] { "MainProduct_Id" });
            DropIndex("dbo.Product", new[] { "ProductType_Id" });
            DropIndex("dbo.Tables", new[] { "PosId" });
            DropIndex("dbo.VipCardPayFlow", new[] { "VipCard_Id" });
            DropIndex("dbo.VipCardFillFlow", new[] { "VipCard_Id" });
            DropIndex("dbo.VipCard", new[] { "CustomerId" });
            DropIndex("dbo.CurOrderDetail", new[] { "OrderId" });
            DropIndex("dbo.HisOrderDetail", new[] { "OrderId" });
            DropIndex("dbo.HisOrder", new[] { "OrderGroup_Id" });
            DropIndex("dbo.ChargeAccounts", new[] { "OrderGroup_Id" });
            DropIndex("dbo.Bill", new[] { "OrderGroup_Id" });
            DropTable("dbo.VipCardType");
            DropTable("dbo.VipCardRule");
            DropTable("dbo.UserShop");
            DropTable("dbo.ShopInfo");
            DropTable("dbo.SubProducts");
            DropTable("dbo.ProductType");
            DropTable("dbo.Product");
            DropTable("dbo.Tables");
            DropTable("dbo.Pos");
            DropTable("dbo.PayType");
            DropTable("dbo.Option");
            DropTable("dbo.Dict");
            DropTable("dbo.VipCardPayFlow");
            DropTable("dbo.VipCardFillFlow");
            DropTable("dbo.VipCard");
            DropTable("dbo.Customer");
            DropTable("dbo.CurOrder");
            DropTable("dbo.CurOrderDetail");
            DropTable("dbo.HisOrderDetail");
            DropTable("dbo.HisOrder");
            DropTable("dbo.ChargeAccounts");
            DropTable("dbo.OrderGroup");
            DropTable("dbo.Bill");
        }
    }
}
