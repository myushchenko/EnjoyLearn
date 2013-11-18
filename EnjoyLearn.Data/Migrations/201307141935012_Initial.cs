namespace EnjoyLearn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Chat_PrivateMessages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Message = c.String(),
                        ReceivedOn = c.DateTimeOffset(nullable: false),
                        UserChatConnectionId = c.Guid(nullable: false),
                        SenderUserId = c.Int(nullable: false),
                        ReceiverUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chat_UserConnectionss", t => t.UserChatConnectionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SenderUserId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ReceiverUserId, cascadeDelete: false)
                .Index(t => t.UserChatConnectionId)
                .Index(t => t.SenderUserId)
                .Index(t => t.ReceiverUserId);
            
            CreateTable(
                "dbo.Chat_UserConnectionss",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Chat_Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Message = c.String(),
                        ReceivedOn = c.DateTimeOffset(nullable: false),
                        UserChatConnectionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chat_UserConnectionss", t => t.UserChatConnectionId, cascadeDelete: true)
                .Index(t => t.UserChatConnectionId);
            
            CreateTable(
                "dbo.Dictionary",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EnglishWord = c.String(),
                        RussianWord = c.String(),
                        TranscriptionWord = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_Dictionaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        DictionaryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Dictionary", t => t.DictionaryId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DictionaryId);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        Level = c.String(),
                        Logger = c.String(),
                        Message = c.String(),
                        ExceptionType = c.String(),
                        Operation = c.String(),
                        ExceptionMessage = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Gender = c.String(),
                        Mobile = c.String(),
                        Age = c.Int(nullable: false),
                        Country = c.String(),
                        City = c.String(),
                        Avatar = c.Binary(),
                        AmountMoney = c.String(),
                        PointsId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Rating_Points", t => t.PointsId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PointsId);
            
            CreateTable(
                "dbo.Rating_Points",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Points = c.Int(),
                        LevelId = c.Guid(nullable: false),
                        NextLevelId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rating_Levels", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Rating_Levels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Profiles", "PointsId", "dbo.Rating_Points");
            DropForeignKey("dbo.Rating_Points", "LevelId", "dbo.Rating_Levels");
            DropForeignKey("dbo.User_Profiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.User_Dictionaries", "DictionaryId", "dbo.Dictionary");
            DropForeignKey("dbo.User_Dictionaries", "UserId", "dbo.Users");
            DropForeignKey("dbo.Chat_PrivateMessages", "ReceiverUserId", "dbo.Users");
            DropForeignKey("dbo.Chat_PrivateMessages", "SenderUserId", "dbo.Users");
            DropForeignKey("dbo.Chat_PrivateMessages", "UserChatConnectionId", "dbo.Chat_UserConnectionss");
            DropForeignKey("dbo.Chat_Messages", "UserChatConnectionId", "dbo.Chat_UserConnectionss");
            DropForeignKey("dbo.Chat_UserConnectionss", "UserId", "dbo.Users");
            DropIndex("dbo.User_Profiles", new[] { "PointsId" });
            DropIndex("dbo.Rating_Points", new[] { "LevelId" });
            DropIndex("dbo.User_Profiles", new[] { "UserId" });
            DropIndex("dbo.User_Dictionaries", new[] { "DictionaryId" });
            DropIndex("dbo.User_Dictionaries", new[] { "UserId" });
            DropIndex("dbo.Chat_PrivateMessages", new[] { "ReceiverUserId" });
            DropIndex("dbo.Chat_PrivateMessages", new[] { "SenderUserId" });
            DropIndex("dbo.Chat_PrivateMessages", new[] { "UserChatConnectionId" });
            DropIndex("dbo.Chat_Messages", new[] { "UserChatConnectionId" });
            DropIndex("dbo.Chat_UserConnectionss", new[] { "UserId" });
            DropTable("dbo.Rating_Levels");
            DropTable("dbo.Rating_Points");
            DropTable("dbo.User_Profiles");
            DropTable("dbo.Log");
            DropTable("dbo.User_Dictionaries");
            DropTable("dbo.Dictionary");
            DropTable("dbo.Chat_Messages");
            DropTable("dbo.Chat_UserConnectionss");
            DropTable("dbo.Chat_PrivateMessages");
            DropTable("dbo.Users");
        }
    }
}
