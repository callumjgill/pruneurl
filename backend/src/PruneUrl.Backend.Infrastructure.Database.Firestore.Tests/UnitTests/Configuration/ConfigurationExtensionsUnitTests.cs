using Google.Api.Gax;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PruneUrl.Backend.Application.Configuration.Exceptions;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Configuration
{
  [TestFixture]
  [Parallelizable]
  public sealed class ConfigurationExtensionsUnitTests
  {
    [Test]
    public void GetFirestoreDbOptionsTest_Invalid_NoConfig()
    {
      IConfiguration configuration = new ConfigurationBuilder().Build();
      Assert.That(
        configuration.GetFirestoreDbOptions,
        Throws
          .TypeOf<InvalidConfigurationException>()
          .With.Message.EqualTo(
            $"The section '{nameof(FirestoreDbOptions)}' is invalid! Could not bind the section to the type '{typeof(FirestoreDbOptions)}'."
          )
      );
    }

    [TestCase("")]
    [TestCase("           ")]
    [TestCase(null)]
    public void GetFirestoreDbOptionsTest_Invalid_ProjectIdInvalid(string? projectId)
    {
      EmulatorDetection emulatorDetection = EmulatorDetection.EmulatorOnly;
      var config = new Dictionary<string, string?>
      {
        { "FirestoreDbOptions:EmulatorDetection", emulatorDetection.ToString() },
        { "FirestoreDbOptions:ProjectId", projectId }
      };
      IConfiguration configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(config)
        .Build();
      Assert.That(
        configuration.GetFirestoreDbOptions,
        Throws
          .TypeOf<InvalidConfigurationException>()
          .With.Message.EqualTo(
            $"The section '{nameof(FirestoreDbOptions)}' is invalid! Missing '{nameof(FirestoreDbOptions.ProjectId)}' property!"
          )
      );
    }

    [Test]
    public void GetFirestoreDbOptionsTest_Valid()
    {
      string projectId = "Testing123";
      EmulatorDetection emulatorDetection = EmulatorDetection.EmulatorOnly;
      var config = new Dictionary<string, string?>
      {
        { "FirestoreDbOptions:EmulatorDetection", emulatorDetection.ToString() },
        { "FirestoreDbOptions:ProjectId", projectId }
      };
      IConfiguration configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(config)
        .Build();
      FirestoreDbOptions firestoreDbOptions = configuration.GetFirestoreDbOptions();
      Assert.That(firestoreDbOptions.EmulatorDetection, Is.EqualTo(emulatorDetection));
      Assert.That(firestoreDbOptions.ProjectId, Is.EqualTo(projectId));
    }
  }
}
