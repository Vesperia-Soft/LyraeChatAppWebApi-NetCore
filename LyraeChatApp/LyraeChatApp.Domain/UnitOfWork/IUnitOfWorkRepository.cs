﻿using LyraeChatApp.Domain.Models;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;

namespace LyraeChatApp.Domain.UnitOfWork;

public interface IUnitOfWorkRepository
{
    IUserCommandRepository userCommandRepository { get; }
    IUserQueryRepository userQueryRepository { get; }
}