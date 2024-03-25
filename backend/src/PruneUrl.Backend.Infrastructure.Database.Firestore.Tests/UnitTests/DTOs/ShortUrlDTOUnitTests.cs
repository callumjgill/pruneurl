using NUnit.Framework;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.DTOs
{
  [TestFixture]
  [Parallelizable]
  internal sealed class ShortUrlDTOUnitTests : FirestoreEntityDTOUnitTests
  {
    [Test]
    public void CreatedTest()
    {
      DateTime testCreated = DateTime.Now;
      PropertyTest(
        null,
        testCreated,
        (entityDTO) => ((ShortUrlDTO)entityDTO).Created,
        (entityDTO, newValue) => ((ShortUrlDTO)entityDTO).Created = newValue
      );
    }

    [Test]
    public void LongUrlTest()
    {
      string testLongUrl = "Some long url would be here";
      PropertyTest(
        null,
        testLongUrl,
        (entityDTO) => ((ShortUrlDTO)entityDTO).LongUrl,
        (entityDTO, newValue) => ((ShortUrlDTO)entityDTO).LongUrl = newValue
      );
    }

    [Test]
    public void UrlTest()
    {
      string testUrl = "Some short url would be here";
      PropertyTest(
        null,
        testUrl,
        (entityDTO) => ((ShortUrlDTO)entityDTO).Url,
        (entityDTO, newValue) => ((ShortUrlDTO)entityDTO).Url = newValue
      );
    }

    protected override FirestoreEntityDTO CreateDTO()
    {
      return new ShortUrlDTO();
    }
  }
}
