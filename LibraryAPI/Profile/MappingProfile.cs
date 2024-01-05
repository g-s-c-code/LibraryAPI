using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, ReadBookDTO>();
        CreateMap<CreateBookDTO, Book>();

        CreateMap<Borrower, ReadBorrowerDTO>();
        CreateMap<CreateBorrowerDTO, Borrower>();

        CreateMap<Loan, ReadLoanDTO>();
        CreateMap<CreateLoanDTO, Loan>();
    }
}

