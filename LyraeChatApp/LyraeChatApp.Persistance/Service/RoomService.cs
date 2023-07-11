using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateRoom(CreateRoomModel model)
    {
       using(var context = _unitOfWork.Create())
        {
            var entity = _mapper.Map<Room>(model);
            await context.Repositories.roomCommandRepository.AddAsync(entity);

            context.SaveChanges();
        }
    }

    public async Task<Room> Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            if (!context.Repositories.roomQueryRepository.CheckRoomId(id).Result)
                throw new Exception("Listelemek istediğiniz oda sistemde bulunamadı");

            var result = context.Repositories.roomQueryRepository.GetById(id).Result;
            return result;
        }
    }

    public PaginationHelper<RoomListModel> GetAllRoom(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.roomQueryRepository.GetAll(request.PageNumber, request.PageSize);
            return result;
        }
    }

    public async Task RemoveRoom(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            if (!context.Repositories.roomQueryRepository.CheckRoomId(id).Result)
                throw new Exception("Silmek istediğiniz oda sistemde bulunamadı");

            await context.Repositories.roomCommandRepository.RemoveById(id);
            context.SaveChanges();
        }
    }

    public async void UpdateRoom(UpdateRoomModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            if (!context.Repositories.roomQueryRepository.CheckRoomId(model.Id).Result)
                throw new Exception("Güncellemek istediğiniz oda  sistemde bulunamadı");

            var entity = _mapper.Map<Room>(model);
            entity.UpdateDate = DateTime.Now;
            entity.UpdaterName = "Admin";
            context.Repositories.roomCommandRepository.Update(entity);
            context.SaveChanges();
        }
    }
}
