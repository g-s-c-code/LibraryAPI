using System.ComponentModel.DataAnnotations;

public class CreateBookDTO
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(100, ErrorMessage = "Book title must be less than 100 characters.")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "ISBN is required.")]
    [RegularExpression(@"^[0-9]{10,13}$", ErrorMessage = "ISBN must be between 10-13 digits.")]
    public required string ISBN { get; set; }

    [Range(1450, 2030, ErrorMessage = "Release year must be between 1450 and 2030.")]
    public int ReleaseYear { get; set; }
}
