using NUnit.Framework;
using PruneUrl.Backend.Application.Commands.CreateShortUrl;

namespace PruneUrl.Backend.Application.Commands.Tests.UnitTests.CreateShortUrl;

[TestFixture]
[Parallelizable]
public sealed class CreateShortUrlCommandUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    string testLongUrl = "testing123";
    int testSequenceId = 6124323;
    var command = new CreateShortUrlCommand(testLongUrl, testSequenceId);
    Assert.That(command.LongUrl, Is.EqualTo(testLongUrl));
    Assert.That(command.SequenceId, Is.EqualTo(testSequenceId));
  }
}
