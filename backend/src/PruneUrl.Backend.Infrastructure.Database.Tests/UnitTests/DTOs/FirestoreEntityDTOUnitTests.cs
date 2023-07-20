using NUnit.Framework;
using PruneUrl.Backend.Infrastructure.Database.Firestore.DTOs;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.DTOs
{
  internal abstract class FirestoreEntityDTOUnitTests
  {
    #region Public Methods

    [Test]
    public void IdTest()
    {
      string testId = Guid.NewGuid().ToString();
      PropertyTest(null, testId, (entityDTO) => entityDTO.Id, (entityDTO, newValue) => entityDTO.Id = newValue);
    }

    #endregion Public Methods

    #region Protected Methods

    protected abstract FirestoreEntityDTO CreateDTO();

    protected void PropertyTest<T>(T? defaultValue, T newValue, Func<FirestoreEntityDTO, T> getProperty, Action<FirestoreEntityDTO, T> setProperty)
    {
      FirestoreEntityDTO entityDTO = CreateDTO();
      Assert.That(getProperty(entityDTO), Is.EqualTo(defaultValue));
      setProperty(entityDTO, newValue);
      Assert.That(getProperty(entityDTO), Is.EqualTo(newValue));
    }

    #endregion Protected Methods
  }
}