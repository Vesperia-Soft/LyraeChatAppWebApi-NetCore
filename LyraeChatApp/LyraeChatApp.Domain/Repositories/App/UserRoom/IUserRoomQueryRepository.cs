﻿using LyraeChatApp.Domain.Models.UserRoom;

namespace LyraeChatApp.Domain.Repositories.App.UserRoom;

public interface IUserRoomQueryRepository
{
    IList<UserRoomListModel> GetOtherUsersInSameRooms(int userId);
    Task<bool> CheckUserRoomByUsers(IList<int> userId);
}
