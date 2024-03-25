using NUnit.Framework;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Queries.Tests;

[TestFixture]
[Parallelizable]
public sealed class GetShortUrlQueryResponseUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    ShortUrl shortUrl = EntityTestHelper.CreateShortUrl();
    var response = new GetShortUrlQueryResponse(shortUrl);
    Assert.That(response.ShortUrl, Is.EqualTo(shortUrl));
  }
}
