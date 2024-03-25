using NUnit.Framework;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests;

[TestFixture]
[Parallelizable]
public sealed class FirestoreTransactionOptionsUnitTests
{
  [TestCase(0)]
  [TestCase(1)]
  [TestCase(2)]
  [TestCase(4)]
  [TestCase(8)]
  [TestCase(16)]
  [TestCase(32)]
  [TestCase(64)]
  [TestCase(128)]
  [TestCase(256)]
  public void MaxAttemptsTest(int maxAttemptValues)
  {
    var options = new FirestoreTransactionOptions();
    Assert.That(options.MaxAttempts, Is.EqualTo(default(int)));
    options.MaxAttempts = maxAttemptValues;
    Assert.That(options.MaxAttempts, Is.EqualTo(maxAttemptValues));
  }
}
