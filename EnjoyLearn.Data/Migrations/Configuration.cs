
using EnjoyLearn.Models.Rating;

namespace EnjoyLearn.Data.Migrations
{
  using EnjoyLearn.Models;
  using EnjoyLearn.Models.Authorize;
  using System;
  using System.Data.Entity.Migrations;
  using System.Linq;
  using System.Web.Security;
  using WebMatrix.WebData;
  using System.Collections.Generic;

  internal sealed class Configuration : DbMigrationsConfiguration<EnjoyLearnContext>
  {
    private const string UserName = "Michael";
    private const string UserPassword = "q1!";

    public Configuration()
    {
      AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(EnjoyLearnContext context)
    {
      WebSecurity.InitializeDatabaseConnection(
          "EnjoyLearnDB",
          "Users",
          "UserId",
          "UserName", autoCreateTables: true
         );

      if (!Roles.RoleExists("Administrator"))
        Roles.CreateRole("Administrator");

      if (!WebSecurity.UserExists("Michael"))
      {
        WebSecurity.CreateUserAndAccount(
         UserName,
         UserPassword);
      }

      if (!Roles.GetRolesForUser(UserName).Contains("Administrator"))
        Roles.AddUsersToRoles(new[] { UserName }, new[] { "Administrator" });


      SeedUserProfile(context);
      SeedDictionary(context);
      SeedRatingLevels(context);
      SeedRatingRanksNobility(context);
    }

    private void SeedUserProfile(EnjoyLearnContext context)
    {
      var level = new LevelModel
       {
         Id = Guid.NewGuid(),
         Type = 0,
         Name = "Level 0",
         Points = 0
       };

      var nextLevel = new LevelModel
        {
          Id = Guid.NewGuid(),
          Type = 1,
          Name = "Level 1",
          Points = 10
        };

      context.Levels.AddOrUpdate(l => l.Type, nextLevel);

      var point = new PointsModel
        {
          Id = Guid.NewGuid(),
          Points = 0,
          Level = level,
          NextLevelId = nextLevel.Id
        };

      var ranksNobility = new RanksNobilityModel
      {
        Id = Guid.NewGuid(),
        MaleName = "Commoner",
        FemaleName = "Commoner",
        Points = 0,

      };
      context.UserProfiles.AddOrUpdate(l => l.Email,
        new UserProfileModel
        {
          Id = Guid.NewGuid(),
          FirstName = "Michael",
          LastName = "Yushchenko",
          Email = "yushchenko.michael@gmail.com",
          UserId = WebSecurity.GetUserId(UserName),
          Age = 24,
          City = "Vinnytsia",
          Country = "Ukraine",
          Gender = "Male",
          Mobile = "+380973202720",
          Points = point,
          RanksNobility = ranksNobility
        });
    }



    private void SeedRatingLevels(EnjoyLearnContext context)
    {
      for (var i = 2; i < 30; i++)
      {
        context.Levels.AddOrUpdate(l => l.Type, new LevelModel
        {
          Id = Guid.NewGuid(),
          Type = i,
          Name = "Level " + i,
          Points = 10 + 10 * i
        });
      }
    }

    private void SeedRatingRanksNobility(EnjoyLearnContext context)
    {
      foreach (var ranksNobility in _lstRanksNobility)
      {
        context.RanksNobility.AddOrUpdate(l => l.MaleName, ranksNobility);
      }
    }

    private readonly List<RanksNobilityModel> _lstRanksNobility = new List<RanksNobilityModel>
     {
             new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "Lord",
              FemaleName = "Lady",
               Points = 10,
             },
            new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "Baron",
               FemaleName = "Baroness",
               Points = 20,
             },
              new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "Viscount",
              FemaleName = "Viscountess",
               Points = 30,
             },
             new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "Earl",
               FemaleName = "Countess",
               Points = 40,
             },
              new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "Marquess",
              FemaleName = "Marchioness",
               Points = 60,
             },
             new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "Duke",
               FemaleName = "Duchess",
               Points = 200,
             },
              new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "Prince",
               FemaleName = "Princess",
               Points = 300,
             },
             new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "King",
               FemaleName = "Queen",
               Points = 400,
             },
             new RanksNobilityModel
             {
               Id = Guid.NewGuid(),
               MaleName = "Emperor",
               FemaleName = "Empress",
               Points = 500,
             }
         };


    private void SeedDictionary(EnjoyLearnContext context)
    {
      foreach (var dictionary in _lstDictionary.OrderBy(t => t.EnglishWord))
      {
        context.Dictionary.AddOrUpdate(l => l.EnglishWord, dictionary);
        context.UserDictionary.AddOrUpdate(l => l.DictionaryId, new UserDictionaryModel
                                             {
                                               Id = Guid.NewGuid(),
                                               DictionaryId = dictionary.Id,
                                               UserId = context.Users.FirstOrDefault().UserId
                                             });
      }
    }

    private readonly List<DictionaryModel> _lstDictionary = new List<DictionaryModel>
         {
           new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "anniversary",
               TranscriptionWord = "[ænɪvˈɜːsəri]",
               RussianWord = "годовщина"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "clue",
               TranscriptionWord = "[klˈuː]",
               RussianWord = "ключ к разгадке"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "sigh",
               TranscriptionWord = "[sˈaɪ]",
               RussianWord = "вздох"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Touchy-feely",
               TranscriptionWord = "[tˈʌtʃi fˈiːli]",
               RussianWord = "черезчур эмоциональный"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "in a jiffy",
               TranscriptionWord = "",
               RussianWord = "мигом"
             }
             ,
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Two shakes",
               TranscriptionWord = "[tˈuː ʃˈeɪːks]",
               RussianWord = "в два щета"
             }
             ,
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Two Ticks",
               TranscriptionWord = "[tˈuː tˈɪks]",
               RussianWord = "в два счета; пару секунд"
             }
             ,
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "That's a bit of a swizz",
               TranscriptionWord = "",
               RussianWord = "это чистое надувательство"
             }
             ,
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Swizz",
               TranscriptionWord = "[swˈɪz]",
               RussianWord = "надувательство"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Savvy",
               TranscriptionWord = "[sˈævi]",
               RussianWord = "находчивость"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Anorak",
               TranscriptionWord = "[ˈænəræk]",
               RussianWord = "'помешанный' на чем-то"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "dram",
               TranscriptionWord = "[drˈæm]",
               RussianWord = "глоток"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "wee bit",
               TranscriptionWord = "[wˈiː bˈɪt]",
               RussianWord = "чуть-чуть"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Wee",
               TranscriptionWord = "[wˈiː]",
               RussianWord = "крошечный"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Loved Up",
               TranscriptionWord = "[lˈʌvd ʌp]",
               RussianWord = "влюблен, окрылен любовью"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "What's wrong",
               TranscriptionWord = "[wˈets rˈɒŋ]",
               RussianWord = "В чем дело"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "Chillax",
               TranscriptionWord = "[tʃˈɪləks]",
               RussianWord = "не волноваться"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "rabbit in the headlight",
               TranscriptionWord = "",
               RussianWord = "кролик в свете фар"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "whispering",
               TranscriptionWord = "[wˈɪspərɪŋ]",
               RussianWord = "шёпот"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "insurance",
               TranscriptionWord = "[ɪnʃˈʊəːrəns]",
               RussianWord = "страховка"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "menacingly",
               TranscriptionWord = "[mˈenəsɪŋli]",
               RussianWord = "угрожающе"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "swig",
               TranscriptionWord = "[swˈɪg]",
               RussianWord = "большой глоток"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "disturb",
               TranscriptionWord = "[dɪstˈɜːb]",
               RussianWord = "беспокоить"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "To freak someone out",
               TranscriptionWord = "",
               RussianWord = "волновать кого-то"
             },
             new DictionaryModel
             {
               Id = Guid.NewGuid(),
               EnglishWord = "intention",
               TranscriptionWord = "[ɪntˈenʃən]",
               RussianWord = "намерение"
             }
         };

    /*private IEnumerable<DictionaryModel> GetDictionaryData()
    {
      var path = AppDomain.CurrentDomain.BaseDirectory + @"..\\words.xlsx";
      var strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + @";Extended Properties=""Excel 12.0 Xml;HDR=YES""";
      var lstDictionary = new List<DictionaryModel>();
      using (var oleDbConnection = new OleDbConnection(strConn))
      {
        oleDbConnection.Open();
        foreach (DataRow row in oleDbConnection.GetSchema("tables").Rows)
        {
          try
          {
            var name = row[2].ToString().Replace("''", "'").TrimEnd('_');
            var ds = new DataSet();
            new OleDbDataAdapter("SELECT * from [" + name + "]", strConn).Fill(ds);
            foreach (DataTable table in ds.Tables)
            {
              foreach (DataRow row2 in table.Rows)
              {
                lstDictionary.Add(new DictionaryModel
                {
                  Id = Guid.NewGuid(),
                  UserId = WebSecurity.GetUserId(UserName),
                  EnglishWord = row2.ItemArray[1].ToString().Trim(),
                  TranscriptionWord = row2.ItemArray[2].ToString().Trim(),
                  RussianWord = row2.ItemArray[3].ToString().Trim()
                });
              }
            }
          }
          catch (Exception e){}
        }
      }
      return lstDictionary;
    }*/

  }
}
