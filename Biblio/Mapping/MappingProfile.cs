using AutoMapper;
using Biblio.DAL.Models.Book;
using Biblio.DTOs;

namespace Biblio.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
