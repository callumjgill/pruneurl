using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.App.Endpoints;

namespace PruneUrl.Backend.API.Tests.UnitTests.Endpoints
{
  [TestFixture]
  [Parallelizable]
  public sealed class EndpointRestMethodsUtilitiesUnitTests
  {
    #region Public Methods

    [Test]
    public async Task HandleErrorsTest_ExceptionThrown()
    {
      IResult actualResult = await EndpointRestMethodsUtilities.HandleErrors(() => Task.FromException<IResult>(new Exception()));
      Assert.That(actualResult, Is.TypeOf<StatusCodeHttpResult>());
      Assert.That(((StatusCodeHttpResult)actualResult).StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task HandleErrorsTest_NoExceptionThrown()
    {
      var result = Substitute.For<IResult>();

      IResult actualResult = await EndpointRestMethodsUtilities.HandleErrors(() => Task.FromResult(result));
      Assert.That(actualResult, Is.EqualTo(result));
    }

    #endregion Public Methods
  }
}