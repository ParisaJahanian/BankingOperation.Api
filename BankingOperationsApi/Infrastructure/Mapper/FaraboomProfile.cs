using AutoMapper;
using BankingOperationsApi.Models;

namespace BankingOperationsApi.Infrastructure.Mapper
{
    public class FaraboomProfile : Profile
    {
        public FaraboomProfile()
        {
            CreateMap<SatnaTransferReqDTO, SatnaTransferReq>().ReverseMap();
            CreateMap<PayaTransferReq, PayaTransferReqDTO>().ReverseMap();
            CreateMap<PayaBatchTransferReq, PayaBatchTransferReqDTO>().ReverseMap();
            CreateMap<TokenRes,TokenOutput>().ReverseMap();
       
        }
    }
}
