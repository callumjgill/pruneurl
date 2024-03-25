using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Commands;
using PruneUrl.Backend.Application.Configuration;
using PruneUrl.Backend.Application.Queries;
using PruneUrl.Backend.TestHelpers;

namespace PruneUrl.Backend.API.Tests;

[TestFixture]
[Parallelizable]
public sealed class HostExtensionsUnitTests
{
  [TestCase(true)]
  [TestCase(false)]
  public async Task EnsureDbIsSetupTest(bool sequenceIdNull)
  {
    var host = Substitute.For<IHost>();
    var serviceProvider = Substitute.For<IServiceProvider>();
    var mediator = Substitute.For<IMediator>();
    var options = Substitute.For<IOptions<SequenceIdOptions>>();
    var sequenceIdOptions = new SequenceIdOptions() { Id = "Testing123" };

    options.Value.Returns(sequenceIdOptions);

    serviceProvider.GetService(typeof(IMediator)).Returns(mediator);
    serviceProvider.GetService(typeof(IOptions<SequenceIdOptions>)).Returns(options);

    host.Services.Returns(serviceProvider);

    mediator
      .Send(Arg.Any<GetSequenceIdQuery>(), Arg.Any<CancellationToken>())
      .Returns(
        new GetSequenceIdQueryResponse(sequenceIdNull ? null : EntityTestHelper.CreateSequenceId())
      );

    await host.EnsureDbIsSetup();

    await mediator.Received(1).Send(Arg.Any<GetSequenceIdQuery>(), Arg.Any<CancellationToken>());
    await mediator
      .Received(sequenceIdNull ? 1 : 0)
      .Send(Arg.Any<CreateSequenceIdCommand>(), Arg.Any<CancellationToken>());
  }
}
