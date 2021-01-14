using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServiceAuto.Models
{
    public class Min18YearsValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (RegisterViewModel)validationContext.ObjectInstance;

            if (user.BirthDay == null)
                return new ValidationResult("Birthdate is required.");

            var age = DateTime.Today.Year - user.BirthDay.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("User should be at least 18 years to create an account");
        }
        
    }
}