using NUnit.Framework;
using PruneUrl.Backend.Application.Queries.GetSequenceId;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetSequenceId;

[TestFixture]
[Parallelizable]
public sealed class GetSequenceIdQueryResponseUnitTests
{
  [TestCase(true)]
  [TestCase(false)]
  public void ConstructorTest(bool isNull)
  {
    SequenceId? testSequenceId = isNull ? null : EntityTestHelper.CreateSequenceId();
    var response = new GetSequenceIdQueryResponse(testSequenceId);
    Assert.That(response.SequenceId, Is.EqualTo(testSequenceId));
  }
}
