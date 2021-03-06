﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace WebApplication.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
    //Parsa
    public class ManageUserViewModel
    {
        [Required(ErrorMessage = "The field First name must be filled in.")]
        [RegularExpression("^[A-ZÆØÅ]+([a-zæøå]|([-][A-ZÆØÅ]))+$", ErrorMessage = "First name must only contain letters and dash(-).")]

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field Last name must be filled in.")]
        [RegularExpression("^[A-ZÆØÅ]+([a-zæøå]|([-][A-ZÆØÅ]))+$", ErrorMessage = "Last name must only contain letters and dash(-).")]

        public string SurName { get; set; }

        [Required(ErrorMessage = "The field Phone number must be filled in.")]
        [RegularExpression("\\d{8}", ErrorMessage = "Phone number must be digits only and must be 8 digits long.")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        // [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The field Email must be filled in.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(\w+@iha.dk)|(\w+@post.au.dk)",
            ErrorMessage = "Email must be with domain @iha.dk or @post.au.dk")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field Password must be filled in.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The field First name must be filled in.")]
        [RegularExpression("^[A-ZÆØÅ]+([a-zæøå]|([-][A-ZÆØÅ]))+$", ErrorMessage = "First name must only contain letters and dash(-).")]

        [Display(Name = "First name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "The field Last name must be filled in.")]
        [RegularExpression("^[A-ZÆØÅ]+([a-zæøå]|([-][A-ZÆØÅ]))+$$", ErrorMessage = "Last name must only contain letters and dash(-).")]

        [Display(Name = "Last name")]
        public string LName { get; set; }

        [Required(ErrorMessage = "The field Phone number must be filled in.")]
        [RegularExpression("\\d{8}", ErrorMessage = "Phone number must be digits only and must be 8 digits long.")]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }
    }


    public class ActivationViewModel
    {

        [Display(Name = "Activation code")]
        public string ActivationCode { get; set; }

    }

    public class EditUsersViewModel
    {
        [Display(Name = "Role")]
        [Required(ErrorMessage = "User role must be filled!")]
        [RegularExpression("^[A][d][m][i][n]$|^[U][s][e][r]$", ErrorMessage = "You can choose either 'Admin' or 'User'")]
        public string UserRole { get; set; }

        public string UserId { get; set; }
    }

    public class IndexUsersViewModel
    {
        public List<Tuple<ApplicationUser, string>> UserTuples { get; set; }


    }
    public class DetailsUserViewModel
    {
        public string UserRole;
        public string PhoneNumber;
        public string Email;
        public string FName;
        public string LName;
        public string Activated;
        public string Id;
    }
}
