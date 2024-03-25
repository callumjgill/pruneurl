using NUnit.Framework;

namespace PruneUrl.Backend.Application.Queries.Tests;

[TestFixture]
[Parallelizable]
public sealed class GetSequenceIdQueryUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    const string testId = "Testing123";
    var query = new GetSequenceIdQuery(testId);
    Assert.That(query.Id, Is.EqualTo(testId));
  }
}
