using NUnit.Framework;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Infrastructure.Database.Tests.UnitTests.Utilities
{
  [TestFixture]
  [Parallelizable]
  public sealed class CollectionReferenceHelperUnitTests
  {
    [TestCase<IEntity>("IEntitys")]
    [TestCase<ShortUrl>("ShortUrls")]
    [TestCase<SequenceId>("SequenceIds")]
    public void GetCollectionPathTest<T>(string expectedResult)
      where T : IEntity
    {
      Assert.That(CollectionReferenceHelper.GetCollectionPath<T>(), Is.EqualTo(expectedResult));
    }
  }
}
