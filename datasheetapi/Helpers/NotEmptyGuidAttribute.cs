using System.ComponentModel.DataAnnotations;

namespace datasheetapi.Helpers;

public class NotEmptyGuidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is Guid guidValue)
        {
            return guidValue != Guid.Empty;
        }
        return false; // If the value is not a Guid, it's considered invalid.
    }
}