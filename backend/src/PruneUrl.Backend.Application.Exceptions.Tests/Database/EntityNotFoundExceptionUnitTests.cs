using NUnit.Framework;
using PruneUrl.Backend.Application.Exceptions.Database;

namespace PruneUrl.Backend.Application.Exceptions.Tests.Database
{
  [TestFixture]
  [Parallelizable]
  public sealed class EntityNotFoundExceptionUnitTests
  {
    #region Public Methods

    [Test]
    public void MessageTest()
    {
      Type dummyType = typeof(string);
      string testId = Guid.NewGuid().ToString();
      var exception = new EntityNotFoundException(dummyType, testId);
      Assert.That(exception.Message, Is.EqualTo($"Entity of type {dummyType} with id {testId} was not found!"));
    }

    #endregion Public Methods
  }
}