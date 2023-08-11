using NUnit.Framework;
using PruneUrl.Backend.App.Endpoints.Models;

namespace PruneUrl.Backend.API.Tests.UnitTests.Endpoints.Models
{
  [TestFixture]
  [Parallelizable]
  public sealed class ShortUrlPostRequestUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest()
    {
      const string testLongUrl = "Testing123";
      var shortUrlPostRequest = new ShortUrlPostRequest(testLongUrl);
      Assert.That(shortUrlPostRequest.LongUrl, Is.EqualTo(testLongUrl));
    }

    #endregion Public Methods
  }
}