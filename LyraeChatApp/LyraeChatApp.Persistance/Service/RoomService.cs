using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogService _logService;

    public RoomService(IUnitOfWork unitOfWork, IMapper mapper, ILogService logService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logService = logService;
    }

    public async Task<int> CreateRoom(CreateRoomModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                var checkRoom = await context.Repositories.roomQueryRepository.GetByName(model.Name);
                if (checkRoom != 0)
                {
                    return checkRoom;
                }
                var entity = _mapper.Map<Room>(model);
                var insertedId = (int)await context.Repositories.roomCommandRepository.AddAsync(entity);

                context.SaveChanges();

                return insertedId;
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Oda oluşturulurken hata oluştu: {ex.Message}", "Admin");
                throw;
            }
        }
    }

    public async Task<Room> Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                if (!context.Repositories.roomQueryRepository.CheckRoomId(id).Result)
                    throw new Exception("Listelemek istediğiniz oda sistemde bulunamadı");

                var result = context.Repositories.roomQueryRepository.GetById(id).Result;
                return result;
            }catch(Exception ex)
            {
                _logService.LogToDb($"Odalar listelenirken hata oluştu: {ex.Message}", id.ToString());
                throw;
            }
        }
    }

    public PaginationHelper<RoomListModel> GetAllRoom(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                var result = context.Repositories.roomQueryRepository.GetAll(request.PageNumber, request.PageSize);
                return result;
            }catch(Exception ex)
            {
                _logService.LogToDb($"Odalar listelenirken hata oluştu: {ex.Message}", "");
                throw;
            }
        }
    }

    public async Task RemoveRoom(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                if (!context.Repositories.roomQueryRepository.CheckRoomId(id).Result)
                    throw new Exception("Silmek istediğiniz oda sistemde bulunamadı");

                await context.Repositories.roomCommandRepository.RemoveById(id);
                context.SaveChanges();
            }catch(Exception ex)
            {
                _logService.LogToDb($"Oda silinirken hata oluştu: {ex.Message}", id.ToString());
                throw;
            }
        }
    }

    public async void UpdateRoom(UpdateRoomModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                if (!context.Repositories.roomQueryRepository.CheckRoomId(model.Id).Result)
                    throw new Exception("Güncellemek istediğiniz oda  sistemde bulunamadı");

                var entity = _mapper.Map<Room>(model);
                entity.UpdateDate = DateTime.Now;
                entity.UpdaterName = "Admin";
                context.Repositories.roomCommandRepository.Update(entity);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                _logService.LogToDb($"Oda güncellenirken hata oluştu: {ex.Message}", "Admin");
                throw;
            }
        }
    }
}
