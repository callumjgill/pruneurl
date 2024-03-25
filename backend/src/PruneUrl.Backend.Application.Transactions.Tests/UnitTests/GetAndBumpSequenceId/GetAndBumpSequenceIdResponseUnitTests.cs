using NUnit.Framework;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Transactions.Tests.UnitTests;

[TestFixture]
[Parallelizable]
public sealed class GetAndBumpSequenceIdResponseUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    var sequenceId = EntityTestHelper.CreateSequenceId();
    var response = new GetAndBumpSequenceIdResponse(sequenceId);
    Assert.That(response.SequenceId, Is.EqualTo(sequenceId));
  }
}
