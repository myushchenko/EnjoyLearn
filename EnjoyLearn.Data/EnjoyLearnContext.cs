using EnjoyLearn.Data.Core;
using EnjoyLearn.Models.Rating;

namespace EnjoyLearn.Data
{
  using EnjoyLearn.Models.Chat;
  using Models;
  using Models.Authorize;
  using System.Data.Entity;

  public class EnjoyLearnContext : EntitiesContextBase
  {
    public EnjoyLearnContext() : base("EnjoyLearnDB") { }

    public DbSet<UserModel> Users { get; set; }

    public DbSet<DictionaryModel> Dictionary { get; set; }

    public DbSet<UserDictionaryModel> UserDictionary { get; set; }

    public DbSet<LogModel> LogEntries { get; set; }

    public DbSet<UserProfileModel> UserProfiles { get; set; }

    public DbSet<UserChatConnectionModel> UserChatConnectionss { get; set; }

    public DbSet<ChatMessageModel> ChatMessages { get; set; }

    public DbSet<PrivateChatMessageModel> PrivateChatMessages { get; set; }

    public DbSet<LevelModel> Levels { get; set; }

    public DbSet<PointsModel> Points { get; set; }

    public DbSet<RanksNobilityModel> RanksNobility { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {

      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<LogModel>().ToTable("Log");

      modelBuilder.Entity<UserModel>().ToTable("Users");
      modelBuilder.Entity<UserProfileModel>().ToTable("User_Profiles");

      modelBuilder.Entity<DictionaryModel>().ToTable("Dictionary");
      modelBuilder.Entity<UserDictionaryModel>().ToTable("User_Dictionaries");
      
      modelBuilder.Entity<UserChatConnectionModel>().ToTable("Chat_UserConnectionss");
      modelBuilder.Entity<ChatMessageModel>().ToTable("Chat_Messages");
      modelBuilder.Entity<PrivateChatMessageModel>().ToTable("Chat_PrivateMessages");

      modelBuilder.Entity<LevelModel>().ToTable("Rating_Levels");
      modelBuilder.Entity<PointsModel>().ToTable("Rating_Points");
      modelBuilder.Entity<RanksNobilityModel>().ToTable("Rating_RanksNobility");
    }

    public virtual void Commit()
    {
      base.SaveChanges();
    }
  }
}
