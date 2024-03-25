using NUnit.Framework;

namespace PruneUrl.Backend.Application.Commands.Tests;

[TestFixture]
[Parallelizable]
public sealed class CreateSequenceIdCommandUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    const string testId = "Testing123";
    var command = new CreateSequenceIdCommand(testId);
  }
}
