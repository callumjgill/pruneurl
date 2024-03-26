using NUnit.Framework;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Exceptions.Tests;

[TestFixture]
[Parallelizable]
public sealed class EntityNotFoundExceptionUnitTests
{
  [Test]
  public void MessageTest()
  {
    const string testCriteria = "This is a criteria.";
    EntityNotFoundException<Entity> exception = new(testCriteria);
    Assert.That(
      exception.Message,
      Is.EqualTo($"Entity of type {typeof(Entity)} was not found! {testCriteria}")
    );
  }
}
