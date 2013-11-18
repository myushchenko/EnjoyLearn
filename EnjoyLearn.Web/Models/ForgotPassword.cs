using System.ComponentModel.DataAnnotations;

namespace EnjoyLearn.Web.Models
{
  public class ForgotPassword
    {
        [Required]
        public string Email { get; set; }
    }
}