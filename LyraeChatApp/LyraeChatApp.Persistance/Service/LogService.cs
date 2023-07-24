using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.LogModels;
using LyraeChatApp.Domain.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace LyraeChatApp.Persistance.Service;

public class LogService : ILogService
{
    #region Fields
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LogService> _logger;
    #endregion
    #region Ctor
    public LogService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<LogService> logger
      )
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods
    public async void LogToDb(string message, string userName)
    {
        using (var context = _unitOfWork.Create())
        {
            await context.Repositories.logCommandRepository.AddAsync(new LogModel
            {
                LogDate = DateTime.Now,
                LogMessage = message,
                UserName = userName
            });
            context.SaveChanges();
        }
    }
    #endregion
}
