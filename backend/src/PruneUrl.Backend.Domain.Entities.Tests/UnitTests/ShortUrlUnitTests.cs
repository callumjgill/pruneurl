using NUnit.Framework;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Domain.Entities.Tests.UnitTests
{
  [TestFixture]
  [Parallelizable]
  public sealed class ShortUrlUnitTests
  {
    #region Private Properties

    private static IEnumerable<TestCaseData> constructorTestCases
    {
      get
      {
        yield return new TestCaseData(string.Empty, string.Empty, string.Empty, DateTime.MinValue).SetName("Constructor Test 1");
        yield return new TestCaseData("ab0ed3aa-b540-4732-a11f-1d43333a659d", "https://www.youtube.com", "pruneurl.com/yt", DateTime.Now).SetName("Constructor Test 2");
        yield return new TestCaseData("yes this is an id", "Absolutely is a long url", "Absolutely is a small url", DateTime.UtcNow).SetName("Constructor Test 3");
      }
    }

    #endregion Private Properties

    #region Public Methods

    [TestCaseSource(nameof(constructorTestCases))]
    public void ConstructorTest(string id, string longUrl, string shortUrl, DateTime created)
    {
      ShortUrl testShortUrl = EntityTestHelper.CreateShortUrl(id, longUrl, shortUrl, created);
      Assert.Multiple(() =>
      {
        Assert.That(testShortUrl.Id, Is.EqualTo(id));
        Assert.That(testShortUrl.LongUrl, Is.EqualTo(longUrl));
        Assert.That(testShortUrl.Url, Is.EqualTo(shortUrl));
        Assert.That(testShortUrl.Created, Is.EqualTo(created));
      });
    }

    #endregion Public Methods
  }
}