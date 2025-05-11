using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Extensions;

public static class LoggerExtensions
{
    public static void VerifyLoggedMessage<T>(
        this Mock<ILogger<T>> mockLogger,
        LogLevel logLevel,
        string expectedMessage,
       Func<Times> times)
    {
        mockLogger.Verify(logger =>
            logger.Log(
                logLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(expectedMessage)),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ),
            times
        );
    }
}
