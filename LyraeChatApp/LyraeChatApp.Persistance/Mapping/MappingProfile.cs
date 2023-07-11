using AutoMapper;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Persistance.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region UserMapProfile
        CreateMap<User, CreateUserModel>().ReverseMap();
        #endregion

        #region DepartmentMapProfile
        CreateMap<Department, CreateDepartmentModel>().ReverseMap();
        CreateMap<Department, UpdateDepartmentModel>().ReverseMap();
        #endregion

        #region Room
        CreateMap<Room,CreateRoomModel>().ReverseMap(); 
        CreateMap<Room,UpdateRoomModel>().ReverseMap();
        #endregion
    }
}
