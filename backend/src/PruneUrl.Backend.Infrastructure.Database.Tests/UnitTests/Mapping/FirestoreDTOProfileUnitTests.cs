using AutoMapper;
using NUnit.Framework;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Mapping;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Mapping
{
  [TestFixture]
  [Parallelizable]
  public sealed class FirestoreDTOProfileUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest_MappingsAreValid()
    {
      var configuration = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<FirestoreDTOProfile>();
      });
      Assert.That(configuration.AssertConfigurationIsValid, Throws.Nothing);
    }

    #endregion Public Methods
  }
}