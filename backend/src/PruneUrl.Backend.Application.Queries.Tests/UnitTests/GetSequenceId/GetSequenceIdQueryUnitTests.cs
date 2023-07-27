using NUnit.Framework;
using PruneUrl.Backend.Application.Queries.GetSequenceId;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetSequenceId
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetSequenceIdQueryUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest()
    {
      const string testId = "Testing123";
      var query = new GetSequenceIdQuery(testId);
      Assert.That(query.Id, Is.EqualTo(testId));
    }

    #endregion Public Methods
  }
}