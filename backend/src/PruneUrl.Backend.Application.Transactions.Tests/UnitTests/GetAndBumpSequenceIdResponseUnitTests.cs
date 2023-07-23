using NUnit.Framework;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Transactions.Tests.UnitTests
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetAndBumpSequenceIdResponseUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest()
    {
      var sequenceId = EntityTestHelper.CreateSequenceId();
      var response = new GetAndBumpSequenceIdResponse(sequenceId);
      Assert.That(response.SequenceId, Is.EqualTo(sequenceId));
    }

    #endregion Public Methods
  }
}