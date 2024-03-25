using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Configuration.Entities.SequenceId;
using PruneUrl.Backend.Application.Exceptions.Database;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Application.Transactions.GetAndBumpSequenceId;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Transactions.Tests.UnitTests.GetAndBumpSequenceId
{
  [TestFixture]
  [Parallelizable]
  public sealed class GetAndBumpSequenceIdRequestHandlerUnitTests
  {
    [Test]
    public void HandleTest_Fail()
    {
      string testId = Guid.NewGuid().ToString();
      var dbTransactionProvider = Substitute.For<IDbTransactionProvider>();
      var sequenceIdOptions = Substitute.For<IOptions<SequenceIdOptions>>();

      SequenceId testSequenceId = EntityTestHelper.CreateSequenceId(testId, 1254);

      sequenceIdOptions.Value.Returns(new SequenceIdOptions() { Id = testId });
      dbTransactionProvider
        .RunTransactionAsync(
          Arg.Any<Func<IDbTransaction<SequenceId>, Task<SequenceId>>>(),
          Arg.Any<CancellationToken>()
        )
        .Returns(x =>
          x.Arg<Func<IDbTransaction<SequenceId>, Task<SequenceId>>>()
            .Invoke(Substitute.For<IDbTransaction<SequenceId>>())
        );

      CancellationToken cancellationToken = CancellationToken.None;
      var requestHandler = new GetAndBumpSequenceIdRequestHandler(
        dbTransactionProvider,
        sequenceIdOptions,
        Substitute.For<ISequenceIdFactory>()
      );
      Assert.That(
        async () =>
          await requestHandler.Handle(new GetAndBumpSequenceIdRequest(), cancellationToken),
        Throws
          .TypeOf<EntityNotFoundException>()
          .With.Message.EqualTo(
            $"Entity of type {typeof(SequenceId)} with id {testId} was not found!"
          )
      );
      _ = sequenceIdOptions.Received(1).Value;
    }

    [Test]
    public async Task HandleTest_Success()
    {
      string testId = Guid.NewGuid().ToString();
      var dbTransactionProvider = Substitute.For<IDbTransactionProvider>();
      var sequenceIdFactory = Substitute.For<ISequenceIdFactory>();
      var sequenceIdOptions = Substitute.For<IOptions<SequenceIdOptions>>();

      SequenceId testSequenceId = EntityTestHelper.CreateSequenceId(testId, 1254);
      var dbTransaction = Substitute.For<IDbTransaction<SequenceId>>();
      SequenceId nextSequenceId = EntityTestHelper.CreateSequenceId(
        testId,
        testSequenceId.Value + 1
      );

      sequenceIdOptions.Value.Returns(new SequenceIdOptions() { Id = testId });
      dbTransaction
        .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
        .Returns(testSequenceId);
      dbTransactionProvider
        .RunTransactionAsync(
          Arg.Any<Func<IDbTransaction<SequenceId>, Task<SequenceId>>>(),
          Arg.Any<CancellationToken>()
        )
        .Returns(x =>
          x.Arg<Func<IDbTransaction<SequenceId>, Task<SequenceId>>>().Invoke(dbTransaction)
        );
      sequenceIdFactory.Create(Arg.Any<string>(), Arg.Any<int>()).Returns(nextSequenceId);

      CancellationToken cancellationToken = CancellationToken.None;
      var requestHandler = new GetAndBumpSequenceIdRequestHandler(
        dbTransactionProvider,
        sequenceIdOptions,
        sequenceIdFactory
      );
      GetAndBumpSequenceIdResponse response = await requestHandler.Handle(
        new GetAndBumpSequenceIdRequest(),
        cancellationToken
      );
      Assert.That(response.SequenceId, Is.EqualTo(testSequenceId));
      await dbTransaction.Received(1).GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
      await dbTransaction.Received(1).GetByIdAsync(testId, cancellationToken);
      dbTransaction.Received(1).Update(Arg.Any<SequenceId>());
      dbTransaction.Received(1).Update(nextSequenceId);
      sequenceIdFactory.Received(1).Create(Arg.Any<string>(), Arg.Any<int>());
      sequenceIdFactory.Received(1).Create(testId, testSequenceId.Value + 1);
      _ = sequenceIdOptions.Received(1).Value;
    }
  }
}
