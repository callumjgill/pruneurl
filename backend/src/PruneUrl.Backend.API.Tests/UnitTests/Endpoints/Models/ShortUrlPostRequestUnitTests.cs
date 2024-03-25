using NUnit.Framework;

namespace PruneUrl.Backend.API.Tests;

[TestFixture]
[Parallelizable]
public sealed class ShortUrlPostRequestUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    const string testLongUrl = "Testing123";
    var shortUrlPostRequest = new ShortUrlPostRequest(testLongUrl);
    Assert.That(shortUrlPostRequest.LongUrl, Is.EqualTo(testLongUrl));
  }
}
