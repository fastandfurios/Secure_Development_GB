using AutoMapper;
using Debit_Cards_Project.DAL.Models.CashBack;
using Debit_Cards_Project.DAL.Models.DebitCard;
using Debit_Cards_Project.Domain.Products;
using Debit_Cards_Project.Domain.Services;
using Debit_Cards_Project.DTO;

namespace Debit_Cards_Project.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DebitCard, DebitCardDto>();
            CreateMap<Holder, HolderDto>();
            CreateMap<CashBack, CashBackDto>()
                .ForMember(dest => dest.Percent, oct => oct.MapFrom(src => $"{src.Percent:F1} %"));
        }
    }
}
