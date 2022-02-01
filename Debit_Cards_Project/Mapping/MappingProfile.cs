using AutoMapper;
using Debit_Cards_Project.DAL.Models.DebitCard;
using Debit_Cards_Project.DTO;

namespace Debit_Cards_Project.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DebitCard, DebitCardDto>();
            CreateMap<Holder, HolderDto>();
        }
    }
}
