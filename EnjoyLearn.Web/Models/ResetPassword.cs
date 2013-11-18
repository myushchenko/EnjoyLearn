﻿using System.ComponentModel.DataAnnotations;

namespace EnjoyLearn.Web.Models
{
  public class ResetPassword
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(64, MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Required, Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}