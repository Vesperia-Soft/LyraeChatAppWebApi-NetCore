using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.UserRoom;
using LyraeChatApp.Domain.ResponseDtosModel;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public class UserRoomService : IUserRoomService
{

    #region Fields
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    #endregion

    #region Ctor
    public UserRoomService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods
    public async Task Create(IList<int> userId, int roomId)
    {
        using (var context = _unitOfWork.Create())
        {
            foreach (var item in userId)
            {
                var userRoom = new CreateUserRoomModel
                {
                    UserId = item,
                    RoomId = roomId
                };

                var userRoomMap = _mapper.Map<UserRoom>(userRoom);
                await context.Repositories.userRoomCommandRepository.AddAsync(userRoomMap);
            }

            context.SaveChanges();
        }
    }

    public async Task<ResponseDto<IList<UserRoomListModel>>> GetUserRoomListAsync(int userId)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.userRoomQueryRepository.GetOtherUsersInSameRooms(userId);

            return ResponseDto<IList<UserRoomListModel>>.Success(result, 200);
        }
    }

    public async Task<bool> CheckUserRoomByUsers(IList<int> userIds)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = await context.Repositories.userRoomQueryRepository.CheckUserRoomByUsers(userIds);

            return result;
        }
    }
    #endregion
}
