public class Loan
{
    public int LoanId { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
    public int BookId { get; set; }
    public int BorrowerId { get; set; }
}
