using System.ComponentModel.DataAnnotations;

public class CreateLoanDTO
{
    [Required(ErrorMessage = "A loan date is required.")]
    public required DateOnly LoanDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    [Required(ErrorMessage = "A book ID is required.")]
    public required int BookId { get; set; }

    [Required(ErrorMessage = "A borrower ID is required.")]
    public required int BorrowerId { get; set; }
}
