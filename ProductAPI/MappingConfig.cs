using AutoMapper;
using ProductAPI.Models;
using ProductAPI.Models.DTO.CategoryDtos;
using ProductAPI.Models.DTO.ImageDtos;
using ProductAPI.Models.DTO.ProductDtos;
using ProductAPI.Models.DTO.VideoDtos;

namespace ProductAPI
{
    public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<Product, ProductDTO>().ReverseMap();
			CreateMap<Product, ProductCreateDTO>().ReverseMap();
			CreateMap<Product, ProductUpdateDTO>().ReverseMap();

			CreateMap<Category, CategoryDTO>().ReverseMap();
			CreateMap<Category, CategoryCreateDTO>().ReverseMap();
			CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

			CreateMap<Image, ImageDTO>().ReverseMap();
			CreateMap<Image, ImageCreateDTO>().ReverseMap();
			CreateMap<Image, ImageUpdateDTO>().ReverseMap();

			CreateMap<Video, VideoDTO>().ReverseMap();
			CreateMap<Video, VideoCreateDTO>().ReverseMap();
			CreateMap<Video, VideoUpdateDTO>().ReverseMap();
		}
	}
}
