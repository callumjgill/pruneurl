using NUnit.Framework;
using PruneUrl.Backend.Application.Queries.GetShortUrl;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetShortUrl
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetShortUrlQueryResponseUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest()
    {
      ShortUrl shortUrl = EntityTestHelper.CreateShortUrl();
      var response = new GetShortUrlQueryResponse(shortUrl);
      Assert.That(response.ShortUrl, Is.EqualTo(shortUrl));
    }

    #endregion Public Methods
  }
}