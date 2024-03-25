using NUnit.Framework;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests;

internal abstract class FirestoreEntityDTOUnitTests
{
  [Test]
  public void IdTest()
  {
    string testId = Guid.NewGuid().ToString();
    PropertyTest(
      null,
      testId,
      (entityDTO) => entityDTO.Id,
      (entityDTO, newValue) => entityDTO.Id = newValue
    );
  }

  protected abstract FirestoreEntityDTO CreateDTO();

  protected void PropertyTest<T>(
    T? defaultValue,
    T newValue,
    Func<FirestoreEntityDTO, T> getProperty,
    Action<FirestoreEntityDTO, T> setProperty
  )
  {
    FirestoreEntityDTO entityDTO = CreateDTO();
    Assert.That(getProperty(entityDTO), Is.EqualTo(defaultValue));
    setProperty(entityDTO, newValue);
    Assert.That(getProperty(entityDTO), Is.EqualTo(newValue));
  }
}
