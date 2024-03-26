using NUnit.Framework;

namespace PruneUrl.Backend.Application.Commands.Tests;

[TestFixture]
[Parallelizable]
public sealed class CreateShortUrlCommandUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    const string testLongUrl = "testing123";
    CreateShortUrlCommand command = new(testLongUrl);
    Assert.That(command.LongUrl, Is.EqualTo(testLongUrl));
  }
}
