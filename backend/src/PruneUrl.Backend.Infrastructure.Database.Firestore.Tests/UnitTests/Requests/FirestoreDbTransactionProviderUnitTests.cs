using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PruneUrl.Backend.Application.Interfaces.Database.Requests;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Configuration;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Interfaces;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Requests;
using PruneUrl.Backend.Infrastructure.Database.Tests.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Tests.UnitTests.Requests
{
  [TestFixture]
  [Parallelizable]
  public sealed class FirestoreDbTransactionProviderUnitTests
  {
    #region Public Methods

    [Test]
    public void MaxAttemptsTest()
    {
      FirestoreDb firestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      var firestoreTransactionOptionsMock = new Mock<IOptions<FirestoreTransactionOptions>>();
      var firestoreTransactionOptions = new FirestoreTransactionOptions()
      {
        MaxAttempts = 10
      };

      firestoreTransactionOptionsMock.Setup(x => x.Value).Returns(firestoreTransactionOptions);

      var firestoreDbTransactionProvider = new FirestoreDbTransactionProvider(firestoreDb, Mock.Of<IFirestoreDbTransactionFactory>(), firestoreTransactionOptionsMock.Object);
      firestoreTransactionOptionsMock.Verify(x => x.Value, Times.Once);
    }

    [Test]
    public async Task RunTransactionAsyncTest_ExecutesCallback()
    {
      const int maxAttempts = 5;
      FirestoreDb firestoreDb = TestFirestoreDbHelper.GetTestFirestoreDb();
      var firestoreDbTransactionFactoryMock = new Mock<IFirestoreDbTransactionFactory>();
      var firestoreTransactionOptionsMock = new Mock<IOptions<FirestoreTransactionOptions>>();
      var dbTransactionMock = new Mock<IDbTransaction<StubFirestoreEntity>>();
      var firestoreTransactionOptions = new FirestoreTransactionOptions()
      {
        MaxAttempts = maxAttempts
      };
      var stubEntity = new StubFirestoreEntity(string.Empty);

      firestoreTransactionOptionsMock.Setup(x => x.Value).Returns(firestoreTransactionOptions);
      firestoreDbTransactionFactoryMock.Setup(x => x.Create<StubFirestoreEntity>(It.IsAny<Transaction>())).Returns(dbTransactionMock.Object);

      bool callbackCalled = false;
      var firestoreDbTransactionProvider = new FirestoreDbTransactionProvider(firestoreDb, firestoreDbTransactionFactoryMock.Object, firestoreTransactionOptionsMock.Object);
      StubFirestoreEntity actualEntity = await firestoreDbTransactionProvider.RunTransactionAsync<StubFirestoreEntity>(dbTransaction =>
      {
        Assert.That(dbTransaction, Is.EqualTo(dbTransactionMock.Object));
        callbackCalled = true;
        return Task.FromResult(stubEntity);
      });
      Assert.That(callbackCalled, Is.True);
      Assert.That(actualEntity, Is.EqualTo(stubEntity));
      firestoreDbTransactionFactoryMock.Verify(x => x.Create<StubFirestoreEntity>(It.IsAny<Transaction>()), Times.Once);
    }

    #endregion Public Methods
  }
}