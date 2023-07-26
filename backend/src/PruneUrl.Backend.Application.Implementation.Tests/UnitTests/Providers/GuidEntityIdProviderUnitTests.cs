using NUnit.Framework;
using PruneUrl.Backend.Application.Implementation.Providers;

namespace PruneUrl.Backend.Application.Implementation.Tests.UnitTests.Providers
{
  [TestFixture]
  [Parallelizable]
  public sealed class GuidEntityIdProviderUnitTests
  {
    #region Public Methods

    [Test]
    public void NewIdTest()
    {
      var entityIdProvider = new GuidEntityIdProvider();
      string actualId1 = entityIdProvider.NewId();
      string actualId2 = entityIdProvider.NewId();
      Assert.That(Guid.TryParse(actualId1, out Guid _), Is.True);
      Assert.That(Guid.TryParse(actualId2, out Guid _), Is.True);
      Assert.That(actualId1, Is.Not.EqualTo(actualId2));
    }

    #endregion Public Methods
  }
}