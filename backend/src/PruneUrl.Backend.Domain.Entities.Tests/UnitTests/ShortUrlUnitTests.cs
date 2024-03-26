using NUnit.Framework;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Domain.Entities.Tests;

[TestFixture]
[Parallelizable]
public sealed class ShortUrlUnitTests
{
  private static IEnumerable<TestCaseData> ConstructorTestCases
  {
    get
    {
      yield return new TestCaseData(0, string.Empty, string.Empty).SetName("Constructor Test 1");
      yield return new TestCaseData(50, "https://www.youtube.com", "pruneurl.com/yt").SetName(
        "Constructor Test 2"
      );
      yield return new TestCaseData(
        999999,
        "Absolutely is a long url",
        "Absolutely is a small url"
      ).SetName("Constructor Test 3");
    }
  }

  [TestCaseSource(nameof(ConstructorTestCases))]
  public void ConstructorTest(int id, string longUrl, string shortUrl)
  {
    ShortUrl testShortUrl = EntityTestHelper.CreateShortUrl(id, longUrl, shortUrl);
    Assert.Multiple(() =>
    {
      Assert.That(testShortUrl.Id, Is.EqualTo(id));
      Assert.That(testShortUrl.LongUrl, Is.EqualTo(longUrl));
      Assert.That(testShortUrl.Url, Is.EqualTo(shortUrl));
    });
  }
}
