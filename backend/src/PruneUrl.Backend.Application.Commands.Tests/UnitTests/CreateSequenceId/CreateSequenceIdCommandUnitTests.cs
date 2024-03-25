using NUnit.Framework;
using PruneUrl.Backend.Application.Commands.CreateSequenceId;

namespace PruneUrl.Backend.Application.Commands.Tests.UnitTests.CreateSequenceId;

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
