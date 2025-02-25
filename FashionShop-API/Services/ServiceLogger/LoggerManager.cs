﻿using NLog;
using ILogger = NLog.ILogger;

namespace FashionShop_API.Services.ServiceLogger;

public class LoggerManager :ILoggerManager
{
    private static ILogger _logger = LogManager.GetCurrentClassLogger();

    public LoggerManager()
    {
        
    }
    public void LogInfo(string message)
    {
        _logger.Info(message);
    }

    public void LogDebug(string message)
    {
        _logger.Debug(message);
    }

    public void LogWarn(string message)
    {
        _logger.Warn(message);
    }

    public void LogError(string message)
    {
        _logger.Error(message);
    }
}