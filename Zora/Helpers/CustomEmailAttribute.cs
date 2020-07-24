using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using Zora.Shared.Data;

namespace Zora.Commons.Helpers
{
    //https://docs.microsoft.com/en-us/dotnet/api/system.globalization.idnmapping?view=netcore-3.1
    public class CustomEmailAttribute : ValidationAttribute//I need a custom email attribute, because I need a message in bulgarian
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(ValidationConstants.RequiredFieldEmpty);

            try
            {
                // Normalize the domain
                value = Regex.Replace(value.ToString(), @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return new ValidationResult(ValidationConstants.InvalidEmail);
            }
            catch (ArgumentException)
            {
                return new ValidationResult(ValidationConstants.InvalidEmail);
            }

            try
            {
                if (Regex.IsMatch(value.ToString(),
                     @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                     @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                     RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)) == true)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ValidationConstants.InvalidEmail);
                }


            }
            catch (RegexMatchTimeoutException)
            {
                return new ValidationResult(ValidationConstants.InvalidEmail);
            }
        }
    }
}
