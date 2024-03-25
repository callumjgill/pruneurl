using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Interfaces;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Requests;

[TestFixture]
[Parallelizable]
public sealed class FirestoreDbTransactionProviderUnitTests
{
  [Test]
  public void MaxAttemptsTest()
  {
    FirestoreDb firestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
    var firestoreTransactionIOptions = Substitute.For<IOptions<FirestoreTransactionOptions>>();
    var firestoreTransactionOptions = new FirestoreTransactionOptions() { MaxAttempts = 10 };

    firestoreTransactionIOptions.Value.Returns(firestoreTransactionOptions);

    var firestoreDbTransactionProvider = new FirestoreDbTransactionProvider(
      firestoreDb,
      Substitute.For<IFirestoreDbTransactionFactory>(),
      firestoreTransactionIOptions
    );
    Assert.That(
      firestoreDbTransactionProvider.MaxAttempts,
      Is.EqualTo(firestoreTransactionOptions.MaxAttempts)
    );
    _ = firestoreTransactionIOptions.Received(1).Value;
  }

  [Test]
  public async Task RunTransactionAsyncTest_ExecutesCallback()
  {
    const int maxAttempts = 5;
    FirestoreDb firestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
    var firestoreDbTransactionFactory = Substitute.For<IFirestoreDbTransactionFactory>();
    var firestoreTransactionIOptions = Substitute.For<IOptions<FirestoreTransactionOptions>>();
    var dbTransaction = Substitute.For<IDbTransaction<StubFirestoreEntity>>();
    var firestoreTransactionOptions = new FirestoreTransactionOptions()
    {
      MaxAttempts = maxAttempts
    };
    var stubEntity = new StubFirestoreEntity(string.Empty);

    firestoreTransactionIOptions.Value.Returns(firestoreTransactionOptions);
    firestoreDbTransactionFactory
      .Create<StubFirestoreEntity>(Arg.Any<Transaction>())
      .Returns(dbTransaction);

    bool callbackCalled = false;
    var firestoreDbTransactionProvider = new FirestoreDbTransactionProvider(
      firestoreDb,
      firestoreDbTransactionFactory,
      firestoreTransactionIOptions
    );
    StubFirestoreEntity actualEntity =
      await firestoreDbTransactionProvider.RunTransactionAsync<StubFirestoreEntity>(
        actualDbTransaction =>
        {
          Assert.That(actualDbTransaction, Is.EqualTo(dbTransaction));
          callbackCalled = true;
          return Task.FromResult(stubEntity);
        }
      );
    Assert.That(callbackCalled, Is.True);
    Assert.That(actualEntity, Is.EqualTo(stubEntity));
    firestoreDbTransactionFactory.Received(1).Create<StubFirestoreEntity>(Arg.Any<Transaction>());
  }
}
