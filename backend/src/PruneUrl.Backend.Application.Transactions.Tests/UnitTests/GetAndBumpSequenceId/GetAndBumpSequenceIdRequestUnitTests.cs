using MediatR;
using NUnit.Framework;

namespace PruneUrl.Backend.Application.Transactions.Tests.UnitTests;

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
