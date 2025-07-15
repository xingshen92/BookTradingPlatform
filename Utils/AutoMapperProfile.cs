using AutoMapper;
using BookTradingPlatform.Controllers.Models;
using BookTradingPlatform.Dtos;
using BookTradingPlatform.Models;
using BookTradingPlatform.Vos;

namespace BookTradingPlatform.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User → 註冊回應 DTO
            CreateMap<User, RegisterResponseDto>();
            // Dto → Entity
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.User, opt => opt.Ignore()); // 忽略 User 屬性，因為在服務層會設定
            // Entity → VO (回傳前端)
            CreateMap<Product, ProductVO>();
            // Entity ↔ BO (商業邏輯層)
            CreateMap<Product, ProductBO>().ReverseMap();
            // User → UserDataVO
            CreateMap<User, UserDataVO>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.TelePhone));
		}
	}
}
