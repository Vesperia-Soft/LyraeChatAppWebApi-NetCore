using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.LogModels;
using LyraeChatApp.Domain.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace LyraeChatApp.Persistance.Service;

public class LogService : ILogService
{
    private readonly ILogger<LogService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public LogService(ILogger<LogService> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

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
}
