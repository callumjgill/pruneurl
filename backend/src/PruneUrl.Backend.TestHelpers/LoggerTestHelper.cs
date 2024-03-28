using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace PruneUrl.Backend.TestHelpers;

/// <summary>
/// Helper class for the <see cref="ILogger"/>
/// </summary>
public static class LoggerTestHelper
{
  /// <summary>
  /// Verifies a call to the "Log" method of a substituted <see cref="ILogger{TCategoryName}"/> is made with the expected message and log level.
  /// There is also an optional exception that can be passed if an exception is expected to be logged too.
  /// </summary>
  /// <typeparam name="TCategoryName">The type whose name is used for the logger category name.</typeparam>
  /// <param name="logger">This <see cref="ILogger"/> (substitution).</param>
  /// <param name="expectedMessage">The expected message to be logged.</param>
  /// <param name="expectedLogLevel">The expected log level to have been used.</param>
  /// <param name="invocations">The number of invocations of the method to be expected.</param>
  /// <param name="ex">The exception logged, if any.</param>
  public static void RecievedLogWithStringMessage<TCategoryName>(
    this ILogger<TCategoryName> logger,
    string expectedMessage,
    LogLevel expectedLogLevel,
    int invocations,
    Exception? ex = null
  )
  {
    logger
      .Received(invocations)
      .Log(
        expectedLogLevel,
        Arg.Any<EventId>(),
        Arg.Is<Arg.AnyType>(
          (object o) =>
            string.Equals(expectedMessage, o.ToString(), StringComparison.InvariantCulture)
        ),
        ex,
        Arg.Any<Func<Arg.AnyType, Exception?, string>>()
      );
  }
}
