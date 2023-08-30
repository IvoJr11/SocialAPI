using SocialApi_NET7.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialApi_NET7.Utils.Validations
{
    public class LikeValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var like = (Like)validationContext.ObjectInstance;
            if (!like.IsValid())
            {
                return new ValidationResult("PostID or CommentID must be set");
            }
            return ValidationResult.Success;
        }
    }
}
