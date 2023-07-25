using NUnit.Framework;
using PruneUrl.Backend.Application.Queries.GetShortUrl;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetShortUrl
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetShortUrlQueryUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest()
    {
      const string testShortUrl = "Testing123";
      var query = new GetShortUrlQuery(testShortUrl);
      Assert.That(query.ShortUrl, Is.EqualTo(testShortUrl));
    }

    #endregion Public Methods
  }
}