using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands.CreateSequenceId;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Commands.Tests.UnitTests.CreateSequenceId
{
  [TestFixture]
  [Parallelizable]
  public sealed class CreateSequenceIdCommandHandlerUnitTests
  {
    [Test]
    public async Task HandleTest()
    {
      const string testId = "Testing123";
      var dbWriteBatch = Substitute.For<IDbWriteBatch<SequenceId>>();
      var sequenceIdFactory = Substitute.For<ISequenceIdFactory>();
      var command = new CreateSequenceIdCommand(testId);
      var cancellationToken = CancellationToken.None;
      SequenceId testSequenceId = EntityTestHelper.CreateSequenceId();

      sequenceIdFactory.Create(Arg.Any<string>(), Arg.Any<int>()).Returns(testSequenceId);

      var handler = new CreateSequenceIdCommandHandler(dbWriteBatch, sequenceIdFactory);
      await handler.Handle(command, cancellationToken);
      sequenceIdFactory.Received(1).Create(Arg.Any<string>(), Arg.Any<int>());
      sequenceIdFactory.Received(1).Create(testId, 0);
      dbWriteBatch.Received(1).Create(Arg.Any<SequenceId>());
      dbWriteBatch.Received(1).Create(testSequenceId);
      await dbWriteBatch.Received(1).CommitAsync(Arg.Any<CancellationToken>());
      await dbWriteBatch.Received(1).CommitAsync(cancellationToken);
    }
  }
}
