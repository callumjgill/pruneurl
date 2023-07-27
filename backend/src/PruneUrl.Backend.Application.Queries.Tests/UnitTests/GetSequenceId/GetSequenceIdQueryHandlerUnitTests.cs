using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.Operations.Read;
using PruneUrl.Backend.Application.Queries.GetSequenceId;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Queries.Tests.UnitTests.GetSequenceId
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetSequenceIdQueryHandlerUnitTests
  {
    #region Public Methods

    [TestCase(true)]
    [TestCase(false)]
    public async Task HandleTest(bool isNull)
    {
      const string testId = "Testing123";
      var dbGetByIdOperation = Substitute.For<IDbGetByIdOperation<SequenceId>>();
      SequenceId? testSequenceId = isNull ? null : EntityTestHelper.CreateSequenceId();

      dbGetByIdOperation.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(testSequenceId);

      var cancellationToken = CancellationToken.None;
      var query = new GetSequenceIdQuery(testId);
      var handler = new GetSequenceIdQueryHandler(dbGetByIdOperation);
      GetSequenceIdQueryResponse response = await handler.Handle(query, cancellationToken);

      Assert.That(response.SequenceId, Is.EqualTo(testSequenceId));
      await dbGetByIdOperation.Received(1).GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
      await dbGetByIdOperation.Received(1).GetByIdAsync(testId, cancellationToken);
    }

    #endregion Public Methods
  }
}