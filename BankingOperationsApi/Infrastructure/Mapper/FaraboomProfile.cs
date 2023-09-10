using AutoMapper;
using BankingOperationsApi.Models;

namespace BankingOperationsApi.Infrastructure.Mapper
{
    public class FaraboomProfile : Profile
    {
        public FaraboomProfile()
        {
            CreateMap<SatnaTransferReqDTO, SatnaTransferReq>().ReverseMap();
            CreateMap<SatnaTransferReq, SatnaTransferReqDTO>().ReverseMap();
            //CreateMap<TokenRes,TokenOutput>().ReverseMap();
            //CreateMap<TokenRes, TokenOutput>()
            //            .ForMember(dest => dest.ExpireTime, opt => opt.MapFrom(src => src.ExpireTime));
            //CreateMap<TokenRes, TokenOutput>()
            //          .ForMember(dest => dest.access_token, opt => opt.MapFrom(src => src.AccessToken));
        }
    }
}
