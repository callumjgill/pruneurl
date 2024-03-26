using NUnit.Framework;

namespace PruneUrl.Backend.Application.Commands.Tests;

[TestFixture]
[Parallelizable]
public sealed class CreateShortUrlCommandResponseUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    const string testShortUrl = "testing123";
    CreateShortUrlCommandResponse response = new(testShortUrl);
    Assert.That(response.ShortUrl, Is.EqualTo(testShortUrl));
  }
}
