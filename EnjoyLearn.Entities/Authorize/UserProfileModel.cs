using EnjoyLearn.Models.Rating;
using EnjoyLearn.Models.Tranings;

namespace EnjoyLearn.Models.Authorize
{
  using EnjoyLearn.Models.Interfaces;
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class UserProfileModel : IEntity<Guid>
  {
    [Key]
    public Guid Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserModel User { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    //[DataType(DataType.EmailAddress)]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})$")]
    public string Email { get; set; }

    public string Gender { get; set; }

    //[DataType(DataType.PhoneNumber)]
    public string Mobile { get; set; }
   
    public int Age { get; set; }
     
    public string Country { get; set; }

    public string City { get; set; }

    [MaxLength]
    //[DataType(DataType.ImageUrl)]
    public byte[] Avatar { get; set; }

    //[DataType(DataType.Currency)]
    public string AmountMoney { get; set; }

    //[DataType(DataType.DateTime)]
    //public DateTime RegisterDate { get; set; }

    ////[DataType(DataType.DateTime)]
    //public DateTime LastOnlineDate { get; set; }

    public Guid PointsId { get; set; }
    [ForeignKey("PointsId")]
    public virtual PointsModel Points { get; set; }
    
    public Guid RanksNobilityId { get; set; }
    [ForeignKey("RanksNobilityId")]
    public virtual RanksNobilityModel RanksNobility { get; set; }
    /*
    public Guid TraningResultsId { get; set; }
    [ForeignKey("TraningResultsId")]
    public virtual TraningResultsModel TraningResults { get; set; }*/
  }
}
