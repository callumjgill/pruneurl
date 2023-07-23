using MediatR;
using NUnit.Framework;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;

namespace PruneUrl.Backend.Transactions.Tests.UnitTests
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetAndBumpSequenceIdRequestUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest()
    {
      var request = new GetAndBumpSequenceIdRequest();
      Assert.That(request is IRequest<GetAndBumpSequenceIdResponse>, Is.True);
    }

    #endregion Public Methods
  }
}