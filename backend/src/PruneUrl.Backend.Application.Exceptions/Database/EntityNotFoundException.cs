namespace PruneUrl.Backend.Application.Exceptions.Database
{
  /// <summary>
  /// An exception to be thrown when a particular entity must be found in the database but it wasn't.
  /// </summary>
  public sealed class EntityNotFoundException : Exception
  {
    #region Public Constructors

    /// <summary>
    /// Instantiates a new instance of the <see cref="EntityNotFoundException" /> class.
    /// </summary>
    /// <param name="entityType"> The type of entity that wasn't found. </param>
    /// <param name="id"> The id of the entity that wasn't found. </param>
    public EntityNotFoundException(Type entityType, string id) : base($"Entity of type {entityType} with id {id} was not found!")
    {
    }

    #endregion Public Constructors
  }
}