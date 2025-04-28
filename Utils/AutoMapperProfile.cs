using AutoMapper;
using BookTradingPlatform.Dtos;
using BookTradingPlatform.Models;

namespace BookTradingPlatform.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterResponseDto>();
        }
    }
}
