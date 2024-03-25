using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PruneUrl.Backend.Application.Exceptions;
using PruneUrl.Backend.Application.Interfaces;
using PruneUrl.Backend.Application.Interfaces.Database;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Queries.Tests;

[TestFixture]
[Parallelizable]
public sealed class GetShortUrlQueryHandlerUnitTests
{
  [Test]
  public async Task GetShortUrlTest_EntityFound()
  {
    const string testShortUrl = "hsfwgss";
    const int testSequenceId = 233243;
    var dbGetByIdOperation = Substitute.For<IDbGetByIdOperation<ShortUrl>>();
    var sequenceIdProvider = Substitute.For<ISequenceIdProvider>();
    ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl();
    var query = new GetShortUrlQuery(testShortUrl);
    var cancellationToken = CancellationToken.None;

    dbGetByIdOperation
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
      .Returns(testShortUrlEntity);
    sequenceIdProvider.GetSequenceId(Arg.Any<string>()).Returns(testSequenceId);

    var handler = new GetShortUrlQueryHandler(dbGetByIdOperation, sequenceIdProvider);
    GetShortUrlQueryResponse response = await handler.Handle(query, cancellationToken);
    Assert.That(response.ShortUrl, Is.EqualTo(testShortUrlEntity));
    await dbGetByIdOperation
      .Received(1)
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    await dbGetByIdOperation.Received(1).GetByIdAsync(testSequenceId.ToString(), cancellationToken);
    sequenceIdProvider.Received(1).GetSequenceId(Arg.Any<string>());
    sequenceIdProvider.Received(1).GetSequenceId(testShortUrl);
  }

  [Test]
  public async Task GetShortUrlTest_EntityNotFound()
  {
    const string testShortUrl = "hsfwgss";
    const int testSequenceId = 233243;
    var dbGetByIdOperation = Substitute.For<IDbGetByIdOperation<ShortUrl>>();
    var sequenceIdProvider = Substitute.For<ISequenceIdProvider>();
    ShortUrl testShortUrlEntity = EntityTestHelper.CreateShortUrl();
    var query = new GetShortUrlQuery(testShortUrl);
    var cancellationToken = CancellationToken.None;

    sequenceIdProvider.GetSequenceId(Arg.Any<string>()).Returns(testSequenceId);

    var handler = new GetShortUrlQueryHandler(dbGetByIdOperation, sequenceIdProvider);
    Assert.That(
      async () => await handler.Handle(query, cancellationToken),
      Throws
        .TypeOf<EntityNotFoundException>()
        .With.Message.EqualTo(
          $"Entity of type {typeof(ShortUrl)} with id {testSequenceId} was not found!"
        )
    );
    await dbGetByIdOperation
      .Received(1)
      .GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    await dbGetByIdOperation.Received(1).GetByIdAsync(testSequenceId.ToString(), cancellationToken);
    sequenceIdProvider.Received(1).GetSequenceId(Arg.Any<string>());
    sequenceIdProvider.Received(1).GetSequenceId(testShortUrl);
  }
}
