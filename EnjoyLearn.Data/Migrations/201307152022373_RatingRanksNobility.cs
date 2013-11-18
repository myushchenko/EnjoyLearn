namespace EnjoyLearn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingRanksNobility : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rating_RanksNobility",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MaleName = c.String(),
                        FemaleName = c.String(),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User_Profiles", "RanksNobilityId", c => c.Guid(nullable: false));
            CreateIndex("dbo.User_Profiles", "RanksNobilityId");
            AddForeignKey("dbo.User_Profiles", "RanksNobilityId", "dbo.Rating_RanksNobility", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Profiles", "RanksNobilityId", "dbo.Rating_RanksNobility");
            DropIndex("dbo.User_Profiles", new[] { "RanksNobilityId" });
            DropColumn("dbo.User_Profiles", "RanksNobilityId");
            DropTable("dbo.Rating_RanksNobility");
        }
    }
}
