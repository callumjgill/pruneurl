using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NUnit.Framework;

namespace PruneUrl.Backend.API.Tests;

[TestFixture]
[Parallelizable]
public sealed class EndpointRestMethodsUtilitiesUnitTests
{
  [Test]
  public async Task HandleErrorsTest_ExceptionThrown()
  {
    Exception exception = new("This is an error!");
    IResult result = await EndpointRestMethodsUtilities.HandleErrors(
      () => Task.FromException<IResult>(exception)
    );
    Assert.Multiple(() =>
    {
      Assert.That(result, Is.TypeOf<ProblemHttpResult>());
      Assert.That(((ProblemHttpResult)result).StatusCode, Is.EqualTo(500));
      Assert.That(((ProblemHttpResult)result).ProblemDetails.Detail, Is.EqualTo(exception.Message));
    });
  }

  [Test]
  public async Task HandleErrorsTest_NoExceptionThrown()
  {
    var result = Substitute.For<IResult>();

    IResult actualResult = await EndpointRestMethodsUtilities.HandleErrors(
      () => Task.FromResult(result)
    );
    Assert.That(actualResult, Is.EqualTo(result));
  }
}
