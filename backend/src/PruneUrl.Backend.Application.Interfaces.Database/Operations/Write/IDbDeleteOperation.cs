﻿using PruneUrl.Backend.Domain.Entities;

namespace PruneUrl.Backend.Application.Interfaces.Database.Operations.Write
{
  /// <summary>
  /// Defines an operation for deleting a <see cref="IEntity" /> from the underlying database.
  /// </summary>
  public interface IDbDeleteOperation
  {
    #region Public Methods

    /// <summary>
    /// Performs the operation of deleting an entity.
    /// </summary>
    /// <param name="id"> The unique identifier of the <see cref="IEntity" /> to delete. </param>
    void Delete(string id);

    #endregion Public Methods
  }
}