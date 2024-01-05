using System.ComponentModel.DataAnnotations;

public class CreateBorrowerDTO
{
    [Required(ErrorMessage = "A first name is required.")]
    [MaxLength(50, ErrorMessage = "First name cannot be more than 50 characters.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "A last name is required.")]
    [MaxLength(50, ErrorMessage = "Last name cannot be more than 50 characters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "A Social Security Number (SSN) is required.")]
    [RegularExpression(@"^\d{12}$", ErrorMessage = "SSN must be exactly 12 digits.")]
    public required string SSN { get; set; }
}
