using MediatR;
using NUnit.Framework;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;

namespace PruneUrl.Backend.Application.Transactions.Tests.UnitTests.GetAndBumpSequenceId;

[TestFixture]
[Parallelizable]
public sealed class GetAndBumpSequenceIdRequestUnitTests
{
  [Test]
  public void ConstructorTest()
  {
    var request = new GetAndBumpSequenceIdRequest();
    Assert.That(request is IRequest<GetAndBumpSequenceIdResponse>, Is.True);
  }
}
