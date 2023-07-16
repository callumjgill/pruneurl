using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.DbTransaction;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DbTransaction
{
  /// <summary>
  /// A factory for creating <see cref="IDbTransaction{T}" /> ( <see
  /// cref="FirestoreDbTransaction{T}" />) instances.
  /// </summary>
  public sealed class FirestoreDbTransactionFactory : IDbTransactionFactory
  {
    #region Private Fields

    private readonly FirestoreDb firestoreDb;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbTransactionFactory" /> class.
    /// </summary>
    /// <param name="firestoreDb">
    /// The <see cref="FirestoreDb" /> instance for creating transactions against.
    /// </param>
    public FirestoreDbTransactionFactory(FirestoreDb firestoreDb)
    {
      this.firestoreDb = firestoreDb;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbTransactionFactory.Create{T}" />
    public IDbTransaction<T> Create<T>() where T : IEntity
    {
      CollectionReference collection = firestoreDb.Collection(CollectionReferenceHelper.GetCollectionPath<T>());
      WriteBatch writeBatch = firestoreDb.StartBatch();
      return new FirestoreDbTransaction<T>(collection, writeBatch);
    }

    #endregion Public Methods
  }
}