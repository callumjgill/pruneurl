using System.Text.Unicode;
using NUnit.Framework;

namespace PruneUrl.Backend.Application.Implementation.Tests;

[TestFixture]
[Parallelizable]
public sealed class ShortUrlProviderUnitTests
{
  [Test]
  public void GetSequenceIdTest_GeneratesExpectedSequenceId_First62CharMapsToFirst62Numbers()
  {
    var provider = new ShortUrlProvider();
    char[] characterMap =
      "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    Assert.Multiple(() =>
    {
      for (int index = 0; index < characterMap.Length; index++)
      {
        int actualSequenceId = provider.GetSequenceId(characterMap[index].ToString());
        Assert.That(actualSequenceId, Is.EqualTo(index));
      }
    });
  }

  [TestCase(int.MaxValue, "2lkCB1")]
  [TestCase(721059154, "MNuqm")]
  [TestCase(357302113, "obcBj")]
  [TestCase(62, "10")]
  [TestCase(63, "11")]
  [TestCase(3844, "100")]
  [TestCase(3845, "101")]
  public void GetSequenceIdTest_GeneratesExpectedSequenceId_OtherStrings(
    int expectedSequenceId,
    string shortUrl
  )
  {
    var provider = new ShortUrlProvider();
    int actualSequenceId = provider.GetSequenceId(shortUrl);
    Assert.That(actualSequenceId, Is.EqualTo(expectedSequenceId));
  }

  [Test]
  public void GetSequenceIdTest_ReturnsNegativeOne_InvalidCharacters()
  {
    var provider = new ShortUrlProvider();
    IEnumerable<int> characterMap =
      "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(Convert.ToInt32);
    UnicodeRange unicodeRange = UnicodeRanges.All;
    IEnumerable<char> nonAlphanumericUnicodeCharacters = Enumerable
      .Range(unicodeRange.FirstCodePoint, unicodeRange.Length)
      .Except(characterMap)
      .Select(Convert.ToChar);
    Assert.Multiple(() =>
    {
      foreach (char nonAlphanumericUnicodeCharacter in nonAlphanumericUnicodeCharacters)
      {
        int actualSequenceId = provider.GetSequenceId(nonAlphanumericUnicodeCharacter.ToString());
        Assert.That(actualSequenceId, Is.EqualTo(-1));
      }
    });
  }

  [Test]
  public void GetSequenceIdTest_ReturnsNegativeOne_InvalidCharactersInString()
  {
    var provider = new ShortUrlProvider();
    IEnumerable<int> characterMap =
      "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(Convert.ToInt32);
    UnicodeRange unicodeRange = UnicodeRanges.All;
    string nonAlphanumericUnicodeCharactersString = string.Concat(
      Enumerable
        .Range(unicodeRange.FirstCodePoint, unicodeRange.Length)
        .Except(characterMap)
        .Select(Convert.ToChar)
    );
    int actualSequenceId = provider.GetSequenceId(nonAlphanumericUnicodeCharactersString);
    Assert.That(actualSequenceId, Is.EqualTo(-1));
  }

  [Test]
  public void GetShortUrlTest_GeneratesExpectedShortUrl_First62NumbersMapsToSingleChar()
  {
    var provider = new ShortUrlProvider();
    char[] characterMap =
      "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    Assert.Multiple(() =>
    {
      foreach (int sequenceId in Enumerable.Range(0, characterMap.Length))
      {
        string actualShortUrl = provider.GetShortUrl(sequenceId);
        Assert.That(actualShortUrl, Is.EqualTo(characterMap[sequenceId].ToString()));
      }
    });
  }

  [TestCase(int.MaxValue, "2lkCB1")]
  [TestCase(721059154, "MNuqm")]
  [TestCase(357302113, "obcBj")]
  [TestCase(62, "10")]
  [TestCase(63, "11")]
  [TestCase(3844, "100")]
  [TestCase(3845, "101")]
  public void GetShortUrlTest_GeneratesExpectedShortUrl_OtherNumbers(
    int sequenceId,
    string expectedShortUrl
  )
  {
    var provider = new ShortUrlProvider();
    string actualUrl = provider.GetShortUrl(sequenceId);
    Assert.That(actualUrl, Is.EqualTo(expectedShortUrl));
  }
}
