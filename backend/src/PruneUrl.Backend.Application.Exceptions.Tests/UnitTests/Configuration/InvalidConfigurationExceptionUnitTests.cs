using NUnit.Framework;
using PruneUrl.Backend.Application.Configuration.Exceptions;

namespace PruneUrl.Backend.Application.Exceptions.Tests.UnitTests.Configuration
{
  [TestFixture]
  [Parallelizable]
  public sealed class InvalidConfigurationExceptionUnitTests
  {
    #region Public Methods

    [Test]
    public void ConstructorTest()
    {
      const string sectionName = "TestSection";
      const string error = "TestError";
      var invalidConfigException = new InvalidConfigurationException(sectionName, error);
      Assert.That(invalidConfigException.Message, Is.EqualTo($"The section '{sectionName}' is invalid! {error}"));
    }

    #endregion Public Methods
  }
}