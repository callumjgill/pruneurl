using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands.CreateShortUrl;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Application.Interfaces.Factories.Entities;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.Application.Commands.Tests.UnitTests.CreateShortUrl
{
  [TestFixture]
  [Parallelizable]
  public sealed class CreateShortUrlCommandHandlerUnitTests
  {
    #region Public Methods

    [Test]
    public async Task HandleTest()
    {
      string testLongUrl = "https://www.youtube.com";
      int testSequenceId = 6124323;
      var dbWriteBatch = Substitute.For<IDbWriteBatch<ShortUrl>>();
      var shortUrlFactory = Substitute.For<IShortUrlFactory>();
      ShortUrl testShortUrl = EntityTestHelper.CreateShortUrl();
      CancellationToken cancellationToken = CancellationToken.None;

      shortUrlFactory.Create(Arg.Any<string>(), Arg.Any<int>()).Returns(testShortUrl);
      dbWriteBatch.CommitAsync(Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

      var handler = new CreateShortUrlCommandHandler(dbWriteBatch, shortUrlFactory);
      var command = new CreateShortUrlCommand(testLongUrl, testSequenceId);

      await handler.Handle(command, cancellationToken);

      shortUrlFactory.Received(1).Create(Arg.Any<string>(), Arg.Any<int>());
      shortUrlFactory.Received(1).Create(testLongUrl, testSequenceId);
      dbWriteBatch.Received(1).Create(Arg.Any<ShortUrl>());
      dbWriteBatch.Received(1).Create(testShortUrl);
      await dbWriteBatch.Received(1).CommitAsync(Arg.Any<CancellationToken>());
      await dbWriteBatch.Received(1).CommitAsync(cancellationToken);
    }

    #endregion Public Methods
  }
}