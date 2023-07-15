﻿using NUnit.Framework;
using PruneUrl.Backend.Application.Implementation.Providers;

namespace PruneUrl.Backend.Application.Implementation.Tests.UnitTests.Providers
{
  [TestFixture]
  [Parallelizable]
  public sealed class ShortUrlProviderUnitTests
  {
    #region Public Methods

    [Test]
    public void GetShortUrlTest_GeneratesExpectedShortUrl_First62NumbersMapsToSingleChar()
    {
      var provider = new ShortUrlProvider();
      char[] characterMap = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
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
    public void GetShortUrlTest_GeneratesExpectedShortUrl_OtherNumbers(int sequenceId, string expectedShortUrl)
    {
      var provider = new ShortUrlProvider();
      string actualUrl = provider.GetShortUrl(sequenceId);
      Assert.That(actualUrl, Is.EqualTo(expectedShortUrl));
    }

    #endregion Public Methods
  }
}