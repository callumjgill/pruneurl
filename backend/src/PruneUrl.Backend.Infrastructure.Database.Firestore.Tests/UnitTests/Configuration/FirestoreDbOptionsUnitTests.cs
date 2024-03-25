using Google.Api.Gax;
using NUnit.Framework;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Configuration
{
  [TestFixture]
  [Parallelizable]
  public sealed class FirestoreDbOptionsUnitTests
  {
    [TestCase(EmulatorDetection.None)]
    [TestCase(EmulatorDetection.ProductionOnly)]
    [TestCase(EmulatorDetection.EmulatorOrProduction)]
    [TestCase(EmulatorDetection.EmulatorOnly)]
    public void EmulatorDetectionTest(EmulatorDetection emulatorDetection)
    {
      var firestoreDbOptions = new FirestoreDbOptions();
      Assert.That(firestoreDbOptions.EmulatorDetection, Is.EqualTo(default(EmulatorDetection)));
      firestoreDbOptions.EmulatorDetection = emulatorDetection;
      Assert.That(firestoreDbOptions.EmulatorDetection, Is.EqualTo(emulatorDetection));
    }

    [Test]
    public void ProjectIdTest()
    {
      string testProjectId = "Testing123";
      var firestoreDbOptions = new FirestoreDbOptions();
      Assert.That(firestoreDbOptions.ProjectId, Is.EqualTo(string.Empty));
      firestoreDbOptions.ProjectId = testProjectId;
      Assert.That(firestoreDbOptions.ProjectId, Is.EqualTo(testProjectId));
    }
  }
}
