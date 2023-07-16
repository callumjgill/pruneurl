using Google.Cloud.Firestore;
using PruneUrl.Backend.Application.Interfaces.Database.DbQuery;
using PruneUrl.Backend.Domain.Entities;
using PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.DbQuery
{
  /// <summary>
  /// Defines a factory for creating <see cref="IDbQuery{T}" /> ( <see cref="FirestoreDbQuery{T}"
  /// />) instances.
  /// </summary>
  public sealed class FirestoreDbQueryFactory : IDbQueryFactory
  {
    #region Private Fields

    private readonly FirestoreDb firestoreDb;

    #endregion Private Fields

    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="FirestoreDbQueryFactory" /> class.
    /// </summary>
    /// <param name="firestoreDb">
    /// The <see cref="FirestoreDb" /> instance for creating queries against.
    /// </param>
    public FirestoreDbQueryFactory(FirestoreDb firestoreDb)
    {
      this.firestoreDb = firestoreDb;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <inheritdoc cref="IDbQueryFactory.Create{T}" />
    public IDbQuery<T> Create<T>() where T : IEntity
    {
      CollectionReference collection = firestoreDb.Collection(CollectionReferenceHelper.GetCollectionPath<T>());
      return new FirestoreDbQuery<T>(collection);
    }

    #endregion Public Methods
  }
}