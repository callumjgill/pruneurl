using NUnit.Framework;

namespace PruneUrl.Backend.Application.Queries.Tests;

[TestFixture]
[Parallelizable]
public sealed class GetShortUrlQueryUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    const string testShortUrl = "Testing123";
    var query = new GetShortUrlQuery(testShortUrl);
    Assert.That(query.ShortUrl, Is.EqualTo(testShortUrl));
  }
}
