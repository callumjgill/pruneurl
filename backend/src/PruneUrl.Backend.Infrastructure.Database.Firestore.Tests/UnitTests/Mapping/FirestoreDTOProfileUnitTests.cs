using AutoMapper;
using NUnit.Framework;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests;

[TestFixture]
[Parallelizable]
public sealed class FirestoreDTOProfileUnitTests
{
  [Test]
  public void ConstructorTest_MappingsAreValid()
  {
    var configuration = new MapperConfiguration(cfg =>
    {
      cfg.AddProfile<FirestoreDTOProfile>();
    });
    Assert.That(configuration.AssertConfigurationIsValid, Throws.Nothing);
  }
}
