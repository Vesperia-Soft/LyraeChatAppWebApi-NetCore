using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Room;
using LyraeChatApp.Domain.ResponseDtosModel;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public class RoomService : IRoomService
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogService _logService;
    #endregion

    #region Ctor
    public RoomService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogService logService
        )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logService = logService;
    }
    #endregion

    #region Methods


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

    public async Task<ResponseDto<RoomModel>> Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                if (!context.Repositories.roomQueryRepository.CheckRoomId(id).Result)
                    throw new Exception("Listelemek istediğiniz oda sistemde bulunamadı");

                var result = context.Repositories.roomQueryRepository.GetById(id).Result;
                var room = _mapper.Map<RoomModel>(result);

                return ResponseDto<RoomModel>.Success(room, 200);
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Odalar listelenirken hata oluştu: {ex.Message}", id.ToString());
                throw;
            }
        }
    }

    public async Task<ResponseDto<PaginationHelper<RoomListModel>>> GetAllRoom(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                var result = context.Repositories.roomQueryRepository.GetAll(request.PageNumber, request.PageSize);

                var paginationHelper = new PaginationHelper<RoomListModel>(result.TotalCount, request.PageSize, request.PageNumber, null);

                var roomList = result.Items.Select(item => _mapper.Map<RoomListModel>(item)).ToList();

                paginationHelper.Items = roomList;

                return ResponseDto<PaginationHelper<RoomListModel>>.Success(paginationHelper, 200);
            }
            catch (Exception ex)
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
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                _logService.LogToDb($"Oda güncellenirken hata oluştu: {ex.Message}", "Admin");
                throw;
            }
        }
    }
    #endregion
}
