using AutoMapper;
using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Persistance.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserModel>().ReverseMap();
    }
}
