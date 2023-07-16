using Google.Cloud.Firestore;
using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Infrastructure.Database.Firestore.Utilities
{
  /// <summary>
  /// A helper class containing functions useful for dealing with <see cref="CollectionReference" />'s.
  /// </summary>
  internal static class CollectionReferenceHelper
  {
    #region Public Methods

    /// <summary>
    /// Returns the relative path for a <see cref="CollectionReference" /> given a <typeparamref
    /// name="T" /> type.
    /// </summary>
    /// <typeparam name="T"> The <see cref="IEntity" /> type for the collection of concern. </typeparam>
    /// <returns>
    /// The relative path for a <see cref="CollectionReference" /> holding documents concerning the
    /// <typeparamref name="T" /> type.
    /// </returns>
    public static string GetCollectionPath<T>() where T : IEntity
    {
      return $"{typeof(T).Name}s";
    }

    #endregion Public Methods
  }
}